// lib/services/session_manager.dart

import 'dart:async';
import 'package:flutter/material.dart';

enum SessionStatus { active, warning, expired, none }

class SessionManager extends ChangeNotifier {
  DateTime? _expirationTime;
  Timer? _warningTimer;
  Timer? _expirationTimer;
  SessionStatus _status = SessionStatus.none;

  static const sessionDuration = Duration(minutes: 30);
  static const warningBeforeExpiry = Duration(minutes: 5);

  SessionStatus get status => _status;

  bool get isSessionValid {
    if (_expirationTime == null) return false;
    return DateTime.now().isBefore(_expirationTime!);
  }

  Duration? get remainingTime {
    if (_expirationTime == null) return null;
    final remaining = _expirationTime!.difference(DateTime.now());
    return remaining.isNegative ? Duration.zero : remaining;
  }

  void startSession() {
    _expirationTime = DateTime.now().add(sessionDuration);
    _status = SessionStatus.active;
    _startTimers();
    notifyListeners();
  }

  void refreshSession() {
    if (_status == SessionStatus.expired) return;
    
    _cancelTimers();
    _expirationTime = DateTime.now().add(sessionDuration);
    _status = SessionStatus.active;
    _startTimers();
    notifyListeners();
  }

  void endSession() {
    _cancelTimers();
    _expirationTime = null;
    _status = SessionStatus.none;
    notifyListeners();
  }

  void _startTimers() {
    _cancelTimers();

    final warningDelay = sessionDuration - warningBeforeExpiry;
    _warningTimer = Timer(warningDelay, _onWarning);
    _expirationTimer = Timer(sessionDuration, _onExpiration);
  }

  void _cancelTimers() {
    _warningTimer?.cancel();
    _expirationTimer?.cancel();
    _warningTimer = null;
    _expirationTimer = null;
  }

  void _onWarning() {
    _status = SessionStatus.warning;
    notifyListeners();
  }

  void _onExpiration() {
    _status = SessionStatus.expired;
    _expirationTime = null;
    notifyListeners();
  }

  @override
  void dispose() {
    _cancelTimers();
    super.dispose();
  }
}

// Integration with router:
// In app_router.dart redirect callback:
// final session = ref.read(sessionManagerProvider);
// if (!session.isSessionValid && isAuthenticated) {
//   ref.read(authNotifierProvider).setUnauthenticated();
//   return '/login?redirect=...';
// }