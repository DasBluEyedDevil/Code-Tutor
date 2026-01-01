---
type: "THEORY"
title: "Typing Indicators"
---

Typing indicators show when someone is actively composing a message. They require careful handling to avoid flooding the network with updates while still feeling responsive.

**Key Challenges:**

1. **Debouncing**: Don't send an update on every keystroke
2. **Timeout**: Clear typing state if user stops typing
3. **Batching**: Handle multiple users typing simultaneously
4. **Performance**: Updates should be lightweight

**Debouncing Strategy:**

- Start typing: Send immediately (responsiveness)
- Continue typing: Debounce updates (every 2-3 seconds)
- Stop typing: Send after timeout (5-10 seconds of inactivity)

**Implementation Pattern:**

Track typing state with timers that auto-clear.



```dart
// typing_indicator_service.dart (Client)
class TypingIndicatorService {
  final Client _client;
  final String _conversationId;
  
  Timer? _debounceTimer;
  Timer? _timeoutTimer;
  bool _isTyping = false;
  
  // Debounce interval: how often to send 'still typing' updates
  static const _debounceInterval = Duration(seconds: 3);
  // Timeout: how long before we consider user stopped typing
  static const _typingTimeout = Duration(seconds: 5);
  
  TypingIndicatorService(this._client, this._conversationId);
  
  // Called on every keystroke in the input field
  void onTextChanged(String text) {
    if (text.isEmpty) {
      _stopTyping();
      return;
    }
    
    // Reset the timeout timer on each keystroke
    _timeoutTimer?.cancel();
    _timeoutTimer = Timer(_typingTimeout, _stopTyping);
    
    // If not currently marked as typing, start immediately
    if (!_isTyping) {
      _startTyping();
      return;
    }
    
    // Otherwise, debounce the 'still typing' updates
    // Timer already running from _startTyping
  }
  
  void _startTyping() {
    _isTyping = true;
    _sendTypingStatus(true);
    
    // Set up debounced updates while typing continues
    _debounceTimer?.cancel();
    _debounceTimer = Timer.periodic(_debounceInterval, (_) {
      if (_isTyping) {
        _sendTypingStatus(true);
      }
    });
  }
  
  void _stopTyping() {
    if (!_isTyping) return;
    
    _isTyping = false;
    _debounceTimer?.cancel();
    _timeoutTimer?.cancel();
    _sendTypingStatus(false);
  }
  
  Future<void> _sendTypingStatus(bool isTyping) async {
    try {
      await _client.chat.setTypingStatus(
        conversationId: _conversationId,
        isTyping: isTyping,
      );
    } catch (e) {
      // Fail silently - typing status is not critical
      debugPrint('Failed to send typing status: $e');
    }
  }
  
  // Call when leaving the chat screen
  void dispose() {
    _stopTyping();
    _debounceTimer?.cancel();
    _timeoutTimer?.cancel();
  }
}

// Server-side typing tracking
class TypingManager {
  // conversationId -> Map<userId, expirationTime>
  static final Map<String, Map<int, DateTime>> _typingUsers = {};
  
  static void setTyping(
    String conversationId,
    int userId,
    bool isTyping,
  ) {
    _typingUsers.putIfAbsent(conversationId, () => {});
    
    if (isTyping) {
      // Set expiration 10 seconds from now
      _typingUsers[conversationId]![userId] = 
          DateTime.now().add(Duration(seconds: 10));
    } else {
      _typingUsers[conversationId]!.remove(userId);
    }
  }
  
  static List<int> getTypingUsers(String conversationId) {
    final typing = _typingUsers[conversationId] ?? {};
    final now = DateTime.now();
    
    // Filter out expired entries
    typing.removeWhere((_, expiration) => expiration.isBefore(now));
    
    return typing.keys.toList();
  }
}
```
