// lib/services/session_manager.dart

import 'dart:async';
import 'package:flutter/material.dart';

class SessionManager extends ChangeNotifier {
  DateTime? _expirationTime;
  Timer? _warningTimer;
  Timer? _expirationTimer;
  
  static const sessionDuration = Duration(minutes: 30);
  static const warningBeforeExpiry = Duration(minutes: 5);

  bool get isSessionValid {
    // TODO: Check if session is still valid
    throw UnimplementedError();
  }

  void startSession() {
    // TODO: Initialize session with expiration time
    // TODO: Start warning and expiration timers
    throw UnimplementedError();
  }

  void refreshSession() {
    // TODO: Extend session expiration time
    // TODO: Reset timers
    throw UnimplementedError();
  }

  void endSession() {
    // TODO: Clear session and cancel timers
    throw UnimplementedError();
  }

  void _onWarning() {
    // TODO: Notify listeners that session is about to expire
    throw UnimplementedError();
  }

  void _onExpiration() {
    // TODO: Notify listeners that session has expired
    throw UnimplementedError();
  }
}