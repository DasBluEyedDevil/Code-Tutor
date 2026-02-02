// lib/services/session_timeout_manager.dart
import 'dart:async';

class SessionTimeoutManager {
  final Duration timeoutDuration;
  final Duration warningDuration;
  final VoidCallback onTimeout;
  final VoidCallback onWarning;
  
  Timer? _timeoutTimer;
  Timer? _warningTimer;
  
  SessionTimeoutManager({
    this.timeoutDuration = const Duration(minutes: 15),
    this.warningDuration = const Duration(minutes: 1),
    required this.onTimeout,
    required this.onWarning,
  });
  
  /// Call this when user activity is detected
  void resetTimer() {
    // TODO: Cancel existing timers
    // TODO: Start warning timer (timeout - warning duration)
    // TODO: Start timeout timer
  }
  
  /// Extend the session (called from warning dialog)
  void extendSession() {
    // TODO: Reset timers
  }
  
  void dispose() {
    // TODO: Cancel all timers
  }
}

// lib/widgets/activity_tracker.dart
// TODO: Create a widget that wraps the app and calls resetTimer on user activity
// Use Listener widget to detect PointerDownEvent