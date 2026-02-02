---
type: "EXAMPLE"
title: "Chat Endpoints Implementation"
---


**Core Chat API Endpoints**

These endpoints handle conversation management and message operations. The real-time streaming is handled separately.



```dart
// server/lib/src/endpoints/chat_endpoint.dart
import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';
import '../services/chat_service.dart';
import '../services/notification_service.dart';

class ChatEndpoint extends Endpoint {
  final ChatService _chatService = ChatService();
  final NotificationService _notifications = NotificationService();

  /// Create a new direct message conversation
  Future<Conversation> createDirectConversation(
    Session session, {
    required int otherUserId,
  }) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw ChatException(
        code: ChatErrorCode.unauthenticated,
        message: 'Please log in to start a conversation',
      );
    }
    
    final currentUser = await UserProfile.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(userId),
    );
    
    if (currentUser == null) {
      throw ChatException(
        code: ChatErrorCode.userNotFound,
        message: 'User profile not found',
      );
    }
    
    // Check if conversation already exists
    final existingConversation = await _findDirectConversation(
      session,
      currentUser.id!,
      otherUserId,
    );
    
    if (existingConversation != null) {
      return existingConversation;
    }
    
    // Verify other user exists
    final otherUser = await UserProfile.db.findById(session, otherUserId);
    if (otherUser == null || otherUser.isDeleted) {
      throw ChatException(
        code: ChatErrorCode.userNotFound,
        message: 'User not found',
      );
    }
    
    // Create new conversation
    final now = DateTime.now();
    final conversation = await Conversation.db.insertRow(
      session,
      Conversation(
        type: 'direct',
        isArchived: false,
        isMuted: false,
        participantCount: 2,
        createdAt: now,
      ),
    );
    
    // Add participants
    await ConversationParticipant.db.insert(session, [
      ConversationParticipant(
        conversationId: conversation.id!,
        userId: currentUser.id!,
        role: 'member',
        isMuted: false,
        unreadCount: 0,
        isActive: true,
        joinedAt: now,
      ),
      ConversationParticipant(
        conversationId: conversation.id!,
        userId: otherUserId,
        role: 'member',
        isMuted: false,
        unreadCount: 0,
        isActive: true,
        joinedAt: now,
      ),
    ]);
    
    return conversation;
  }

  /// Create a group conversation
  Future<Conversation> createGroupConversation(
    Session session, {
    required String name,
    String? description,
    required List<int> participantIds,
  }) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw ChatException(
        code: ChatErrorCode.unauthenticated,
        message: 'Please log in',
      );
    }
    
    final currentUser = await UserProfile.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(userId),
    );
    
    if (currentUser == null) {
      throw ChatException(
        code: ChatErrorCode.userNotFound,
        message: 'User profile not found',
      );
    }
    
    // Validate group size
    if (participantIds.isEmpty) {
      throw ChatException(
        code: ChatErrorCode.invalidInput,
        message: 'Group must have at least one other participant',
      );
    }
    
    if (participantIds.length > 100) {
      throw ChatException(
        code: ChatErrorCode.invalidInput,
        message: 'Group cannot have more than 100 participants',
      );
    }
    
    // Ensure creator is included
    final allParticipantIds = {...participantIds, currentUser.id!}.toList();
    
    // Verify all participants exist
    final participants = await UserProfile.db.find(
      session,
      where: (t) => t.id.inSet(allParticipantIds.toSet()),
    );
    
    if (participants.length != allParticipantIds.length) {
      throw ChatException(
        code: ChatErrorCode.userNotFound,
        message: 'One or more participants not found',
      );
    }
    
    // Create conversation
    final now = DateTime.now();
    final conversation = await Conversation.db.insertRow(
      session,
      Conversation(
        type: 'group',
        name: name,
        description: description,
        createdById: currentUser.id,
        isArchived: false,
        isMuted: false,
        participantCount: allParticipantIds.length,
        createdAt: now,
      ),
    );
    
    // Add participants
    final participantRecords = allParticipantIds.map((pId) {
      return ConversationParticipant(
        conversationId: conversation.id!,
        userId: pId,
        role: pId == currentUser.id ? 'owner' : 'member',
        isMuted: false,
        unreadCount: 0,
        isActive: true,
        joinedAt: now,
      );
    }).toList();
    
    await ConversationParticipant.db.insert(session, participantRecords);
    
    // Create system message
    await _createSystemMessage(
      session,
      conversation.id!,
      currentUser.id!,
      '${currentUser.displayName} created the group',
    );
    
    return conversation;
  }

  /// Get user's conversations with last message preview
  Future<List<ConversationWithDetails>> getUserConversations(
    Session session, {
    String? cursor,
    int limit = 20,
  }) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw ChatException(
        code: ChatErrorCode.unauthenticated,
        message: 'Please log in',
      );
    }
    
    final currentUser = await UserProfile.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(userId),
    );
    
    if (currentUser == null) {
      return [];
    }
    
    // Get user's active participations
    final participations = await ConversationParticipant.db.find(
      session,
      where: (t) => t.userId.equals(currentUser.id!) & 
                    t.isActive.equals(true),
    );
    
    if (participations.isEmpty) {
      return [];
    }
    
    final conversationIds = participations
        .map((p) => p.conversationId)
        .toSet();
    
    // Parse cursor
    DateTime? cursorTime;
    if (cursor != null) {
      cursorTime = DateTime.tryParse(cursor);
    }
    
    // Get conversations
    var whereClause = Conversation.t.id.inSet(conversationIds) &
        Conversation.t.isArchived.equals(false);
    
    if (cursorTime != null) {
      whereClause = whereClause & 
          (Conversation.t.lastMessageAt.lessThan(cursorTime) |
           Conversation.t.lastMessageAt.equals(null));
    }
    
    final conversations = await Conversation.db.find(
      session,
      where: (t) => whereClause,
      orderBy: (t) => t.lastMessageAt,
      orderDescending: true,
      limit: limit,
    );
    
    // Enrich with participant info and unread counts
    final enriched = <ConversationWithDetails>[];
    
    for (final conversation in conversations) {
      final participation = participations.firstWhere(
        (p) => p.conversationId == conversation.id,
      );
      
      // Get other participants for display
      final otherParticipants = await _getOtherParticipants(
        session,
        conversation.id!,
        currentUser.id!,
      );
      
      // Get last message sender info if available
      UserProfile? lastMessageSender;
      if (conversation.lastMessageSenderId != null) {
        lastMessageSender = await UserProfile.db.findById(
          session,
          conversation.lastMessageSenderId!,
        );
      }
      
      enriched.add(ConversationWithDetails(
        conversation: conversation,
        participants: otherParticipants,
        unreadCount: participation.unreadCount,
        isMuted: participation.isMuted,
        lastMessageSender: lastMessageSender,
      ));
    }
    
    return enriched;
  }

  /// Get messages in a conversation (paginated)
  Future<PaginatedMessages> getConversationMessages(
    Session session, {
    required int conversationId,
    String? cursor,
    int limit = 50,
  }) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw ChatException(
        code: ChatErrorCode.unauthenticated,
        message: 'Please log in',
      );
    }
    
    final currentUser = await UserProfile.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(userId),
    );
    
    if (currentUser == null) {
      throw ChatException(
        code: ChatErrorCode.userNotFound,
        message: 'User not found',
      );
    }
    
    // Verify user is participant
    final participation = await ConversationParticipant.db.findFirstRow(
      session,
      where: (t) => t.conversationId.equals(conversationId) &
                    t.userId.equals(currentUser.id!) &
                    t.isActive.equals(true),
    );
    
    if (participation == null) {
      throw ChatException(
        code: ChatErrorCode.accessDenied,
        message: 'You are not a member of this conversation',
      );
    }
    
    // Parse cursor
    DateTime? cursorTime;
    if (cursor != null) {
      cursorTime = DateTime.tryParse(cursor);
    }
    
    // Get messages
    var whereClause = Message.t.conversationId.equals(conversationId) &
        Message.t.isDeleted.equals(false);
    
    if (cursorTime != null) {
      whereClause = whereClause & Message.t.createdAt.lessThan(cursorTime);
    }
    
    final messages = await Message.db.find(
      session,
      where: (t) => whereClause,
      orderBy: (t) => t.createdAt,
      orderDescending: true,
      limit: limit,
    );
    
    // Enrich with sender info
    final enriched = <MessageWithSender>[];
    for (final message in messages) {
      final sender = await UserProfile.db.findById(session, message.senderId);
      
      // Get reply-to message if exists
      Message? replyTo;
      if (message.replyToMessageId != null) {
        replyTo = await Message.db.findById(session, message.replyToMessageId!);
      }
      
      enriched.add(MessageWithSender(
        message: message,
        sender: sender,
        replyToMessage: replyTo,
      ));
    }
    
    // Generate next cursor
    String? nextCursor;
    if (messages.length == limit) {
      nextCursor = messages.last.createdAt.toIso8601String();
    }
    
    return PaginatedMessages(
      messages: enriched.reversed.toList(), // Chronological order
      nextCursor: nextCursor,
      hasMore: messages.length == limit,
    );
  }

  /// Send a message (stores and broadcasts)
  Future<Message> sendMessage(
    Session session, {
    required int conversationId,
    required String content,
    required String clientMessageId,
    String messageType = 'text',
    int? replyToMessageId,
    String? mediaUrl,
    String? mediaThumbnailUrl,
    String? mediaMetadata,
  }) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw ChatException(
        code: ChatErrorCode.unauthenticated,
        message: 'Please log in',
      );
    }
    
    final currentUser = await UserProfile.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(userId),
    );
    
    if (currentUser == null) {
      throw ChatException(
        code: ChatErrorCode.userNotFound,
        message: 'User not found',
      );
    }
    
    // Check for duplicate (idempotency)
    final existingMessage = await Message.db.findFirstRow(
      session,
      where: (t) => t.clientMessageId.equals(clientMessageId),
    );
    
    if (existingMessage != null) {
      return existingMessage; // Return existing message (idempotent)
    }
    
    // Verify user is participant
    final participation = await ConversationParticipant.db.findFirstRow(
      session,
      where: (t) => t.conversationId.equals(conversationId) &
                    t.userId.equals(currentUser.id!) &
                    t.isActive.equals(true),
    );
    
    if (participation == null) {
      throw ChatException(
        code: ChatErrorCode.accessDenied,
        message: 'You are not a member of this conversation',
      );
    }
    
    // Validate content
    if (messageType == 'text' && content.trim().isEmpty) {
      throw ChatException(
        code: ChatErrorCode.invalidInput,
        message: 'Message cannot be empty',
      );
    }
    
    if (content.length > 5000) {
      throw ChatException(
        code: ChatErrorCode.invalidInput,
        message: 'Message too long (max 5000 characters)',
      );
    }
    
    // Create message
    final now = DateTime.now();
    final message = await Message.db.insertRow(
      session,
      Message(
        conversationId: conversationId,
        senderId: currentUser.id!,
        content: content,
        messageType: messageType,
        mediaUrl: mediaUrl,
        mediaThumbnailUrl: mediaThumbnailUrl,
        mediaMetadata: mediaMetadata,
        replyToMessageId: replyToMessageId,
        status: 'sent',
        clientMessageId: clientMessageId,
        isEdited: false,
        isDeleted: false,
        createdAt: now,
        sentAt: now,
      ),
    );
    
    // Update conversation
    final conversation = await Conversation.db.findById(
      session,
      conversationId,
    );
    
    if (conversation != null) {
      await Conversation.db.updateRow(
        session,
        conversation.copyWith(
          lastMessageAt: now,
          lastMessagePreview: _truncatePreview(content),
          lastMessageSenderId: currentUser.id,
          updatedAt: now,
        ),
      );
    }
    
    // Increment unread count for other participants
    await _incrementUnreadCounts(
      session,
      conversationId,
      currentUser.id!,
    );
    
    // Broadcast message to connected clients (handled by streaming endpoint)
    await _chatService.broadcastMessage(
      session,
      conversationId,
      message,
      currentUser,
    );
    
    // Send push notifications to offline users
    await _notifications.notifyNewMessage(
      session,
      conversationId: conversationId,
      senderId: currentUser.id!,
      senderName: currentUser.displayName ?? 'Someone',
      messagePreview: _truncatePreview(content),
    );
    
    return message;
  }

  /// Mark messages as read
  Future<void> markMessagesAsRead(
    Session session, {
    required int conversationId,
    required int lastReadMessageId,
  }) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) return;
    
    final currentUser = await UserProfile.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(userId),
    );
    
    if (currentUser == null) return;
    
    // Update participation
    final participation = await ConversationParticipant.db.findFirstRow(
      session,
      where: (t) => t.conversationId.equals(conversationId) &
                    t.userId.equals(currentUser.id!),
    );
    
    if (participation == null) return;
    
    final now = DateTime.now();
    await ConversationParticipant.db.updateRow(
      session,
      participation.copyWith(
        lastReadAt: now,
        lastReadMessageId: lastReadMessageId,
        unreadCount: 0,
        updatedAt: now,
      ),
    );
    
    // Broadcast read receipt
    await _chatService.broadcastReadReceipt(
      session,
      conversationId,
      currentUser.id!,
      lastReadMessageId,
    );
  }

  // Helper methods
  
  Future<Conversation?> _findDirectConversation(
    Session session,
    int userId1,
    int userId2,
  ) async {
    // Find conversations where both users are participants
    final user1Participations = await ConversationParticipant.db.find(
      session,
      where: (t) => t.userId.equals(userId1) & t.isActive.equals(true),
    );
    
    for (final participation in user1Participations) {
      final conversation = await Conversation.db.findById(
        session,
        participation.conversationId,
      );
      
      if (conversation?.type != 'direct') continue;
      
      // Check if other user is also in this conversation
      final otherParticipation = await ConversationParticipant.db.findFirstRow(
        session,
        where: (t) => t.conversationId.equals(participation.conversationId) &
                      t.userId.equals(userId2) &
                      t.isActive.equals(true),
      );
      
      if (otherParticipation != null) {
        return conversation;
      }
    }
    
    return null;
  }
  
  Future<List<UserProfile>> _getOtherParticipants(
    Session session,
    int conversationId,
    int currentUserId,
  ) async {
    final participations = await ConversationParticipant.db.find(
      session,
      where: (t) => t.conversationId.equals(conversationId) &
                    t.isActive.equals(true) &
                    t.userId.notEquals(currentUserId),
      limit: 10, // Limit for performance
    );
    
    final users = <UserProfile>[];
    for (final participation in participations) {
      final user = await UserProfile.db.findById(session, participation.userId);
      if (user != null) {
        users.add(user);
      }
    }
    
    return users;
  }
  
  Future<void> _incrementUnreadCounts(
    Session session,
    int conversationId,
    int excludeUserId,
  ) async {
    await session.db.unsafeExecute(
      '''
      UPDATE conversation_participants 
      SET unread_count = unread_count + 1 
      WHERE conversation_id = @conversationId 
        AND user_id != @excludeUserId 
        AND is_active = true
      ''',
      parameters: QueryParameters.named({
        'conversationId': conversationId,
        'excludeUserId': excludeUserId,
      }),
    );
  }
  
  Future<void> _createSystemMessage(
    Session session,
    int conversationId,
    int senderId,
    String content,
  ) async {
    final now = DateTime.now();
    await Message.db.insertRow(
      session,
      Message(
        conversationId: conversationId,
        senderId: senderId,
        content: content,
        messageType: 'system',
        status: 'sent',
        clientMessageId: 'system-${now.millisecondsSinceEpoch}',
        isEdited: false,
        isDeleted: false,
        createdAt: now,
        sentAt: now,
      ),
    );
  }
  
  String _truncatePreview(String content) {
    if (content.length <= 100) return content;
    return '${content.substring(0, 97)}...';
  }
}
```
