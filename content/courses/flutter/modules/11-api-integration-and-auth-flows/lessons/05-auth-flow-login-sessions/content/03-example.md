---
type: "EXAMPLE"
title: "Serverpod Authentication: Sign In Endpoint"
---

Now let us extend the AuthService from Lesson 10.4 to include login functionality. The login process validates credentials against the server and returns session tokens.

**Extend the Auth Service**

Update `lib/services/auth_service.dart` to add login methods:

```dart
import 'dart:io';
import 'package:your_app_client/your_app_client.dart';
import 'package:serverpod_flutter/serverpod_flutter.dart';
import 'secure_storage_service.dart';

/// Result of a login attempt.
class LoginResult {
  final bool success;
  final String? errorMessage;
  final String? errorCode;
  final UserInfo? user;
  final bool requiresEmailVerification;
  final bool requiresTwoFactor;
  
  LoginResult({
    required this.success,
    this.errorMessage,
    this.errorCode,
    this.user,
    this.requiresEmailVerification = false,
    this.requiresTwoFactor = false,
  });
  
  factory LoginResult.success(UserInfo user) {
    return LoginResult(
      success: true,
      user: user,
    );
  }
  
  factory LoginResult.failure(String message, {String? code}) {
    return LoginResult(
      success: false,
      errorMessage: message,
      errorCode: code,
    );
  }
  
  factory LoginResult.requiresVerification() {
    return LoginResult(
      success: false,
      errorMessage: 'Please verify your email address before signing in.',
      errorCode: 'email_not_verified',
      requiresEmailVerification: true,
    );
  }
  
  factory LoginResult.requiresTwoFactor() {
    return LoginResult(
      success: false,
      errorMessage: 'Two-factor authentication required.',
      errorCode: 'two_factor_required',
      requiresTwoFactor: true,
    );
  }
}

/// Service handling all authentication operations with Serverpod backend.
class AuthService {
  final Client _client;
  final SecureStorageService _secureStorage;
  
  AuthService({
    required Client client,
    required SecureStorageService secureStorage,
  })  : _client = client,
        _secureStorage = secureStorage;
  
  /// Signs in a user with email and password.
  /// 
  /// Returns a [LoginResult] indicating success or failure with details.
  /// If [rememberMe] is true, the session will persist across app restarts.
  Future<LoginResult> signInWithEmail({
    required String email,
    required String password,
    bool rememberMe = true,
  }) async {
    try {
      // Call the Serverpod authentication endpoint
      final response = await _client.auth.authenticate(
        email: email.trim().toLowerCase(),
        password: password,
      );
      
      // Check the response
      if (response.success && response.userInfo != null) {
        // Check if email is verified
        if (response.userInfo!.blocked == true) {
          return LoginResult.failure(
            'Your account has been suspended. Please contact support.',
            code: 'account_blocked',
          );
        }
        
        // Store authentication data
        if (response.keyId != null && response.key != null) {
          await _secureStorage.saveAuthData(
            authToken: response.key!,
            refreshToken: response.keyId.toString(),
            userId: response.userInfo!.id!,
            email: email.trim().toLowerCase(),
          );
          
          // If remember me is enabled, save the email for next time
          if (rememberMe) {
            await _secureStorage.saveUserEmail(email.trim().toLowerCase());
          }
          
          // Store token expiration time for refresh logic
          await _secureStorage.saveTokenExpiration(
            DateTime.now().add(const Duration(hours: 1)),
          );
        }
        
        return LoginResult.success(response.userInfo!);
      } else {
        // Authentication failed - parse the error
        return _parseLoginError(response);
      }
    } on ServerpodClientException catch (e) {
      return _handleServerpodException(e);
    } on SocketException {
      return LoginResult.failure(
        'No internet connection. Please check your network and try again.',
        code: 'network_error',
      );
    } catch (e) {
      return LoginResult.failure(
        'An unexpected error occurred. Please try again.',
        code: 'unknown_error',
      );
    }
  }
  
  /// Parses error response from failed login.
  LoginResult _parseLoginError(AuthenticationResponse response) {
    switch (response.failReason) {
      case AuthenticationFailReason.invalidCredentials:
        return LoginResult.failure(
          'Invalid email or password. Please try again.',
          code: 'invalid_credentials',
        );
      case AuthenticationFailReason.userNotFound:
        return LoginResult.failure(
          'No account found with this email address.',
          code: 'user_not_found',
        );
      case AuthenticationFailReason.tooManyFailedAttempts:
        return LoginResult.failure(
          'Too many failed attempts. Please wait 15 minutes before trying again.',
          code: 'rate_limited',
        );
      case AuthenticationFailReason.blocked:
        return LoginResult.failure(
          'Your account has been suspended. Please contact support.',
          code: 'account_blocked',
        );
      default:
        return LoginResult.failure(
          'Login failed. Please try again.',
          code: 'login_failed',
        );
    }
  }
  
  /// Handles Serverpod client exceptions.
  LoginResult _handleServerpodException(ServerpodClientException e) {
    if (e.statusCode == 401) {
      return LoginResult.failure(
        'Invalid email or password.',
        code: 'invalid_credentials',
      );
    } else if (e.statusCode == 429) {
      return LoginResult.failure(
        'Too many login attempts. Please wait before trying again.',
        code: 'rate_limited',
      );
    } else if (e.statusCode >= 500) {
      return LoginResult.failure(
        'Server error. Please try again later.',
        code: 'server_error',
      );
    } else {
      return LoginResult.failure(
        'Connection error. Please check your internet.',
        code: 'connection_error',
      );
    }
  }
  
  /// Checks if there is a valid stored session.
  /// Returns the user info if session is valid, null otherwise.
  Future<UserInfo?> checkStoredSession() async {
    try {
      final hasCredentials = await _secureStorage.hasAuthCredentials();
      if (!hasCredentials) {
        return null;
      }
      
      // Verify the session is still valid with the server
      final userInfo = await _client.auth.getUserInfo();
      return userInfo;
    } catch (e) {
      // Session invalid or expired - clear stored data
      await _secureStorage.clearAllAuthData();
      return null;
    }
  }
}
```

**Update SecureStorageService for Token Expiration**

Add token expiration tracking to `lib/services/secure_storage_service.dart`:

```dart
// Add these methods to SecureStorageService

static const String _tokenExpirationKey = 'token_expiration';

/// Saves the token expiration timestamp.
Future<void> saveTokenExpiration(DateTime expiration) async {
  await _storage.write(
    key: _tokenExpirationKey,
    value: expiration.toIso8601String(),
  );
}

/// Gets the token expiration timestamp.
Future<DateTime?> getTokenExpiration() async {
  final value = await _storage.read(key: _tokenExpirationKey);
  if (value == null) return null;
  return DateTime.tryParse(value);
}

/// Checks if the stored token is expired or about to expire.
/// Returns true if token expires within the next 5 minutes.
Future<bool> isTokenExpiredOrExpiring() async {
  final expiration = await getTokenExpiration();
  if (expiration == null) return true;
  
  // Consider token expired if it expires in less than 5 minutes
  final expirationBuffer = expiration.subtract(const Duration(minutes: 5));
  return DateTime.now().isAfter(expirationBuffer);
}
```

This implementation handles various login scenarios including invalid credentials, blocked accounts, rate limiting, and network errors.

