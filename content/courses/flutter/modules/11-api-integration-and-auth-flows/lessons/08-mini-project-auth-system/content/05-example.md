---
type: "EXAMPLE"
title: "Section 2 (continued): Auth Service"
---

**Step 3: Auth Service**

The AuthService handles all authentication operations. This example uses a mock implementation that you can replace with Firebase Auth or your custom backend:

```dart
// lib/features/auth/data/auth_service.dart

import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:google_sign_in/google_sign_in.dart';
import '../domain/user_model.dart';

class AuthException implements Exception {
  final String message;
  final String? code;

  AuthException(this.message, {this.code});

  @override
  String toString() => message;
}

class AuthResult {
  final User user;
  final String accessToken;
  final String? refreshToken;

  AuthResult({
    required this.user,
    required this.accessToken,
    this.refreshToken,
  });
}

class AuthService {
  final FlutterSecureStorage _storage;
  final GoogleSignIn _googleSignIn;

  static const _accessTokenKey = 'access_token';
  static const _refreshTokenKey = 'refresh_token';
  static const _userKey = 'user_data';

  AuthService({
    FlutterSecureStorage? storage,
    GoogleSignIn? googleSignIn,
  })  : _storage = storage ?? const FlutterSecureStorage(),
        _googleSignIn = googleSignIn ??
            GoogleSignIn(scopes: ['email', 'profile']);

  /// Register a new user with email and password.
  Future<AuthResult> register({
    required String email,
    required String password,
    String? displayName,
  }) async {
    // Simulate API call
    await Future.delayed(const Duration(seconds: 1));

    // In production, call your backend API:
    // final response = await dio.post('/auth/register', data: {...});

    // Mock successful registration
    final user = User(
      id: 'user_${DateTime.now().millisecondsSinceEpoch}',
      email: email,
      displayName: displayName,
      createdAt: DateTime.now(),
    );

    final accessToken = 'mock_access_token_${user.id}';
    final refreshToken = 'mock_refresh_token_${user.id}';

    // Store tokens
    await _storeTokens(accessToken, refreshToken);

    return AuthResult(
      user: user,
      accessToken: accessToken,
      refreshToken: refreshToken,
    );
  }

  /// Login with email and password.
  Future<AuthResult> login({
    required String email,
    required String password,
  }) async {
    await Future.delayed(const Duration(seconds: 1));

    // Mock validation
    if (password.length < 6) {
      throw AuthException('Invalid email or password', code: 'invalid_credentials');
    }

    // Mock successful login
    final user = User(
      id: 'user_123',
      email: email,
      displayName: email.split('@').first,
    );

    final accessToken = 'mock_access_token_${user.id}';
    final refreshToken = 'mock_refresh_token_${user.id}';

    await _storeTokens(accessToken, refreshToken);

    return AuthResult(
      user: user,
      accessToken: accessToken,
      refreshToken: refreshToken,
    );
  }

  /// Login with Google OAuth.
  Future<AuthResult> loginWithGoogle() async {
    try {
      final googleUser = await _googleSignIn.signIn();

      if (googleUser == null) {
        throw AuthException('Google sign-in cancelled', code: 'cancelled');
      }

      final googleAuth = await googleUser.authentication;

      // In production, send googleAuth.idToken to your backend
      // to verify and create/fetch user

      final user = User(
        id: googleUser.id,
        email: googleUser.email,
        displayName: googleUser.displayName,
        photoUrl: googleUser.photoUrl,
      );

      final accessToken = googleAuth.accessToken ?? 'google_access_token';

      await _storeTokens(accessToken, null);

      return AuthResult(
        user: user,
        accessToken: accessToken,
      );
    } catch (e) {
      if (e is AuthException) rethrow;
      throw AuthException('Google sign-in failed: $e');
    }
  }

  /// Check if user has stored tokens and validate them.
  Future<AuthResult?> checkStoredAuth() async {
    final accessToken = await _storage.read(key: _accessTokenKey);

    if (accessToken == null) {
      return null;
    }

    // In production, validate token with backend
    // If expired, try to refresh

    // Mock: Return stored user
    final user = User(
      id: 'user_123',
      email: 'stored@example.com',
      displayName: 'Stored User',
    );

    return AuthResult(
      user: user,
      accessToken: accessToken,
      refreshToken: await _storage.read(key: _refreshTokenKey),
    );
  }

  /// Refresh the access token.
  Future<String> refreshAccessToken(String refreshToken) async {
    await Future.delayed(const Duration(milliseconds: 500));

    // In production, call your backend refresh endpoint
    final newAccessToken = 'refreshed_token_${DateTime.now().millisecondsSinceEpoch}';

    await _storage.write(key: _accessTokenKey, value: newAccessToken);

    return newAccessToken;
  }

  /// Logout and clear stored tokens.
  Future<void> logout() async {
    await _googleSignIn.signOut();
    await _storage.deleteAll();
  }

  Future<void> _storeTokens(String accessToken, String? refreshToken) async {
    await _storage.write(key: _accessTokenKey, value: accessToken);
    if (refreshToken != null) {
      await _storage.write(key: _refreshTokenKey, value: refreshToken);
    }
  }
}
```
