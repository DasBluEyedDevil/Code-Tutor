import 'dart:async';

/// Manages typing indicators for chat channels.
class TypingIndicatorManager {
  // Track which users are typing in which channels
  // Outer map: channelId -> inner map
  // Inner map: userId -> their timeout timer
  final Map<String, Map<int, Timer>> _typingTimers = {};
  
  static const typingTimeout = Duration(seconds: 3);
  
  // Callback when typing status changes (for broadcasting to clients)
  final void Function(String channelId, int userId, bool isTyping)? onTypingChanged;
  
  TypingIndicatorManager({this.onTypingChanged});
  
  /// Called when a user starts typing (or continues typing).
  /// Should reset the timeout timer each time.
  void startTyping(String channelId, int userId) {
    // Ensure channel map exists
    _typingTimers[channelId] ??= {};
    final channelTimers = _typingTimers[channelId]!;
    
    // Check if this is a new typing session (user wasn't already typing)
    final isNewSession = !channelTimers.containsKey(userId);
    
    // Cancel existing timer if any (debouncing)
    channelTimers[userId]?.cancel();
    
    // Start new timeout timer
    channelTimers[userId] = Timer(typingTimeout, () {
      // Auto-stop typing after timeout
      _removeTyping(channelId, userId);
    });
    
    // Notify only if this is a new typing session
    if (isNewSession) {
      onTypingChanged?.call(channelId, userId, true);
    }
  }
  
  /// Called when user explicitly stops typing (e.g., sent message or cleared input).
  void stopTyping(String channelId, int userId) {
    _removeTyping(channelId, userId);
  }
  
  /// Internal method to remove typing and notify.
  void _removeTyping(String channelId, int userId) {
    final channelTimers = _typingTimers[channelId];
    if (channelTimers == null) return;
    
    final timer = channelTimers[userId];
    if (timer == null) return; // User wasn't typing
    
    // Cancel timer and remove user
    timer.cancel();
    channelTimers.remove(userId);
    
    // Clean up empty channel map
    if (channelTimers.isEmpty) {
      _typingTimers.remove(channelId);
    }
    
    // Notify that user stopped typing
    onTypingChanged?.call(channelId, userId, false);
  }
  
  /// Get list of users currently typing in a channel.
  List<int> getTypingUsers(String channelId) {
    final channelTimers = _typingTimers[channelId];
    if (channelTimers == null) return [];
    return channelTimers.keys.toList();
  }
  
  /// Check if a specific user is typing in a channel.
  bool isUserTyping(String channelId, int userId) {
    final channelTimers = _typingTimers[channelId];
    if (channelTimers == null) return false;
    return channelTimers.containsKey(userId);
  }
  
  /// Clean up all timers (call when shutting down).
  void dispose() {
    for (final channelTimers in _typingTimers.values) {
      for (final timer in channelTimers.values) {
        timer.cancel();
      }
    }
    _typingTimers.clear();
  }
}

// Test the implementation
void main() async {
  print('Testing Typing Indicator Manager\n');
  
  final manager = TypingIndicatorManager(
    onTypingChanged: (channel, user, isTyping) {
      print('Channel $channel: User $user is ${isTyping ? "typing" : "stopped typing"}');
    },
  );
  
  // Test 1: Basic typing
  print('--- Test 1: Basic typing ---');
  manager.startTyping('general', 1);
  print('Typing users in general: ${manager.getTypingUsers("general")}'); // [1]
  print('User 1 typing: ${manager.isUserTyping("general", 1)}'); // true
  
  // Test 2: Multiple users typing
  print('\n--- Test 2: Multiple users ---');
  manager.startTyping('general', 2);
  manager.startTyping('general', 3);
  print('Typing users in general: ${manager.getTypingUsers("general")}'); // [1, 2, 3]
  
  // Test 3: Explicit stop
  print('\n--- Test 3: Explicit stop ---');
  manager.stopTyping('general', 2);
  print('Typing users in general: ${manager.getTypingUsers("general")}'); // [1, 3]
  
  // Test 4: Different channels
  print('\n--- Test 4: Different channels ---');
  manager.startTyping('random', 1);
  print('Typing in general: ${manager.getTypingUsers("general")}'); // [1, 3]
  print('Typing in random: ${manager.getTypingUsers("random")}'); // [1]
  
  // Test 5: Auto timeout
  print('\n--- Test 5: Auto timeout (wait 4 seconds) ---');
  print('User 3 typing before timeout: ${manager.isUserTyping("general", 3)}');
  await Future.delayed(Duration(seconds: 4));
  print('User 3 typing after timeout: ${manager.isUserTyping("general", 3)}'); // false
  
  // Test 6: Debouncing - repeated startTyping resets timer
  print('\n--- Test 6: Debouncing ---');
  manager.startTyping('test', 5);
  await Future.delayed(Duration(seconds: 2));
  manager.startTyping('test', 5); // Reset timer
  await Future.delayed(Duration(seconds: 2));
  print('User 5 still typing (timer reset): ${manager.isUserTyping("test", 5)}'); // true
  await Future.delayed(Duration(seconds: 2));
  print('User 5 after full timeout: ${manager.isUserTyping("test", 5)}'); // false
  
  manager.dispose();
  print('\nAll tests completed!');
}