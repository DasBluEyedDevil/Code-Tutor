---
type: "THEORY"
title: "Online/Offline Status"
---

The foundation of presence is knowing when users connect and disconnect. Serverpod provides connection lifecycle hooks that let you track this automatically.

**Connection Tracking Strategy:**

1. When a user connects to a stream, mark them as online
2. When the connection closes, mark them as offline
3. Use heartbeats to detect stale connections
4. Store presence in a fast-access cache (Redis or in-memory)

**Heartbeat Mechanism:**

Network connections can fail silently. A heartbeat system periodically pings clients to ensure they're still alive. If a client misses several heartbeats, consider them offline.

**Server-Side Implementation:**

The server tracks each user's connection status and last activity time.



```dart
// presence_manager.dart (Server)
class PresenceManager {
  // In-memory store for demo; use Redis in production
  static final Map<int, UserPresence> _presenceMap = {};
  static final Map<int, Set<StreamingSession>> _userSessions = {};
  
  // Called when user connects to any stream
  static Future<void> userConnected(
    Session session,
    int userId,
    StreamingSession streamingSession,
  ) async {
    // Track this session for the user
    _userSessions.putIfAbsent(userId, () => {});
    _userSessions[userId]!.add(streamingSession);
    
    // Update presence
    final wasOffline = !_presenceMap.containsKey(userId) ||
        _presenceMap[userId]!.status == PresenceStatus.offline;
    
    _presenceMap[userId] = UserPresence(
      userId: userId,
      status: PresenceStatus.online,
      lastSeen: DateTime.now(),
    );
    
    // Broadcast status change if they were offline
    if (wasOffline) {
      await _broadcastPresenceChange(session, userId, PresenceStatus.online);
    }
  }
  
  // Called when user disconnects
  static Future<void> userDisconnected(
    Session session,
    int userId,
    StreamingSession streamingSession,
  ) async {
    // Remove this session
    _userSessions[userId]?.remove(streamingSession);
    
    // Only mark offline if no remaining sessions
    if (_userSessions[userId]?.isEmpty ?? true) {
      _presenceMap[userId] = UserPresence(
        userId: userId,
        status: PresenceStatus.offline,
        lastSeen: DateTime.now(),
      );
      
      await _broadcastPresenceChange(session, userId, PresenceStatus.offline);
    }
  }
  
  // Get current presence for a user
  static UserPresence? getPresence(int userId) {
    return _presenceMap[userId];
  }
  
  // Get presence for multiple users
  static Map<int, UserPresence> getPresenceForUsers(List<int> userIds) {
    return Map.fromEntries(
      userIds
          .where((id) => _presenceMap.containsKey(id))
          .map((id) => MapEntry(id, _presenceMap[id]!)),
    );
  }
}

// Models
enum PresenceStatus { online, away, offline }

class UserPresence {
  final int userId;
  final PresenceStatus status;
  final DateTime lastSeen;
  
  UserPresence({
    required this.userId,
    required this.status,
    required this.lastSeen,
  });
}
```
