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
}

class StreamingSession {
  void sendStreamMessage(Object message) {
    print('Sending: $message');
  }
}

/// Manages notification delivery to connected clients.
class NotificationManager {
  // TODO: Create a singleton instance
  
  // TODO: Create a map to track sessions by userId
  // Remember: one user can have multiple devices connected!
  
  /// Register a new streaming session for a user.
  void addSession(int userId, StreamingSession session) {
    // TODO: Add session to the user's set of sessions
    // Create the set if it doesn't exist
  }
  
  /// Remove a session when user disconnects.
  void removeSession(int userId, StreamingSession session) {
    // TODO: Remove session from user's set
    // Clean up empty sets
  }
  
  /// Send notification to a specific user (all their devices).
  void sendToUser(int userId, Notification notification) {
    // TODO: Get all sessions for this user
    // TODO: Send notification to each session
    // TODO: Handle errors (session might be disconnected)
  }
  
  /// Broadcast notification to ALL connected users.
  void broadcastToAll(Notification notification) {
    // TODO: Iterate over all users and their sessions
    // TODO: Send notification to each session
  }
  
  /// Check if a specific user is online.
  bool isUserOnline(int userId) {
    // TODO: Return true if user has at least one connected session
    return false;
  }
  
  /// Get total count of connected users.
  int get onlineUserCount {
    // TODO: Count users with at least one session
    return 0;
  }
  
  /// Get total count of connected sessions (all devices).
  int get totalSessionCount {
    // TODO: Sum up all sessions across all users
    return 0;
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