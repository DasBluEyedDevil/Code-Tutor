---
type: "EXAMPLE"
title: "Real-Time Message Streaming"
---


**Implementing WebSocket Streaming for Chat**

Serverpod's `StreamingSession` provides the foundation for real-time communication. We'll create a dedicated streaming endpoint that handles message broadcasting, connection tracking, and reconnection logic.



```dart
// server/lib/src/endpoints/chat_streaming_endpoint.dart
import 'dart:async';
import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';
import '../services/connection_manager.dart';

/// Streaming endpoint for real-time chat functionality
class ChatStreamingEndpoint extends Endpoint {
  /// Connection manager tracks who's online in which conversations
  static final ConnectionManager _connectionManager = ConnectionManager();
  
  /// Called when client opens streaming connection
  @override
  Future<void> streamOpened(StreamingSession session) async {
    // Get authenticated user
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      // Close unauthorized connections
      await session.close();
      return;
    }
    
    final user = await UserProfile.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(userId),
    );
    
    if (user == null) {
      await session.close();
      return;
    }
    
    // Register connection
    _connectionManager.registerConnection(
      sessionId: session.sessionId,
      userId: user.id!,
    );
    
    // Get user's active conversations
    final participations = await ConversationParticipant.db.find(
      session,
      where: (t) => t.userId.equals(user.id!) & t.isActive.equals(true),
    );
    
    // Subscribe to all user's conversations
    for (final participation in participations) {
      _connectionManager.subscribeToConversation(
        sessionId: session.sessionId,
        conversationId: participation.conversationId,
      );
    }
    
    // Broadcast presence update
    await _broadcastPresence(
      session,
      user.id!,
      PresenceStatus.online,
    );
    
    // Send any missed messages since last connection
    await _sendMissedMessages(session, user.id!, participations);
    
    session.log('User ${user.id} connected to chat streaming');
  }

  /// Called when client closes connection
  @override
  Future<void> streamClosed(StreamingSession session) async {
    final connection = _connectionManager.getConnection(session.sessionId);
    
    if (connection != null) {
      // Update last seen timestamp
      await _updateLastSeen(session, connection.userId);
      
      // Broadcast offline status
      await _broadcastPresence(
        session,
        connection.userId,
        PresenceStatus.offline,
      );
      
      // Remove connection
      _connectionManager.removeConnection(session.sessionId);
      
      session.log('User ${connection.userId} disconnected from chat');
    }
  }

  /// Handle incoming stream messages from clients
  @override
  Future<void> handleStreamMessage(
    StreamingSession session,
    SerializableModel message,
  ) async {
    final connection = _connectionManager.getConnection(session.sessionId);
    if (connection == null) {
      return; // Not authenticated
    }
    
    if (message is ChatStreamMessage) {
      switch (message.type) {
        case ChatMessageType.message:
          await _handleNewMessage(session, connection, message);
          break;
        case ChatMessageType.typing:
          await _handleTypingIndicator(session, connection, message);
          break;
        case ChatMessageType.read:
          await _handleReadReceipt(session, connection, message);
          break;
        case ChatMessageType.presence:
          await _handlePresenceUpdate(session, connection, message);
          break;
      }
    }
  }

  /// Handle new message from client
  Future<void> _handleNewMessage(
    StreamingSession session,
    UserConnection connection,
    ChatStreamMessage message,
  ) async {
    if (message.conversationId == null || message.content == null) {
      return;
    }
    
    final user = await UserProfile.db.findById(session, connection.userId);
    if (user == null) return;
    
    // Verify user is in conversation
    final participation = await ConversationParticipant.db.findFirstRow(
      session,
      where: (t) => t.conversationId.equals(message.conversationId!) &
                    t.userId.equals(connection.userId) &
                    t.isActive.equals(true),
    );
    
    if (participation == null) {
      // Send error back to client
      await sendStreamMessage(
        session,
        ChatStreamError(
          type: 'error',
          code: 'access_denied',
          message: 'Not a member of this conversation',
          clientMessageId: message.clientMessageId,
        ),
      );
      return;
    }
    
    // Check for duplicate (idempotency)
    if (message.clientMessageId != null) {
      final existing = await Message.db.findFirstRow(
        session,
        where: (t) => t.clientMessageId.equals(message.clientMessageId!),
      );
      
      if (existing != null) {
        // Already processed, send confirmation
        await sendStreamMessage(
          session,
          ChatStreamAck(
            type: 'ack',
            clientMessageId: message.clientMessageId!,
            serverMessageId: existing.id!,
            timestamp: existing.sentAt ?? existing.createdAt,
          ),
        );
        return;
      }
    }
    
    // Create message
    final now = DateTime.now();
    final savedMessage = await Message.db.insertRow(
      session,
      Message(
        conversationId: message.conversationId!,
        senderId: connection.userId,
        content: message.content!,
        messageType: message.messageType ?? 'text',
        replyToMessageId: message.replyToMessageId,
        mediaUrl: message.mediaUrl,
        status: 'sent',
        clientMessageId: message.clientMessageId ?? 
            'server-${now.millisecondsSinceEpoch}',
        isEdited: false,
        isDeleted: false,
        createdAt: now,
        sentAt: now,
      ),
    );
    
    // Update conversation last message
    await _updateConversationLastMessage(
      session,
      message.conversationId!,
      savedMessage,
      user,
    );
    
    // Send acknowledgment to sender
    await sendStreamMessage(
      session,
      ChatStreamAck(
        type: 'ack',
        clientMessageId: message.clientMessageId ?? '',
        serverMessageId: savedMessage.id!,
        timestamp: now,
      ),
    );
    
    // Broadcast to all participants
    await _broadcastToConversation(
      session,
      message.conversationId!,
      ChatStreamMessage(
        type: ChatMessageType.message,
        conversationId: message.conversationId,
        messageId: savedMessage.id,
        senderId: connection.userId,
        senderName: user.displayName,
        senderAvatarUrl: user.avatarUrl,
        content: message.content,
        messageType: message.messageType ?? 'text',
        replyToMessageId: message.replyToMessageId,
        mediaUrl: message.mediaUrl,
        timestamp: now,
        clientMessageId: message.clientMessageId,
      ),
      excludeSessionId: session.sessionId,
    );
    
    // Update unread counts
    await _incrementUnreadCounts(
      session,
      message.conversationId!,
      connection.userId,
    );
  }

  /// Handle typing indicator
  Future<void> _handleTypingIndicator(
    StreamingSession session,
    UserConnection connection,
    ChatStreamMessage message,
  ) async {
    if (message.conversationId == null) return;
    
    final user = await UserProfile.db.findById(session, connection.userId);
    if (user == null) return;
    
    // Broadcast typing status to other participants
    await _broadcastToConversation(
      session,
      message.conversationId!,
      ChatStreamMessage(
        type: ChatMessageType.typing,
        conversationId: message.conversationId,
        senderId: connection.userId,
        senderName: user.displayName,
        isTyping: message.isTyping ?? true,
        timestamp: DateTime.now(),
      ),
      excludeSessionId: session.sessionId,
    );
  }

  /// Handle read receipt
  Future<void> _handleReadReceipt(
    StreamingSession session,
    UserConnection connection,
    ChatStreamMessage message,
  ) async {
    if (message.conversationId == null || message.messageId == null) {
      return;
    }
    
    final now = DateTime.now();
    
    // Update participation
    final participation = await ConversationParticipant.db.findFirstRow(
      session,
      where: (t) => t.conversationId.equals(message.conversationId!) &
                    t.userId.equals(connection.userId),
    );
    
    if (participation != null) {
      await ConversationParticipant.db.updateRow(
        session,
        participation.copyWith(
          lastReadAt: now,
          lastReadMessageId: message.messageId,
          unreadCount: 0,
          updatedAt: now,
        ),
      );
    }
    
    // Update message status records
    await _updateMessageStatuses(
      session,
      message.conversationId!,
      connection.userId,
      message.messageId!,
    );
    
    // Broadcast read receipt
    await _broadcastToConversation(
      session,
      message.conversationId!,
      ChatStreamMessage(
        type: ChatMessageType.read,
        conversationId: message.conversationId,
        messageId: message.messageId,
        senderId: connection.userId,
        timestamp: now,
      ),
      excludeSessionId: session.sessionId,
    );
  }

  /// Handle presence update
  Future<void> _handlePresenceUpdate(
    StreamingSession session,
    UserConnection connection,
    ChatStreamMessage message,
  ) async {
    // Update user's presence status
    _connectionManager.updatePresence(
      sessionId: session.sessionId,
      status: message.presenceStatus ?? PresenceStatus.online,
    );
    
    await _broadcastPresence(
      session,
      connection.userId,
      message.presenceStatus ?? PresenceStatus.online,
    );
  }

  /// Broadcast message to all connections in a conversation
  Future<void> _broadcastToConversation(
    StreamingSession session,
    int conversationId,
    SerializableModel message, {
    String? excludeSessionId,
  }) async {
    final sessionIds = _connectionManager.getConversationSessions(
      conversationId,
    );
    
    for (final sessionId in sessionIds) {
      if (sessionId == excludeSessionId) continue;
      
      try {
        await session.messages.postMessage(
          sessionId,
          message,
        );
      } catch (e) {
        // Connection may have closed, remove it
        _connectionManager.removeConnection(sessionId);
      }
    }
  }

  /// Broadcast presence update to relevant users
  Future<void> _broadcastPresence(
    Session session,
    int userId,
    PresenceStatus status,
  ) async {
    // Get all conversations this user is in
    final participations = await ConversationParticipant.db.find(
      session,
      where: (t) => t.userId.equals(userId) & t.isActive.equals(true),
    );
    
    final user = await UserProfile.db.findById(session, userId);
    if (user == null) return;
    
    final presenceMessage = ChatStreamMessage(
      type: ChatMessageType.presence,
      senderId: userId,
      senderName: user.displayName,
      presenceStatus: status,
      timestamp: DateTime.now(),
    );
    
    // Broadcast to each conversation
    for (final participation in participations) {
      final sessionIds = _connectionManager.getConversationSessions(
        participation.conversationId,
      );
      
      for (final sessionId in sessionIds) {
        // Don't send to self
        final conn = _connectionManager.getConnection(sessionId);
        if (conn?.userId == userId) continue;
        
        try {
          if (session is StreamingSession) {
            await session.messages.postMessage(sessionId, presenceMessage);
          }
        } catch (e) {
          // Ignore broadcast errors
        }
      }
    }
  }

  /// Send missed messages since last connection
  Future<void> _sendMissedMessages(
    StreamingSession session,
    int userId,
    List<ConversationParticipant> participations,
  ) async {
    for (final participation in participations) {
      if (participation.lastReadMessageId == null) continue;
      
      // Get messages after last read
      final missedMessages = await Message.db.find(
        session,
        where: (t) => t.conversationId.equals(participation.conversationId) &
                      t.id.greaterThan(participation.lastReadMessageId!) &
                      t.isDeleted.equals(false),
        orderBy: (t) => t.createdAt,
        limit: 100, // Limit catchup messages
      );
      
      for (final message in missedMessages) {
        final sender = await UserProfile.db.findById(
          session,
          message.senderId,
        );
        
        await sendStreamMessage(
          session,
          ChatStreamMessage(
            type: ChatMessageType.message,
            conversationId: message.conversationId,
            messageId: message.id,
            senderId: message.senderId,
            senderName: sender?.displayName,
            content: message.content,
            messageType: message.messageType,
            timestamp: message.createdAt,
            clientMessageId: message.clientMessageId,
            isCatchup: true, // Flag as catchup message
          ),
        );
      }
    }
  }

  // Helper methods
  
  Future<void> _updateLastSeen(Session session, int userId) async {
    final user = await UserProfile.db.findById(session, userId);
    if (user != null) {
      await UserProfile.db.updateRow(
        session,
        user.copyWith(lastSeenAt: DateTime.now()),
      );
    }
  }
  
  Future<void> _updateConversationLastMessage(
    Session session,
    int conversationId,
    Message message,
    UserProfile sender,
  ) async {
    final conversation = await Conversation.db.findById(
      session,
      conversationId,
    );
    
    if (conversation != null) {
      await Conversation.db.updateRow(
        session,
        conversation.copyWith(
          lastMessageAt: message.createdAt,
          lastMessagePreview: message.content.length > 100
              ? '${message.content.substring(0, 97)}...'
              : message.content,
          lastMessageSenderId: sender.id,
          updatedAt: DateTime.now(),
        ),
      );
    }
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
  
  Future<void> _updateMessageStatuses(
    Session session,
    int conversationId,
    int userId,
    int upToMessageId,
  ) async {
    // Mark all messages up to this one as read by this user
    final messages = await Message.db.find(
      session,
      where: (t) => t.conversationId.equals(conversationId) &
                    t.id.lessOrEqualThan(upToMessageId) &
                    t.senderId.notEquals(userId),
    );
    
    final now = DateTime.now();
    for (final message in messages) {
      // Upsert message status
      final existing = await MessageStatus.db.findFirstRow(
        session,
        where: (t) => t.messageId.equals(message.id!) & 
                      t.userId.equals(userId),
      );
      
      if (existing == null) {
        await MessageStatus.db.insertRow(
          session,
          MessageStatus(
            messageId: message.id!,
            userId: userId,
            deliveredAt: now,
            readAt: now,
          ),
        );
      } else if (existing.readAt == null) {
        await MessageStatus.db.updateRow(
          session,
          existing.copyWith(readAt: now),
        );
      }
    }
  }
}

// Connection Manager for tracking online users

class ConnectionManager {
  final Map<String, UserConnection> _connections = {};
  final Map<int, Set<String>> _conversationSessions = {};
  
  void registerConnection({
    required String sessionId,
    required int userId,
  }) {
    _connections[sessionId] = UserConnection(
      sessionId: sessionId,
      userId: userId,
      connectedAt: DateTime.now(),
      status: PresenceStatus.online,
    );
  }
  
  void removeConnection(String sessionId) {
    final connection = _connections.remove(sessionId);
    if (connection != null) {
      // Remove from all conversation subscriptions
      for (final sessions in _conversationSessions.values) {
        sessions.remove(sessionId);
      }
    }
  }
  
  UserConnection? getConnection(String sessionId) {
    return _connections[sessionId];
  }
  
  void subscribeToConversation({
    required String sessionId,
    required int conversationId,
  }) {
    _conversationSessions
        .putIfAbsent(conversationId, () => {})
        .add(sessionId);
  }
  
  void unsubscribeFromConversation({
    required String sessionId,
    required int conversationId,
  }) {
    _conversationSessions[conversationId]?.remove(sessionId);
  }
  
  Set<String> getConversationSessions(int conversationId) {
    return _conversationSessions[conversationId] ?? {};
  }
  
  void updatePresence({
    required String sessionId,
    required PresenceStatus status,
  }) {
    final connection = _connections[sessionId];
    if (connection != null) {
      _connections[sessionId] = UserConnection(
        sessionId: connection.sessionId,
        userId: connection.userId,
        connectedAt: connection.connectedAt,
        status: status,
      );
    }
  }
  
  bool isUserOnline(int userId) {
    return _connections.values.any(
      (c) => c.userId == userId && c.status == PresenceStatus.online,
    );
  }
  
  List<int> getOnlineUsers(int conversationId) {
    final sessionIds = _conversationSessions[conversationId] ?? {};
    return sessionIds
        .map((sid) => _connections[sid]?.userId)
        .whereType<int>()
        .toList();
  }
}

class UserConnection {
  final String sessionId;
  final int userId;
  final DateTime connectedAt;
  final PresenceStatus status;
  
  UserConnection({
    required this.sessionId,
    required this.userId,
    required this.connectedAt,
    required this.status,
  });
}

enum PresenceStatus { online, away, busy, offline }
```
