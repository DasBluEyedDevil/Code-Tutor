// Protocol definition (notification.yaml):
// class: Notification
// fields:
//   id: int?
//   userId: int
//   type: String
//   title: String
//   body: String
//   isRead: bool
//   createdAt: DateTime

import 'package:serverpod/serverpod.dart';
// import '../generated/protocol.dart';

// Placeholder classes for demonstration
class Notification {
  final int? id;
  final int userId;
  final String type;
  final String title;
  final String body;
  final bool isRead;
  final DateTime createdAt;
  
  Notification({
    this.id,
    required this.userId,
    required this.type,
    required this.title,
    required this.body,
    required this.isRead,
    required this.createdAt,
  });
  
  @override
  String toString() => 'Notification($type: $title)';
}

class StreamingSession {
  void sendStreamMessage(Object message) {
    print('Sending: $message');
  }
}

/// Manages notification delivery to connected clients.
class NotificationManager {
  // Singleton pattern for global access
  static final NotificationManager instance = NotificationManager._internal();
  NotificationManager._internal();
  factory NotificationManager() => instance;
  
  // Map of userId to their connected sessions
  // Using Set because one user can have multiple devices
  final Map<int, Set<StreamingSession>> _userSessions = {};
  
  /// Register a new streaming session for a user.
  void addSession(int userId, StreamingSession session) {
    // Create the set if it doesn't exist for this user
    _userSessions[userId] ??= {};
    // Add the session to the user's set
    _userSessions[userId]!.add(session);
  }
  
  /// Remove a session when user disconnects.
  void removeSession(int userId, StreamingSession session) {
    final sessions = _userSessions[userId];
    if (sessions != null) {
      sessions.remove(session);
      // Clean up empty sets to prevent memory leaks
      if (sessions.isEmpty) {
        _userSessions.remove(userId);
      }
    }
  }
  
  /// Send notification to a specific user (all their devices).
  void sendToUser(int userId, Notification notification) {
    final sessions = _userSessions[userId];
    if (sessions == null || sessions.isEmpty) {
      // User is not online - could queue for later or send push notification
      return;
    }
    
    // Send to all sessions (devices) for this user
    // Use toList() to avoid concurrent modification if sending fails
    for (final session in sessions.toList()) {
      try {
        session.sendStreamMessage(notification);
      } catch (e) {
        // Session might be disconnected, remove it
        print('Error sending to session, removing: $e');
        sessions.remove(session);
      }
    }
  }
  
  /// Broadcast notification to ALL connected users.
  void broadcastToAll(Notification notification) {
    // Iterate over all users
    for (final entry in _userSessions.entries) {
      final userId = entry.key;
      final sessions = entry.value;
      
      // Send to all sessions for each user
      for (final session in sessions.toList()) {
        try {
          session.sendStreamMessage(notification);
        } catch (e) {
          print('Error broadcasting to user $userId: $e');
          sessions.remove(session);
        }
      }
    }
  }
  
  /// Check if a specific user is online.
  bool isUserOnline(int userId) {
    final sessions = _userSessions[userId];
    // User is online if they have at least one connected session
    return sessions != null && sessions.isNotEmpty;
  }
  
  /// Get total count of connected users.
  int get onlineUserCount {
    // Count entries that have at least one session
    return _userSessions.entries
        .where((entry) => entry.value.isNotEmpty)
        .length;
  }
  
  /// Get total count of connected sessions (all devices).
  int get totalSessionCount {
    // Sum up all sessions across all users
    return _userSessions.values
        .fold(0, (sum, sessions) => sum + sessions.length);
  }
}

// Test your implementation
void main() {
  final manager = NotificationManager();
  
  // Simulate connections
  final session1 = StreamingSession();
  final session2 = StreamingSession();
  final session3 = StreamingSession();
  
  manager.addSession(1, session1); // User 1, device 1
  manager.addSession(1, session2); // User 1, device 2 (multiple devices!)
  manager.addSession(2, session3); // User 2
  
  print('Online users: ${manager.onlineUserCount}'); // Should be 2
  print('Total sessions: ${manager.totalSessionCount}'); // Should be 3
  print('User 1 online: ${manager.isUserOnline(1)}'); // Should be true
  print('User 3 online: ${manager.isUserOnline(3)}'); // Should be false
  
  // Send notification to user 1 (both devices should receive)
  final notification = Notification(
    userId: 1,
    type: 'test',
    title: 'Hello',
    body: 'This is a test notification',
    isRead: false,
    createdAt: DateTime.now(),
  );
  
  print('\nSending to user 1:');
  manager.sendToUser(1, notification);
  
  print('\nBroadcasting to all:');
  manager.broadcastToAll(notification);
  
  // Disconnect user 1's first device
  manager.removeSession(1, session1);
  print('\nAfter disconnect:');
  print('User 1 online: ${manager.isUserOnline(1)}'); // Still true (has session2)
  print('Total sessions: ${manager.totalSessionCount}'); // Should be 2
}