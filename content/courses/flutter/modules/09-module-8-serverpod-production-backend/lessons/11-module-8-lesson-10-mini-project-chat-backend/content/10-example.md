---
type: "EXAMPLE"
title: "Step 8: Implement ChatStreamEndpoint (Real-Time Core)"
---

The ChatStreamEndpoint is the heart of real-time functionality. It manages WebSocket connections, message broadcasting, typing indicators, and presence.



```dart
// File: lib/src/endpoints/chat_stream_endpoint.dart

import 'dart:async';
import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';

/// Manages real-time chat streaming.
class ChatStreamEndpoint extends Endpoint {
  // Global state for connected sessions
  // In production, consider using Redis for multi-server support
  
  // Map of roomId -> Set of sessions subscribed to that room
  static final Map<int, Set<StreamingSession>> _roomSubscribers = {};
  
  // Map of session -> user info (for quick lookup)
  static final Map<StreamingSession, _ConnectedUser> _sessionUsers = {};
  
  // Map of session -> Set of room IDs they're subscribed to
  static final Map<StreamingSession, Set<int>> _sessionRooms = {};
  
  // Typing indicator timers (auto-clear after timeout)
  static final Map<String, Timer> _typingTimers = {}; // Key: "roomId:userId"
  
  // Track which users are typing in which rooms
  static final Map<int, Set<int>> _roomTypingUsers = {}; // roomId -> Set<userId>
  
  @override
  Future<void> streamOpened(StreamingSession session) async {
    session.log('Chat stream opened');
    
    // Get authenticated user
    final authUserId = await session.auth.authenticatedUserId;
    if (authUserId == null) {
      session.log('Unauthenticated connection attempt - closing');
      session.close();
      return;
    }
    
    // Get chat user profile
    final chatUser = await ChatUser.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(authUserId),
    );
    
    if (chatUser == null) {
      session.log('User profile not found - closing');
      session.close();
      return;
    }
    
    // Store session info
    _sessionUsers[session] = _ConnectedUser(
      userId: chatUser.id!,
      username: chatUser.username,
      displayName: chatUser.displayName ?? chatUser.username,
    );
    _sessionRooms[session] = {};
    
    // Update user online status
    final updatedUser = chatUser.copyWith(
      isOnline: true,
      lastSeenAt: DateTime.now(),
    );
    await ChatUser.db.updateRow(session, updatedUser);
    
    // Auto-subscribe to all user's rooms
    final memberships = await ChatMember.db.find(
      session,
      where: (t) => t.userId.equals(chatUser.id!),
    );
    
    for (final membership in memberships) {
      _subscribeToRoom(session, membership.chatRoomId);
    }
    
    session.log('User ${chatUser.username} connected to ${memberships.length} rooms');
  }
  
  @override
  Future<void> handleStreamMessage(
    StreamingSession session,
    SerializableModel message,
  ) async {
    final user = _sessionUsers[session];
    if (user == null) {
      session.log('Message from unknown session - ignoring');
      return;
    }
    
    if (message is TypingIndicator) {
      await _handleTypingIndicator(session, message, user);
    } else if (message is ChatRoomSubscription) {
      _handleRoomSubscription(session, message);
    }
    // ChatMessages are sent via HTTP endpoint, then broadcast by us
  }
  
  @override
  Future<void> streamClosed(StreamingSession session) async {
    session.log('Chat stream closed');
    
    final user = _sessionUsers[session];
    if (user != null) {
      // Update user offline status
      // We need a regular session for database access
      // In production, use a background service or scheduled task
      
      // Clear typing indicators for this user
      _clearUserTyping(user.userId);
      
      // Broadcast presence update to all rooms user was in
      final rooms = _sessionRooms[session] ?? {};
      for (final roomId in rooms) {
        _broadcastPresence(roomId, user.userId, false);
        _roomSubscribers[roomId]?.remove(session);
      }
    }
    
    // Cleanup session data
    _sessionUsers.remove(session);
    _sessionRooms.remove(session);
  }
  
  /// Subscribe a session to a room.
  static void _subscribeToRoom(StreamingSession session, int roomId) {
    _roomSubscribers[roomId] ??= {};
    _roomSubscribers[roomId]!.add(session);
    _sessionRooms[session]?.add(roomId);
  }
  
  /// Unsubscribe a session from a room.
  static void _unsubscribeFromRoom(StreamingSession session, int roomId) {
    _roomSubscribers[roomId]?.remove(session);
    _sessionRooms[session]?.remove(roomId);
    
    // Cleanup empty room
    if (_roomSubscribers[roomId]?.isEmpty ?? false) {
      _roomSubscribers.remove(roomId);
    }
  }
  
  /// Handle typing indicator from client.
  Future<void> _handleTypingIndicator(
    StreamingSession session,
    TypingIndicator indicator,
    _ConnectedUser user,
  ) async {
    final roomId = indicator.chatRoomId;
    final timerKey = '$roomId:${user.userId}';
    
    // Cancel existing timer
    _typingTimers[timerKey]?.cancel();
    
    if (indicator.isTyping) {
      // Add to typing users
      _roomTypingUsers[roomId] ??= {};
      final wasTyping = _roomTypingUsers[roomId]!.contains(user.userId);
      _roomTypingUsers[roomId]!.add(user.userId);
      
      // Broadcast if this is a new typing session
      if (!wasTyping) {
        _broadcastTypingIndicator(roomId, user, true);
      }
      
      // Auto-clear after 5 seconds
      _typingTimers[timerKey] = Timer(Duration(seconds: 5), () {
        _roomTypingUsers[roomId]?.remove(user.userId);
        _broadcastTypingIndicator(roomId, user, false);
        _typingTimers.remove(timerKey);
      });
    } else {
      // Immediately stop typing
      _roomTypingUsers[roomId]?.remove(user.userId);
      _broadcastTypingIndicator(roomId, user, false);
      _typingTimers.remove(timerKey);
    }
  }
  
  /// Handle room subscription changes.
  void _handleRoomSubscription(
    StreamingSession session,
    ChatRoomSubscription subscription,
  ) {
    if (subscription.subscribe) {
      _subscribeToRoom(session, subscription.roomId);
    } else {
      _unsubscribeFromRoom(session, subscription.roomId);
    }
  }
  
  /// Broadcast a message to all subscribers of a room.
  /// Called from ChatMessageEndpoint after saving a message.
  static void broadcastMessage(
    int roomId,
    ChatMessage message,
    ChatUser sender,
  ) {
    final subscribers = _roomSubscribers[roomId];
    if (subscribers == null || subscribers.isEmpty) {
      return;
    }
    
    // Create event wrapper
    final event = ChatEvent(
      eventType: 'message',
      chatRoomId: roomId,
      payload: _encodeMessage(message, sender),
      timestamp: DateTime.now(),
    );
    
    // Send to all subscribers
    for (final session in subscribers.toList()) {
      try {
        session.sendStreamMessage(event);
      } catch (e) {
        // Session might be disconnected
        _cleanupSession(session);
      }
    }
  }
  
  /// Broadcast typing indicator to room.
  static void _broadcastTypingIndicator(
    int roomId,
    _ConnectedUser user,
    bool isTyping,
  ) {
    final subscribers = _roomSubscribers[roomId];
    if (subscribers == null || subscribers.isEmpty) {
      return;
    }
    
    final indicator = TypingIndicator(
      chatRoomId: roomId,
      userId: user.userId,
      username: user.displayName,
      isTyping: isTyping,
    );
    
    // Send to all subscribers EXCEPT the typing user
    for (final session in subscribers.toList()) {
      final sessionUser = _sessionUsers[session];
      if (sessionUser?.userId == user.userId) continue;
      
      try {
        session.sendStreamMessage(indicator);
      } catch (e) {
        _cleanupSession(session);
      }
    }
  }
  
  /// Broadcast presence (online/offline) to room.
  static void _broadcastPresence(int roomId, int userId, bool isOnline) {
    final subscribers = _roomSubscribers[roomId];
    if (subscribers == null || subscribers.isEmpty) {
      return;
    }
    
    final event = ChatEvent(
      eventType: 'presence',
      chatRoomId: roomId,
      payload: '{"userId": $userId, "isOnline": $isOnline}',
      timestamp: DateTime.now(),
    );
    
    for (final session in subscribers.toList()) {
      try {
        session.sendStreamMessage(event);
      } catch (e) {
        _cleanupSession(session);
      }
    }
  }
  
  /// Clear all typing indicators for a user.
  static void _clearUserTyping(int userId) {
    for (final entry in _roomTypingUsers.entries) {
      entry.value.remove(userId);
    }
    
    // Cancel any pending timers for this user
    final keysToRemove = _typingTimers.keys
        .where((key) => key.endsWith(':$userId'))
        .toList();
    
    for (final key in keysToRemove) {
      _typingTimers[key]?.cancel();
      _typingTimers.remove(key);
    }
  }
  
  /// Cleanup a disconnected session.
  static void _cleanupSession(StreamingSession session) {
    final rooms = _sessionRooms[session] ?? {};
    for (final roomId in rooms) {
      _roomSubscribers[roomId]?.remove(session);
    }
    _sessionUsers.remove(session);
    _sessionRooms.remove(session);
  }
  
  /// Encode message for transport.
  static String _encodeMessage(ChatMessage message, ChatUser sender) {
    // In production, use proper JSON serialization
    return '''{
      "id": ${message.id},
      "chatRoomId": ${message.chatRoomId},
      "senderId": ${message.senderId},
      "senderName": "${sender.displayName ?? sender.username}",
      "senderAvatar": "${sender.avatarUrl ?? ''}",
      "content": "${message.content}",
      "messageType": "${message.messageType}",
      "attachmentUrl": ${message.attachmentUrl != null ? '"${message.attachmentUrl}"' : 'null'},
      "createdAt": "${message.createdAt.toIso8601String()}"
    }''';
  }
  
  /// Get online users in a room (utility method).
  static List<int> getOnlineUsersInRoom(int roomId) {
    final subscribers = _roomSubscribers[roomId];
    if (subscribers == null) return [];
    
    return subscribers
        .map((s) => _sessionUsers[s]?.userId)
        .whereType<int>()
        .toList();
  }
  
  /// Get count of online connections.
  static int get totalConnections => _sessionUsers.length;
}

/// Internal class to track connected user info.
class _ConnectedUser {
  final int userId;
  final String username;
  final String displayName;
  
  _ConnectedUser({
    required this.userId,
    required this.username,
    required this.displayName,
  });
}

/// Model for room subscription requests.
// Add to protocol/chat_room_subscription.yaml:
// class: ChatRoomSubscription
// fields:
//   roomId: int
//   subscribe: bool
class ChatRoomSubscription {
  final int roomId;
  final bool subscribe;
  
  ChatRoomSubscription({required this.roomId, required this.subscribe});
}
```
