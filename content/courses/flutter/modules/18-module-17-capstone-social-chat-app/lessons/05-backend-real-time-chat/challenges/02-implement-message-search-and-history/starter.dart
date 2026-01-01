// server/lib/src/endpoints/message_search_endpoint.dart
import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';

class MessageSearchEndpoint extends Endpoint {
  /// Search messages across all user's conversations
  Future<MessageSearchResults> searchMessages(
    Session session, {
    required String query,
    int? conversationId,  // Optional: filter to specific conversation
    int limit = 20,
    String? cursor,
  }) async {
    // TODO: Implement
    // 1. Authenticate user
    // 2. Get user's conversation IDs
    // 3. Search messages with full-text matching
    // 4. Filter by conversation if specified
    // 5. Highlight matched text
    // 6. Return paginated results with context
    throw UnimplementedError();
  }

  /// Get message context (messages before and after)
  Future<MessageContext> getMessageContext(
    Session session, {
    required int messageId,
    int surroundingCount = 5,
  }) async {
    // TODO: Implement
    // Return the message with N messages before and after
    throw UnimplementedError();
  }
  
  /// Highlight search term in text
  String _highlightMatches(
    String text,
    String searchTerm,
  ) {
    // TODO: Implement
    // Return text with <mark>matched</mark> tags around matches
    throw UnimplementedError();
  }
}

/// Search result container
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

/// Single search result with context
class SearchResultItem {
  final Message message;
  final String highlightedContent;  // Content with <mark> tags
  final UserProfile? sender;
  final Conversation? conversation;
  final String? conversationName;  // For display
  
  SearchResultItem({
    required this.message,
    required this.highlightedContent,
    this.sender,
    this.conversation,
    this.conversationName,
  });
}

/// Message with surrounding context
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
}