---
type: "EXAMPLE"
title: "Typing Indicators and Presence"
---


**Real-Time Typing and Presence System**

Typing indicators and presence information make chat feel alive. Users can see when others are typing and who's currently online. These features require efficient broadcasting with minimal network overhead.



```dart
// server/lib/src/protocol/chat_stream_message.yaml
class: ChatStreamMessage
fields:
  # Message type
  type: ChatMessageType
  
  # Conversation context
  conversationId: int?
  
  # Message data
  messageId: int?
  senderId: int?
  senderName: String?
  senderAvatarUrl: String?
  content: String?
  messageType: String?  # 'text', 'image', etc.
  replyToMessageId: int?
  mediaUrl: String?
  timestamp: DateTime?
  clientMessageId: String?
  
  # Typing indicator
  isTyping: bool?
  
  # Presence data
  presenceStatus: PresenceStatus?
  
  # Catchup flag (for missed messages)
  isCatchup: bool?

---

# server/lib/src/protocol/enums/chat_message_type.yaml
enum: ChatMessageType
values:
  - message     # Regular chat message
  - typing      # Typing indicator
  - read        # Read receipt
  - presence    # Online/offline status
  - delivered   # Delivery confirmation
  - reaction    # Emoji reaction

---

// server/lib/src/services/typing_indicator_service.dart
import 'dart:async';

/// Service for managing typing indicators with debouncing and timeouts
class TypingIndicatorService {
  /// Map of conversation -> user -> typing timer
  final Map<int, Map<int, Timer>> _typingTimers = {};
  
  /// How long until typing indicator auto-expires
  static const Duration typingTimeout = Duration(seconds: 5);
  
  /// Callback when typing status changes
  final void Function(int conversationId, int userId, bool isTyping)? onTypingChanged;
  
  TypingIndicatorService({this.onTypingChanged});
  
  /// User started or continued typing
  void userTyping(int conversationId, int userId) {
    // Cancel existing timer if any
    _typingTimers[conversationId]?[userId]?.cancel();
    
    // Initialize maps if needed
    _typingTimers.putIfAbsent(conversationId, () => {});
    
    // Check if this is a new typing session
    final wasTyping = _typingTimers[conversationId]!.containsKey(userId);
    
    // Set new timeout
    _typingTimers[conversationId]![userId] = Timer(
      typingTimeout,
      () => _typingExpired(conversationId, userId),
    );
    
    // Notify if this is a new typing session
    if (!wasTyping) {
      onTypingChanged?.call(conversationId, userId, true);
    }
  }
  
  /// User stopped typing (sent message or cleared input)
  void userStoppedTyping(int conversationId, int userId) {
    final timer = _typingTimers[conversationId]?.remove(userId);
    if (timer != null) {
      timer.cancel();
      onTypingChanged?.call(conversationId, userId, false);
    }
  }
  
  /// Called when typing timeout expires
  void _typingExpired(int conversationId, int userId) {
    _typingTimers[conversationId]?.remove(userId);
    onTypingChanged?.call(conversationId, userId, false);
  }
  
  /// Get list of users currently typing in a conversation
  List<int> getTypingUsers(int conversationId) {
    return _typingTimers[conversationId]?.keys.toList() ?? [];
  }
  
  /// Check if specific user is typing
  bool isUserTyping(int conversationId, int userId) {
    return _typingTimers[conversationId]?.containsKey(userId) ?? false;
  }
  
  /// Clean up when user disconnects
  void userDisconnected(int userId) {
    for (final conversationTimers in _typingTimers.values) {
      final timer = conversationTimers.remove(userId);
      timer?.cancel();
    }
  }
  
  /// Clean up all timers
  void dispose() {
    for (final conversationTimers in _typingTimers.values) {
      for (final timer in conversationTimers.values) {
        timer.cancel();
      }
    }
    _typingTimers.clear();
  }
}

---

// server/lib/src/services/presence_service.dart
import 'dart:async';

/// Service for tracking user online/offline presence
class PresenceService {
  /// User ID -> presence info
  final Map<int, UserPresence> _presences = {};
  
  /// Callback when presence changes
  final void Function(int userId, PresenceStatus status, DateTime? lastSeen)? 
      onPresenceChanged;
  
  /// Timer for checking stale presences
  Timer? _cleanupTimer;
  
  /// How long before considering a user offline without heartbeat
  static const Duration presenceTimeout = Duration(minutes: 2);
  
  /// Heartbeat interval expected from clients
  static const Duration heartbeatInterval = Duration(seconds: 30);
  
  PresenceService({this.onPresenceChanged}) {
    // Start cleanup timer
    _cleanupTimer = Timer.periodic(
      Duration(seconds: 30),
      (_) => _cleanupStalePresences(),
    );
  }
  
  /// Update user presence (called on connect and heartbeat)
  void updatePresence(
    int userId,
    PresenceStatus status, {
    String? deviceInfo,
  }) {
    final previous = _presences[userId];
    final now = DateTime.now();
    
    _presences[userId] = UserPresence(
      userId: userId,
      status: status,
      lastHeartbeat: now,
      lastSeen: now,
      deviceInfo: deviceInfo,
    );
    
    // Notify if status changed
    if (previous?.status != status) {
      onPresenceChanged?.call(userId, status, now);
    }
  }
  
  /// Record heartbeat to keep presence alive
  void heartbeat(int userId) {
    final presence = _presences[userId];
    if (presence != null) {
      _presences[userId] = UserPresence(
        userId: userId,
        status: presence.status,
        lastHeartbeat: DateTime.now(),
        lastSeen: DateTime.now(),
        deviceInfo: presence.deviceInfo,
      );
    }
  }
  
  /// Mark user as offline (called on disconnect)
  void userOffline(int userId) {
    final presence = _presences[userId];
    if (presence != null && presence.status != PresenceStatus.offline) {
      final now = DateTime.now();
      _presences[userId] = UserPresence(
        userId: userId,
        status: PresenceStatus.offline,
        lastHeartbeat: now,
        lastSeen: now,
        deviceInfo: presence.deviceInfo,
      );
      onPresenceChanged?.call(userId, PresenceStatus.offline, now);
    }
  }
  
  /// Get current presence for a user
  UserPresence? getPresence(int userId) {
    return _presences[userId];
  }
  
  /// Check if user is online
  bool isOnline(int userId) {
    final presence = _presences[userId];
    if (presence == null) return false;
    return presence.status == PresenceStatus.online ||
           presence.status == PresenceStatus.away ||
           presence.status == PresenceStatus.busy;
  }
  
  /// Get presences for multiple users (for conversation participant list)
  Map<int, UserPresence> getPresences(List<int> userIds) {
    final result = <int, UserPresence>{};
    for (final userId in userIds) {
      final presence = _presences[userId];
      if (presence != null) {
        result[userId] = presence;
      }
    }
    return result;
  }
  
  /// Get last seen time for a user
  DateTime? getLastSeen(int userId) {
    return _presences[userId]?.lastSeen;
  }
  
  /// Clean up presences that haven't sent heartbeat
  void _cleanupStalePresences() {
    final now = DateTime.now();
    final staleThreshold = now.subtract(presenceTimeout);
    
    final staleUsers = <int>[];
    
    for (final entry in _presences.entries) {
      if (entry.value.status != PresenceStatus.offline &&
          entry.value.lastHeartbeat.isBefore(staleThreshold)) {
        staleUsers.add(entry.key);
      }
    }
    
    for (final userId in staleUsers) {
      userOffline(userId);
    }
  }
  
  /// Clean up on shutdown
  void dispose() {
    _cleanupTimer?.cancel();
    _presences.clear();
  }
}

/// User presence information
class UserPresence {
  final int userId;
  final PresenceStatus status;
  final DateTime lastHeartbeat;
  final DateTime lastSeen;
  final String? deviceInfo;
  
  UserPresence({
    required this.userId,
    required this.status,
    required this.lastHeartbeat,
    required this.lastSeen,
    this.deviceInfo,
  });
  
  /// Format last seen for display
  String get lastSeenDisplay {
    if (status != PresenceStatus.offline) {
      return 'Online';
    }
    
    final now = DateTime.now();
    final diff = now.difference(lastSeen);
    
    if (diff.inMinutes < 1) {
      return 'Just now';
    } else if (diff.inMinutes < 60) {
      return '${diff.inMinutes}m ago';
    } else if (diff.inHours < 24) {
      return '${diff.inHours}h ago';
    } else if (diff.inDays < 7) {
      return '${diff.inDays}d ago';
    } else {
      return 'Over a week ago';
    }
  }
}

enum PresenceStatus { online, away, busy, offline }
```
