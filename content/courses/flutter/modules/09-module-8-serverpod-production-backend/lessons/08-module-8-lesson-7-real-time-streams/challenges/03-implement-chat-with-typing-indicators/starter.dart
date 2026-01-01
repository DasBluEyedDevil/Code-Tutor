import 'dart:async';

/// Manages typing indicators for chat channels.
class TypingIndicatorManager {
  // TODO: Track which users are typing in which channels
  // Map<channelId, Map<userId, Timer>>
  
  static const typingTimeout = Duration(seconds: 3);
  
  // Callback when typing status changes (for broadcasting to clients)
  final void Function(String channelId, int userId, bool isTyping)? onTypingChanged;
  
  TypingIndicatorManager({this.onTypingChanged});
  
  /// Called when a user starts typing (or continues typing).
  /// Should reset the timeout timer each time.
  void startTyping(String channelId, int userId) {
    // TODO: Cancel existing timer for this user if any
    // TODO: Add user to typing set for this channel
    // TODO: Start a new timeout timer
    // TODO: Notify via callback if this is a new typing session
  }
  
  /// Called when user explicitly stops typing (e.g., sent message or cleared input).
  void stopTyping(String channelId, int userId) {
    // TODO: Cancel timer for this user
    // TODO: Remove user from typing set
    // TODO: Notify via callback
  }
  
  /// Get list of users currently typing in a channel.
  List<int> getTypingUsers(String channelId) {
    // TODO: Return list of userIds currently typing in this channel
    return [];
  }
  
  /// Check if a specific user is typing in a channel.
  bool isUserTyping(String channelId, int userId) {
    // TODO: Return true if user is currently typing
    return false;
  }
  
  /// Clean up all timers (call when shutting down).
  void dispose() {
    // TODO: Cancel all active timers
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