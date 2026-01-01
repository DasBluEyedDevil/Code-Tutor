---
type: "EXAMPLE"
title: "Broadcasting to Multiple Clients"
---

A common requirement is sending a message to all connected clients, or all clients in a specific group. Here is how to implement efficient broadcasting.



```dart
// File: lib/src/endpoints/broadcast_endpoint.dart

import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';

/// Manages global message broadcasting.
class BroadcastManager {
  // Singleton instance
  static final BroadcastManager instance = BroadcastManager._();
  BroadcastManager._();
  
  // All connected streaming sessions
  final Set<StreamingSession> _allSessions = {};
  
  // Sessions grouped by topic/channel
  final Map<String, Set<StreamingSession>> _topicSubscribers = {};
  
  // Sessions grouped by user ID (for targeted messages)
  final Map<int, Set<StreamingSession>> _userSessions = {};
  
  /// Register a new session.
  void addSession(StreamingSession session, {int? userId}) {
    _allSessions.add(session);
    if (userId != null) {
      _userSessions[userId] ??= {};
      _userSessions[userId]!.add(session);
    }
  }
  
  /// Remove a session (call on disconnect).
  void removeSession(StreamingSession session, {int? userId}) {
    _allSessions.remove(session);
    if (userId != null) {
      _userSessions[userId]?.remove(session);
    }
    // Remove from all topics
    for (final subscribers in _topicSubscribers.values) {
      subscribers.remove(session);
    }
  }
  
  /// Subscribe session to a topic.
  void subscribe(StreamingSession session, String topic) {
    _topicSubscribers[topic] ??= {};
    _topicSubscribers[topic]!.add(session);
  }
  
  /// Unsubscribe session from a topic.
  void unsubscribe(StreamingSession session, String topic) {
    _topicSubscribers[topic]?.remove(session);
  }
  
  /// Broadcast to ALL connected clients.
  void broadcastToAll(SerializableModel message) {
    for (final session in _allSessions) {
      _safeSend(session, message);
    }
  }
  
  /// Broadcast to all subscribers of a topic.
  void broadcastToTopic(String topic, SerializableModel message) {
    final subscribers = _topicSubscribers[topic];
    if (subscribers == null) return;
    
    for (final session in subscribers) {
      _safeSend(session, message);
    }
  }
  
  /// Send to a specific user (all their sessions/devices).
  void sendToUser(int userId, SerializableModel message) {
    final sessions = _userSessions[userId];
    if (sessions == null) return;
    
    for (final session in sessions) {
      _safeSend(session, message);
    }
  }
  
  /// Broadcast to all EXCEPT the sender.
  void broadcastToOthers(
    StreamingSession sender,
    String topic,
    SerializableModel message,
  ) {
    final subscribers = _topicSubscribers[topic];
    if (subscribers == null) return;
    
    for (final session in subscribers) {
      if (session != sender) {
        _safeSend(session, message);
      }
    }
  }
  
  /// Safe send that handles disconnected sessions.
  void _safeSend(StreamingSession session, SerializableModel message) {
    try {
      session.sendStreamMessage(message);
    } catch (e) {
      // Session might be disconnected, remove it
      removeSession(session);
    }
  }
  
  /// Get count of connected clients.
  int get connectedCount => _allSessions.length;
  
  /// Get count of subscribers to a topic.
  int topicSubscriberCount(String topic) {
    return _topicSubscribers[topic]?.length ?? 0;
  }
}

// Usage in your endpoint:
class NotificationEndpoint extends Endpoint {
  final _broadcast = BroadcastManager.instance;
  
  @override
  Future<void> streamOpened(StreamingSession session) async {
    final userId = await session.auth.authenticatedUserId;
    _broadcast.addSession(session, userId: userId);
  }
  
  @override
  Future<void> streamClosed(StreamingSession session) async {
    final userId = await session.auth.authenticatedUserId;
    _broadcast.removeSession(session, userId: userId);
  }
  
  /// HTTP endpoint to send notification (called from server-side code)
  Future<void> notifyAllUsers(Session session, SystemNotification notification) async {
    _broadcast.broadcastToAll(notification);
  }
  
  /// Send notification to specific user
  Future<void> notifyUser(Session session, int userId, UserNotification notification) async {
    _broadcast.sendToUser(userId, notification);
  }
}
```
