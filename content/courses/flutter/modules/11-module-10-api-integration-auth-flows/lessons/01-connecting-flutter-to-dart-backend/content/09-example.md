---
type: "EXAMPLE"
title: "Implementing Session Management"
---


Here is a complete authentication service that manages login, logout, and session state:



```dart
// lib/services/auth_service.dart
import 'package:flutter/foundation.dart';
import 'package:my_app_client/my_app_client.dart';
import 'package:serverpod_auth_shared_flutter/serverpod_auth_shared_flutter.dart';

/// Manages authentication state and provides login/logout functionality.
/// Uses ChangeNotifier so UI can react to auth state changes.
class AuthService extends ChangeNotifier {
  final Client _client;
  final SessionManager _sessionManager;
  
  UserInfo? _currentUser;
  bool _isLoading = false;
  
  AuthService(this._client)
      : _sessionManager = SessionManager(
          caller: _client.modules.auth,
        ) {
    // Listen to session changes
    _sessionManager.addListener(_onSessionChanged);
  }
  
  // Getters for UI to access state
  UserInfo? get currentUser => _currentUser;
  bool get isSignedIn => _currentUser != null;
  bool get isLoading => _isLoading;
  
  /// Initialize the auth service - call this at app startup
  Future<void> initialize() async {
    _isLoading = true;
    notifyListeners();
    
    try {
      // Check if we have a stored session
      await _sessionManager.initialize();
      _currentUser = _sessionManager.signedInUser;
    } finally {
      _isLoading = false;
      notifyListeners();
    }
  }
  
  /// Sign in with email and password
  Future<UserInfo?> signInWithEmail(String email, String password) async {
    _isLoading = true;
    notifyListeners();
    
    try {
      // Call the authentication endpoint
      final response = await _client.modules.auth.email.authenticate(
        email,
        password,
      );
      
      if (response.success) {
        // Store the authentication key
        await _sessionManager.registerSignedInUser(
          response.userInfo!,
          response.keyId!,
          response.key!,
        );
        
        _currentUser = response.userInfo;
        notifyListeners();
        return _currentUser;
      } else {
        throw AuthenticationFailedException(response.failReason?.name ?? 'Unknown error');
      }
    } finally {
      _isLoading = false;
      notifyListeners();
    }
  }
  
  /// Sign out the current user
  Future<void> signOut() async {
    _isLoading = true;
    notifyListeners();
    
    try {
      // Tell server to invalidate the session
      await _client.modules.auth.status.signOut();
      
      // Clear local session data
      await _sessionManager.signOut();
      
      _currentUser = null;
    } finally {
      _isLoading = false;
      notifyListeners();
    }
  }
  
  /// Called when session state changes
  void _onSessionChanged() {
    _currentUser = _sessionManager.signedInUser;
    notifyListeners();
  }
  
  @override
  void dispose() {
    _sessionManager.removeListener(_onSessionChanged);
    super.dispose();
  }
}

/// Custom exception for authentication failures
class AuthenticationFailedException implements Exception {
  final String message;
  AuthenticationFailedException(this.message);
  
  @override
  String toString() => 'AuthenticationFailedException: $message';
}
```
