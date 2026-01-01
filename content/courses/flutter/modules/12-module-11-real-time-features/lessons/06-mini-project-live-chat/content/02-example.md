---
type: "EXAMPLE"
title: "Backend Setup"
---

The Serverpod backend needs endpoints for both REST operations (loading history) and streaming (real-time updates). We'll create a complete chat module.

**Database Schema:**

We need tables for conversations, messages, and participants.

**Endpoints:**

- `getConversations()`: List user's conversations
- `getMessages()`: Load message history with pagination
- `sendMessage()`: Send a new message (also via stream)
- `streamOpened()`: Handle streaming connection
- `setTypingStatus()`: Broadcast typing state



```dart
// server/lib/src/models/chat_models.dart
import 'package:serverpod/serverpod.dart';

// Conversation between two or more users
class Conversation extends TableRow {
  @override
  int? id;
  
  DateTime createdAt;
  DateTime updatedAt;
  String? lastMessagePreview;
  DateTime? lastMessageAt;
  
  Conversation({
    this.id,
    required this.createdAt,
    required this.updatedAt,
    this.lastMessagePreview,
    this.lastMessageAt,
  });
}

// Participant in a conversation
class ConversationParticipant extends TableRow {
  @override
  int? id;
  
  int conversationId;
  int userId;
  DateTime joinedAt;
  DateTime? lastReadAt;
  bool isTyping;
  
  ConversationParticipant({
    this.id,
    required this.conversationId,
    required this.userId,
    required this.joinedAt,
    this.lastReadAt,
    this.isTyping = false,
  });
}

// Individual chat message
class ChatMessage extends TableRow {
  @override
  int? id;
  
  int conversationId;
  int senderId;
  String content;
  String? localId; // For optimistic updates
  DateTime createdAt;
  MessageStatus status;
  
  ChatMessage({
    this.id,
    required this.conversationId,
    required this.senderId,
    required this.content,
    this.localId,
    required this.createdAt,
    this.status = MessageStatus.sent,
  });
}

enum MessageStatus { sending, sent, delivered, read }

// server/lib/src/endpoints/chat_endpoint.dart
class ChatEndpoint extends Endpoint {
  // Track active streaming sessions per conversation
  static final Map<int, Set<StreamingSession>> _conversationSessions = {};
  
  // REST: Get user's conversations
  Future<List<ConversationWithDetails>> getConversations(
    Session session,
  ) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) throw UnauthorizedException();
    
    // Get conversations where user is a participant
    final participants = await ConversationParticipant.db.find(
      session,
      where: (p) => p.userId.equals(userId),
    );
    
    final conversationIds = participants.map((p) => p.conversationId).toList();
    
    final conversations = await Conversation.db.find(
      session,
      where: (c) => c.id.inSet(conversationIds),
      orderBy: (c) => c.lastMessageAt,
      orderDescending: true,
    );
    
    // Add participant details to each conversation
    return Future.wait(conversations.map((c) async {
      final parts = await ConversationParticipant.db.find(
        session,
        where: (p) => p.conversationId.equals(c.id!),
      );
      return ConversationWithDetails(
        conversation: c,
        participants: parts,
        unreadCount: await _getUnreadCount(session, c.id!, userId),
      );
    }));
  }
  
  // REST: Get messages with pagination
  Future<List<ChatMessage>> getMessages(
    Session session, {
    required int conversationId,
    DateTime? before,
    int limit = 50,
  }) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) throw UnauthorizedException();
    
    // Verify user is participant
    await _verifyParticipant(session, conversationId, userId);
    
    var query = ChatMessage.db.find(
      session,
      where: (m) {
        var condition = m.conversationId.equals(conversationId);
        if (before != null) {
          condition = condition & m.createdAt.lessThan(before);
        }
        return condition;
      },
      orderBy: (m) => m.createdAt,
      orderDescending: true,
      limit: limit,
    );
    
    return query;
  }
  
  // REST: Send a message (non-streaming fallback)
  Future<ChatMessage> sendMessage(
    Session session, {
    required int conversationId,
    required String content,
    String? localId,
  }) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) throw UnauthorizedException();
    
    await _verifyParticipant(session, conversationId, userId);
    
    final message = ChatMessage(
      conversationId: conversationId,
      senderId: userId,
      content: content,
      localId: localId,
      createdAt: DateTime.now(),
      status: MessageStatus.sent,
    );
    
    final saved = await ChatMessage.db.insertRow(session, message);
    
    // Update conversation
    await _updateConversationLastMessage(session, conversationId, content);
    
    // Broadcast to connected clients
    await _broadcastMessage(saved);
    
    // Send push to offline participants
    await _sendPushToOffline(session, conversationId, userId, saved);
    
    return saved;
  }
  
  // Streaming: Handle new connection
  @override
  Future<void> streamOpened(StreamingSession session) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      session.close();
      return;
    }
    
    // Register message handler
    session.messages.addListener('chat_message', (message) async {
      if (message is ChatMessage) {
        await _handleStreamMessage(session, userId, message);
      }
    });
    
    // Register typing handler
    session.messages.addListener('typing', (message) async {
      if (message is TypingStatus) {
        await _handleTypingStatus(session, message);
      }
    });
    
    // Update user presence to online
    await PresenceManager.userConnected(userId, session);
  }
  
  @override
  Future<void> streamClosed(StreamingSession session) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId != null) {
      await PresenceManager.userDisconnected(userId, session);
    }
    
    // Remove from all conversation sessions
    for (final sessions in _conversationSessions.values) {
      sessions.remove(session);
    }
  }
  
  // Join a conversation stream
  Future<void> joinConversation(
    Session session, {
    required int conversationId,
  }) async {
    final streamingSession = session as StreamingSession;
    final userId = await session.auth.authenticatedUserId;
    
    await _verifyParticipant(session, conversationId, userId!);
    
    _conversationSessions.putIfAbsent(conversationId, () => {});
    _conversationSessions[conversationId]!.add(streamingSession);
    
    // Mark messages as delivered
    await _markMessagesDelivered(session, conversationId, userId);
  }
  
  // Helper methods...
  Future<void> _broadcastMessage(ChatMessage message) async {
    final sessions = _conversationSessions[message.conversationId] ?? {};
    
    for (final session in sessions) {
      try {
        await session.messages.postMessage('new_message', message);
      } catch (e) {
        // Session may be closed
        sessions.remove(session);
      }
    }
  }
}
```
