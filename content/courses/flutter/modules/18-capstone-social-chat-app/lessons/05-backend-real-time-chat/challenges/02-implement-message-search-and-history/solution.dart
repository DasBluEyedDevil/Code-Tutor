// First, add full-text search index in migration
// migrations/YYYYMMDDHHMMSS_add_message_search_index.sql
// CREATE INDEX messages_content_search_idx ON messages 
//   USING gin(to_tsvector('english', content));

// server/lib/src/endpoints/message_search_endpoint.dart
import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';

class MessageSearchEndpoint extends Endpoint {
  /// Search messages across all user's conversations
  Future<MessageSearchResults> searchMessages(
    Session session, {
    required String query,
    int? conversationId,
    int limit = 20,
    String? cursor,
  }) async {
    // 1. Authenticate
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw SearchException(
        code: 'unauthenticated',
        message: 'Please log in to search messages',
      );
    }
    
    final user = await UserProfile.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(userId),
    );
    
    if (user == null) {
      throw SearchException(
        code: 'user_not_found',
        message: 'User not found',
      );
    }
    
    // Validate query
    final trimmedQuery = query.trim();
    if (trimmedQuery.isEmpty) {
      return MessageSearchResults(
        results: [],
        totalCount: 0,
        hasMore: false,
      );
    }
    
    if (trimmedQuery.length < 2) {
      throw SearchException(
        code: 'query_too_short',
        message: 'Search query must be at least 2 characters',
      );
    }
    
    // 2. Get user's conversation IDs
    List<int> conversationIds;
    if (conversationId != null) {
      // Verify user is in this conversation
      final participation = await ConversationParticipant.db.findFirstRow(
        session,
        where: (t) => t.conversationId.equals(conversationId) &
                      t.userId.equals(user.id!) &
                      t.isActive.equals(true),
      );
      
      if (participation == null) {
        throw SearchException(
          code: 'access_denied',
          message: 'You are not a member of this conversation',
        );
      }
      
      conversationIds = [conversationId];
    } else {
      // Get all user's conversations
      final participations = await ConversationParticipant.db.find(
        session,
        where: (t) => t.userId.equals(user.id!) & t.isActive.equals(true),
      );
      conversationIds = participations.map((p) => p.conversationId).toList();
    }
    
    if (conversationIds.isEmpty) {
      return MessageSearchResults(
        results: [],
        totalCount: 0,
        hasMore: false,
      );
    }
    
    // 3. Parse cursor
    DateTime? cursorTime;
    int? cursorId;
    if (cursor != null) {
      final parts = cursor.split('_');
      if (parts.length == 2) {
        cursorTime = DateTime.tryParse(parts[0]);
        cursorId = int.tryParse(parts[1]);
      }
    }
    
    // 4. Search using PostgreSQL full-text search
    final searchTerms = _prepareSearchTerms(trimmedQuery);
    
    // Using raw SQL for full-text search
    final results = await session.db.unsafeQuery(
      '''
      SELECT m.*, 
             ts_rank(to_tsvector('english', m.content), plainto_tsquery('english', @query)) as rank
      FROM messages m
      WHERE m.conversation_id = ANY(@conversationIds)
        AND m.is_deleted = false
        AND (
          to_tsvector('english', m.content) @@ plainto_tsquery('english', @query)
          OR m.content ILIKE @likeQuery
        )
        ${cursorTime != null ? "AND (m.created_at < @cursorTime OR (m.created_at = @cursorTime AND m.id < @cursorId))" : ""}
      ORDER BY m.created_at DESC, m.id DESC
      LIMIT @limit
      ''',
      parameters: QueryParameters.named({
        'query': searchTerms,
        'likeQuery': '%$trimmedQuery%',
        'conversationIds': conversationIds,
        'limit': limit + 1,  // Fetch one extra to check hasMore
        if (cursorTime != null) 'cursorTime': cursorTime,
        if (cursorId != null) 'cursorId': cursorId,
      }),
    );
    
    // 5. Process results
    final hasMore = results.length > limit;
    final messageRows = hasMore ? results.take(limit).toList() : results;
    
    final searchResults = <SearchResultItem>[];
    
    for (final row in messageRows) {
      // Parse message from row
      final message = Message(
        id: row['id'] as int,
        conversationId: row['conversation_id'] as int,
        senderId: row['sender_id'] as int,
        content: row['content'] as String,
        messageType: row['message_type'] as String,
        status: row['status'] as String,
        clientMessageId: row['client_message_id'] as String,
        isEdited: row['is_edited'] as bool,
        isDeleted: row['is_deleted'] as bool,
        createdAt: row['created_at'] as DateTime,
      );
      
      // Get sender
      final sender = await UserProfile.db.findById(
        session,
        message.senderId,
      );
      
      // Get conversation
      final conversation = await Conversation.db.findById(
        session,
        message.conversationId,
      );
      
      // Get conversation name
      String? conversationName;
      if (conversation != null) {
        if (conversation.type == 'group') {
          conversationName = conversation.name;
        } else {
          // Direct message - get other participant's name
          final otherParticipant = await _getOtherParticipant(
            session,
            message.conversationId,
            user.id!,
          );
          conversationName = otherParticipant?.displayName ?? 'Unknown';
        }
      }
      
      // Highlight matched text
      final highlighted = _highlightMatches(
        message.content,
        trimmedQuery,
      );
      
      searchResults.add(SearchResultItem(
        message: message,
        highlightedContent: highlighted,
        sender: sender,
        conversation: conversation,
        conversationName: conversationName,
      ));
    }
    
    // Generate next cursor
    String? nextCursor;
    if (hasMore && searchResults.isNotEmpty) {
      final lastMessage = searchResults.last.message;
      nextCursor = '${lastMessage.createdAt.toIso8601String()}_${lastMessage.id}';
    }
    
    // Get total count (for UI)
    final countResult = await session.db.unsafeQuery(
      '''
      SELECT COUNT(*) as total
      FROM messages m
      WHERE m.conversation_id = ANY(@conversationIds)
        AND m.is_deleted = false
        AND (
          to_tsvector('english', m.content) @@ plainto_tsquery('english', @query)
          OR m.content ILIKE @likeQuery
        )
      ''',
      parameters: QueryParameters.named({
        'query': searchTerms,
        'likeQuery': '%$trimmedQuery%',
        'conversationIds': conversationIds,
      }),
    );
    
    final totalCount = countResult.first['total'] as int;
    
    return MessageSearchResults(
      results: searchResults,
      totalCount: totalCount,
      nextCursor: nextCursor,
      hasMore: hasMore,
    );
  }

  /// Get message with surrounding context
  Future<MessageContext> getMessageContext(
    Session session, {
    required int messageId,
    int surroundingCount = 5,
  }) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw SearchException(
        code: 'unauthenticated',
        message: 'Please log in',
      );
    }
    
    final user = await UserProfile.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(userId),
    );
    
    if (user == null) {
      throw SearchException(
        code: 'user_not_found',
        message: 'User not found',
      );
    }
    
    // Get target message
    final targetMessage = await Message.db.findById(session, messageId);
    if (targetMessage == null || targetMessage.isDeleted) {
      throw SearchException(
        code: 'message_not_found',
        message: 'Message not found',
      );
    }
    
    // Verify access
    final participation = await ConversationParticipant.db.findFirstRow(
      session,
      where: (t) => t.conversationId.equals(targetMessage.conversationId) &
                    t.userId.equals(user.id!) &
                    t.isActive.equals(true),
    );
    
    if (participation == null) {
      throw SearchException(
        code: 'access_denied',
        message: 'You are not a member of this conversation',
      );
    }
    
    // Get messages before
    final messagesBefore = await Message.db.find(
      session,
      where: (t) => t.conversationId.equals(targetMessage.conversationId) &
                    t.createdAt.lessThan(targetMessage.createdAt) &
                    t.isDeleted.equals(false),
      orderBy: (t) => t.createdAt,
      orderDescending: true,
      limit: surroundingCount,
    );
    
    // Get messages after
    final messagesAfter = await Message.db.find(
      session,
      where: (t) => t.conversationId.equals(targetMessage.conversationId) &
                    t.createdAt.greaterThan(targetMessage.createdAt) &
                    t.isDeleted.equals(false),
      orderBy: (t) => t.createdAt,
      limit: surroundingCount,
    );
    
    // Get conversation
    final conversation = await Conversation.db.findById(
      session,
      targetMessage.conversationId,
    );
    
    if (conversation == null) {
      throw SearchException(
        code: 'conversation_not_found',
        message: 'Conversation not found',
      );
    }
    
    return MessageContext(
      targetMessage: targetMessage,
      messagesBefore: messagesBefore.reversed.toList(),
      messagesAfter: messagesAfter,
      conversation: conversation,
    );
  }
  
  /// Prepare search terms for full-text search
  String _prepareSearchTerms(String query) {
    // Escape special characters and prepare for plainto_tsquery
    return query
        .replaceAll(RegExp(r'[&|!():]'), ' ')
        .trim();
  }
  
  /// Highlight search matches in text
  String _highlightMatches(String text, String searchTerm) {
    if (searchTerm.isEmpty) return text;
    
    // Case-insensitive replacement with highlight tags
    final pattern = RegExp(
      RegExp.escape(searchTerm),
      caseSensitive: false,
    );
    
    return text.replaceAllMapped(
      pattern,
      (match) => '<mark>${match.group(0)}</mark>',
    );
  }
  
  /// Get other participant in direct message
  Future<UserProfile?> _getOtherParticipant(
    Session session,
    int conversationId,
    int currentUserId,
  ) async {
    final participation = await ConversationParticipant.db.findFirstRow(
      session,
      where: (t) => t.conversationId.equals(conversationId) &
                    t.userId.notEquals(currentUserId) &
                    t.isActive.equals(true),
    );
    
    if (participation == null) return null;
    
    return UserProfile.db.findById(session, participation.userId);
  }
}

class SearchException implements Exception {
  final String code;
  final String message;
  
  SearchException({required this.code, required this.message});
  
  @override
  String toString() => 'SearchException: [$code] $message';
}

class MessageSearchResults {
  final List<SearchResultItem> results;
  final int totalCount;
  final String? nextCursor;
  final bool hasMore;
  
  MessageSearchResults({
    required this.results,
    required this.totalCount,
    this.nextCursor,
    required this.hasMore,
  });
}

class SearchResultItem {
  final Message message;
  final String highlightedContent;
  final UserProfile? sender;
  final Conversation? conversation;
  final String? conversationName;
  
  SearchResultItem({
    required this.message,
    required this.highlightedContent,
    this.sender,
    this.conversation,
    this.conversationName,
  });
}

class MessageContext {
  final Message targetMessage;
  final List<Message> messagesBefore;
  final List<Message> messagesAfter;
  final Conversation conversation;
  
  MessageContext({
    required this.targetMessage,
    required this.messagesBefore,
    required this.messagesAfter,
    required this.conversation,
  });
  
  /// Get all messages in order
  List<Message> get allMessages => [
    ...messagesBefore,
    targetMessage,
    ...messagesAfter,
  ];
}