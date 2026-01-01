---
type: "EXAMPLE"
title: "Implementing Real-Time Notifications"
---

Here is a complete notification system with both streaming and persistence.



```dart
// File: lib/src/endpoints/notification_endpoint.dart

import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';

class NotificationEndpoint extends Endpoint {
  // Track user sessions for real-time delivery
  static final Map<int, Set<StreamingSession>> _userConnections = {};
  
  @override
  Future<void> streamOpened(StreamingSession session) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      session.close(); // Require authentication
      return;
    }
    
    _userConnections[userId] ??= {};
    _userConnections[userId]!.add(session);
    
    // Send any unread notifications on connect
    final unread = await Notification.db.find(
      session,
      where: (t) => t.userId.equals(userId) & t.isRead.equals(false),
      orderBy: (t) => t.createdAt,
      orderDescending: true,
      limit: 50,
    );
    
    for (final notification in unread) {
      session.sendStreamMessage(notification);
    }
  }
  
  @override
  Future<void> streamClosed(StreamingSession session) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId != null) {
      _userConnections[userId]?.remove(session);
    }
  }
  
  @override
  Future<void> handleStreamMessage(
    StreamingSession session,
    SerializableModel message,
  ) async {
    if (message is MarkNotificationRead) {
      await _markAsRead(session, message.notificationId);
    } else if (message is MarkAllRead) {
      await _markAllAsRead(session);
    }
  }
  
  /// Create and deliver a notification.
  /// Call this from other endpoints when events occur.
  static Future<void> sendNotification(
    Session session, {
    required int userId,
    required String type,
    required String title,
    required String body,
    Map<String, String>? data,
  }) async {
    // 1. Create and persist the notification
    final notification = Notification(
      userId: userId,
      type: type,
      title: title,
      body: body,
      data: data,
      isRead: false,
      createdAt: DateTime.now(),
    );
    
    final saved = await Notification.db.insertRow(session, notification);
    
    // 2. Deliver via stream if user is connected
    final sessions = _userConnections[userId];
    if (sessions != null && sessions.isNotEmpty) {
      for (final userSession in sessions) {
        try {
          userSession.sendStreamMessage(saved);
        } catch (e) {
          // Session might be stale, remove it
          sessions.remove(userSession);
        }
      }
    }
    
    // 3. Optionally send push notification if not connected
    // if (sessions == null || sessions.isEmpty) {
    //   await PushNotificationService.send(userId, title, body);
    // }
  }
  
  Future<void> _markAsRead(StreamingSession session, int notificationId) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) return;
    
    await session.db.query(
      'UPDATE notification SET is_read = true WHERE id = @id AND user_id = @userId',
      parameters: {'id': notificationId, 'userId': userId},
    );
  }
  
  Future<void> _markAllAsRead(StreamingSession session) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) return;
    
    await session.db.query(
      'UPDATE notification SET is_read = true WHERE user_id = @userId',
      parameters: {'userId': userId},
    );
  }
  
  /// Check if a user is currently connected.
  static bool isUserOnline(int userId) {
    final sessions = _userConnections[userId];
    return sessions != null && sessions.isNotEmpty;
  }
  
  /// Get count of online users.
  static int get onlineUserCount {
    return _userConnections.entries
        .where((e) => e.value.isNotEmpty)
        .length;
  }
}

// Example: Using notifications from another endpoint
class FollowEndpoint extends Endpoint {
  Future<bool> followUser(Session session, int targetUserId) async {
    final currentUserId = await session.auth.authenticatedUserId;
    if (currentUserId == null) throw UnauthorizedException();
    
    // ... create follow relationship ...
    
    // Send notification to the followed user
    await NotificationEndpoint.sendNotification(
      session,
      userId: targetUserId,
      type: 'new_follower',
      title: 'New Follower',
      body: 'Someone started following you!',
      data: {'followerId': currentUserId.toString()},
    );
    
    return true;
  }
}
```
