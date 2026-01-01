// lib/services/session_timeout_manager.dart
import 'dart:async';
import 'package:flutter/foundation.dart';

class SessionTimeoutManager {
  final Duration timeoutDuration;
  final Duration warningDuration;
  final VoidCallback onTimeout;
  final VoidCallback onWarning;
  
  Timer? _timeoutTimer;
  Timer? _warningTimer;
  bool _warningShown = false;
  bool _isActive = false;
  
  SessionTimeoutManager({
    this.timeoutDuration = const Duration(minutes: 15),
    this.warningDuration = const Duration(minutes: 1),
    required this.onTimeout,
    required this.onWarning,
  });
  
  void start() {
    _isActive = true;
    resetTimer();
  }
  
  void stop() {
    _isActive = false;
    _cancelTimers();
  }
  
  void resetTimer() {
    if (!_isActive) return;
    
    _cancelTimers();
    _warningShown = false;
    
    // Calculate warning time
    final warningTime = timeoutDuration - warningDuration;
    
    // Start warning timer
    _warningTimer = Timer(warningTime, () {
      _warningShown = true;
      onWarning();
    });
    
    // Start timeout timer
    _timeoutTimer = Timer(timeoutDuration, () {
      _isActive = false;
      onTimeout();
    });
    
    if (kDebugMode) {
      print('Session timer reset. Timeout in ${timeoutDuration.inMinutes} minutes.');
    }
  }
  
  void extendSession() {
    if (kDebugMode) {
      print('Session extended by user.');
    }
    resetTimer();
  }
  
  void _cancelTimers() {
    _timeoutTimer?.cancel();
    _warningTimer?.cancel();
    _timeoutTimer = null;
    _warningTimer = null;
  }
  
  bool get warningShown => _warningShown;
  bool get isActive => _isActive;
  
  void dispose() {
    _cancelTimers();
  }
}

// lib/widgets/activity_tracker.dart
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class ActivityTracker extends ConsumerStatefulWidget {
  final Widget child;
  
  const ActivityTracker({super.key, required this.child});
  
  @override
  ConsumerState<ActivityTracker> createState() => _ActivityTrackerState();
}

class _ActivityTrackerState extends ConsumerState<ActivityTracker> {
  @override
  void initState() {
    super.initState();
    // Start timeout tracking when authenticated
    ref.read(sessionTimeoutManagerProvider).start();
  }
  
  void _onUserActivity() {
    final isAuthenticated = ref.read(isAuthenticatedProvider);
    if (isAuthenticated) {
      ref.read(sessionTimeoutManagerProvider).resetTimer();
    }
  }
  
  @override
  Widget build(BuildContext context) {
    // Listen for auth state changes
    ref.listen<bool>(isAuthenticatedProvider, (previous, next) {
      final manager = ref.read(sessionTimeoutManagerProvider);
      if (next) {
        manager.start();
      } else {
        manager.stop();
      }
    });
    
    return Listener(
      onPointerDown: (_) => _onUserActivity(),
      onPointerMove: (_) => _onUserActivity(),
      behavior: HitTestBehavior.translucent,
      child: widget.child,
    );
  }
}

// lib/widgets/timeout_warning_dialog.dart
import 'package:flutter/material.dart';
import 'dart:async';

class TimeoutWarningDialog extends StatefulWidget {
  final Duration remainingTime;
  final VoidCallback onExtend;
  final VoidCallback onLogout;
  
  const TimeoutWarningDialog({
    super.key,
    required this.remainingTime,
    required this.onExtend,
    required this.onLogout,
  });
  
  @override
  State<TimeoutWarningDialog> createState() => _TimeoutWarningDialogState();
}

class _TimeoutWarningDialogState extends State<TimeoutWarningDialog> {
  late int _secondsRemaining;
  Timer? _countdownTimer;
  
  @override
  void initState() {
    super.initState();
    _secondsRemaining = widget.remainingTime.inSeconds;
    _startCountdown();
  }
  
  void _startCountdown() {
    _countdownTimer = Timer.periodic(const Duration(seconds: 1), (timer) {
      setState(() {
        _secondsRemaining--;
      });
      if (_secondsRemaining <= 0) {
        timer.cancel();
        Navigator.of(context).pop();
        widget.onLogout();
      }
    });
  }
  
  @override
  void dispose() {
    _countdownTimer?.cancel();
    super.dispose();
  }
  
  @override
  Widget build(BuildContext context) {
    return AlertDialog(
      icon: const Icon(Icons.timer, color: Colors.orange, size: 48),
      title: const Text('Session Timeout Warning'),
      content: Column(
        mainAxisSize: MainAxisSize.min,
        children: [
          const Text('Your session will expire due to inactivity.'),
          const SizedBox(height: 16),
          Text(
            'Time remaining: $_secondsRemaining seconds',
            style: const TextStyle(fontWeight: FontWeight.bold, fontSize: 18),
          ),
        ],
      ),
      actions: [
        TextButton(
          onPressed: () {
            Navigator.of(context).pop();
            widget.onLogout();
          },
          child: const Text('Log Out'),
        ),
        FilledButton(
          onPressed: () {
            Navigator.of(context).pop();
            widget.onExtend();
          },
          child: const Text('Stay Signed In'),
        ),
      ],
    );
  }
}