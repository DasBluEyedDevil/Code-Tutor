enum TypingActivity { typing, recordingAudio, uploadingFile }

enum TypingState { idle, active, stopping }

class EnhancedTypingService {
  final String conversationId;
  final Function(TypingState, TypingActivity?) onStateChanged;
  
  TypingState _currentState = TypingState.idle;
  TypingActivity? _currentActivity;
  
  Timer? _stopTimer;
  Timer? _minDisplayTimer;
  DateTime? _lastActiveTime;
  
  // Anti-flicker: minimum time to show 'is typing'
  static const Duration minDisplayTime = Duration(seconds: 1);
  // Time before transitioning from active to stopping
  static const Duration stopDelay = Duration(seconds: 3);
  // Time in stopping state before going idle
  static const Duration stoppingDuration = Duration(seconds: 2);
  
  EnhancedTypingService({
    required this.conversationId,
    required this.onStateChanged,
  });
  
  void setActivity(TypingActivity activity) {
    // TODO: Transition to active state with this activity
    // Cancel any pending stop timer
    // Start/reset the stop delay timer
    throw UnimplementedError();
  }
  
  void clearActivity() {
    // TODO: Start the stop delay timer
    // Don't immediately go to idle - wait for stopDelay
    throw UnimplementedError();
  }
  
  void _transitionTo(TypingState newState) {
    // TODO: Handle state transitions with anti-flicker logic
    // If transitioning away from active before minDisplayTime,
    // delay the transition
    throw UnimplementedError();
  }
  
  // Called when user navigates away from this conversation
  void onConversationExit() {
    // TODO: Store current state for potential restoration
    // Clear timers but remember state briefly
    throw UnimplementedError();
  }
  
  // Called when user returns to this conversation
  void onConversationEnter() {
    // TODO: Restore recent typing state if within threshold
    throw UnimplementedError();
  }
  
  void dispose() {
    // TODO: Clean up all timers
    throw UnimplementedError();
  }
}

// Widget to display enhanced typing state
class EnhancedTypingIndicator extends StatelessWidget {
  final TypingState state;
  final TypingActivity? activity;
  final List<TypingUser> users;
  
  const EnhancedTypingIndicator({
    required this.state,
    required this.activity,
    required this.users,
    super.key,
  });
  
  @override
  Widget build(BuildContext context) {
    // TODO: Build UI that shows:
    // - 'User is typing...' for active/typing
    // - 'User is recording...' for active/recordingAudio
    // - 'User is uploading...' for active/uploadingFile
    // - 'User was typing...' (faded) for stopping state
    throw UnimplementedError();
  }
}