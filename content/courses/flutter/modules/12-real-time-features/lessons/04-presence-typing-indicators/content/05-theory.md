---
type: "THEORY"
title: "Efficient State Management"
---

Presence and typing systems can generate many updates. Without careful management, you'll flood both the network and the UI with unnecessary refreshes.

**Batching Updates:**

Collect multiple presence changes and send them together, rather than one at a time.

**Rate Limiting:**

Limit how frequently updates can be sent to prevent abuse and reduce load.

**Smart Diffing:**

Only notify the UI when state actually changes.



```dart
// presence_state_manager.dart (Client)
class PresenceStateManager extends ChangeNotifier {
  final Map<int, UserPresence> _presenceMap = {};
  final Map<String, List<TypingUser>> _typingMap = {};
  
  // Batch update timer
  Timer? _batchTimer;
  final List<PresenceUpdate> _pendingUpdates = [];
  
  // Rate limiting
  DateTime? _lastNotification;
  static const _minNotificationInterval = Duration(milliseconds: 100);
  
  // Get presence for a user
  PresenceStatus getStatus(int userId) {
    return _presenceMap[userId]?.status ?? PresenceStatus.offline;
  }
  
  // Get typing users for a conversation
  List<TypingUser> getTypingUsers(String conversationId) {
    return _typingMap[conversationId] ?? [];
  }
  
  // Handle incoming presence update
  void handlePresenceUpdate(PresenceUpdate update) {
    _pendingUpdates.add(update);
    
    // Batch updates that arrive within 50ms
    _batchTimer?.cancel();
    _batchTimer = Timer(Duration(milliseconds: 50), _processBatch);
  }
  
  void _processBatch() {
    if (_pendingUpdates.isEmpty) return;
    
    bool hasChanges = false;
    
    for (final update in _pendingUpdates) {
      final current = _presenceMap[update.userId];
      
      // Only update if status actually changed
      if (current?.status != update.status) {
        _presenceMap[update.userId] = UserPresence(
          userId: update.userId,
          status: update.status,
          lastSeen: update.lastSeen,
        );
        hasChanges = true;
      }
    }
    
    _pendingUpdates.clear();
    
    // Rate limit notifications
    if (hasChanges) {
      _throttledNotify();
    }
  }
  
  void _throttledNotify() {
    final now = DateTime.now();
    if (_lastNotification != null) {
      final elapsed = now.difference(_lastNotification!);
      if (elapsed < _minNotificationInterval) {
        // Schedule notification after remaining time
        Timer(
          _minNotificationInterval - elapsed,
          () {
            _lastNotification = DateTime.now();
            notifyListeners();
          },
        );
        return;
      }
    }
    
    _lastNotification = now;
    notifyListeners();
  }
  
  // Handle typing update
  void handleTypingUpdate(TypingUpdate update) {
    final conversationId = update.conversationId;
    final current = _typingMap[conversationId] ?? [];
    
    if (update.isTyping) {
      // Add user if not already in list
      if (!current.any((u) => u.id == update.userId)) {
        _typingMap[conversationId] = [
          ...current,
          TypingUser(
            id: update.userId,
            name: update.userName,
            avatarUrl: update.avatarUrl,
          ),
        ];
        _throttledNotify();
      }
    } else {
      // Remove user from list
      final newList = current.where((u) => u.id != update.userId).toList();
      if (newList.length != current.length) {
        _typingMap[conversationId] = newList;
        _throttledNotify();
      }
    }
  }
  
  @override
  void dispose() {
    _batchTimer?.cancel();
    super.dispose();
  }
}
```
