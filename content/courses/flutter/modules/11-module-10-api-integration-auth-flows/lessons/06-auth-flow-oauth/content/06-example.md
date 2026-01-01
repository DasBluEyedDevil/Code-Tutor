---
type: "EXAMPLE"
title: "Section 5: Connecting OAuth to Serverpod Backend"
---

Now that we have Google and Apple Sign-In working on the client, we need to verify the tokens with Serverpod and create user sessions.

**Step 1: Update Auth Service for OAuth**

Update `lib/services/auth_service.dart` to add OAuth authentication methods:

```dart
import 'package:your_app_client/your_app_client.dart';
import 'secure_storage_service.dart';
import 'google_auth_service.dart';
import 'apple_auth_service.dart';

/// Result of an OAuth sign-in attempt.
class OAuthSignInResult {
  final bool success;
  final String? errorMessage;
  final String? errorCode;
  final UserInfo? user;
  final bool accountExists;
  final String? existingProvider;
  
  OAuthSignInResult({
    required this.success,
    this.errorMessage,
    this.errorCode,
    this.user,
    this.accountExists = false,
    this.existingProvider,
  });
  
  factory OAuthSignInResult.success(UserInfo user) {
    return OAuthSignInResult(
      success: true,
      user: user,
    );
  }
  
  factory OAuthSignInResult.failure(String message, {String? code}) {
    return OAuthSignInResult(
      success: false,
      errorMessage: message,
      errorCode: code,
    );
  }
  
  factory OAuthSignInResult.accountExistsWithDifferentProvider(
    String provider,
  ) {
    return OAuthSignInResult(
      success: false,
      errorMessage: 'An account with this email already exists. '
          'Please sign in with $provider instead.',
      errorCode: 'account_exists',
      accountExists: true,
      existingProvider: provider,
    );
  }
}

// Add these methods to your existing AuthService class:

class AuthService {
  final Client _client;
  final SecureStorageService _secureStorage;
  
  AuthService({
    required Client client,
    required SecureStorageService secureStorage,
  })  : _client = client,
        _secureStorage = secureStorage;
  
  /// Signs in with Google using the provided tokens.
  /// The server verifies the ID token with Google and creates a session.
  Future<OAuthSignInResult> signInWithGoogle({
    required String idToken,
    required String accessToken,
    String? email,
    String? displayName,
    String? photoUrl,
  }) async {
    try {
      // Call Serverpod to verify the Google token and create session
      final response = await _client.auth.authenticateWithGoogle(
        idToken: idToken,
        accessToken: accessToken,
        email: email,
        displayName: displayName,
        photoUrl: photoUrl,
      );
      
      if (response.success && response.userInfo != null) {
        // Store authentication data
        if (response.keyId != null && response.key != null) {
          await _secureStorage.saveAuthData(
            authToken: response.key!,
            refreshToken: response.keyId.toString(),
            userId: response.userInfo!.id!,
            email: response.userInfo!.email ?? email,
            provider: 'google',
          );
          
          await _secureStorage.saveTokenExpiration(
            DateTime.now().add(const Duration(hours: 1)),
          );
        }
        
        return OAuthSignInResult.success(response.userInfo!);
      } else {
        return _parseOAuthError(response);
      }
    } on ServerpodClientException catch (e) {
      return _handleServerpodException(e);
    } catch (e) {
      return OAuthSignInResult.failure(
        'Failed to sign in with Google. Please try again.',
        code: 'google_auth_failed',
      );
    }
  }
  
  /// Signs in with Apple using the provided tokens.
  /// The server verifies the identity token with Apple and creates a session.
  Future<OAuthSignInResult> signInWithApple({
    required String identityToken,
    required String authorizationCode,
    required String userIdentifier,
    String? email,
    String? fullName,
  }) async {
    try {
      // Call Serverpod to verify the Apple token and create session
      final response = await _client.auth.authenticateWithApple(
        identityToken: identityToken,
        authorizationCode: authorizationCode,
        userIdentifier: userIdentifier,
        email: email,
        fullName: fullName,
      );
      
      if (response.success && response.userInfo != null) {
        // Store authentication data
        if (response.keyId != null && response.key != null) {
          await _secureStorage.saveAuthData(
            authToken: response.key!,
            refreshToken: response.keyId.toString(),
            userId: response.userInfo!.id!,
            email: response.userInfo!.email ?? email,
            provider: 'apple',
          );
          
          await _secureStorage.saveTokenExpiration(
            DateTime.now().add(const Duration(hours: 1)),
          );
        }
        
        return OAuthSignInResult.success(response.userInfo!);
      } else {
        return _parseOAuthError(response);
      }
    } on ServerpodClientException catch (e) {
      return _handleServerpodException(e);
    } catch (e) {
      return OAuthSignInResult.failure(
        'Failed to sign in with Apple. Please try again.',
        code: 'apple_auth_failed',
      );
    }
  }
  
  /// Parses OAuth error responses from the server.
  OAuthSignInResult _parseOAuthError(AuthenticationResponse response) {
    switch (response.failReason) {
      case AuthenticationFailReason.invalidCredentials:
        return OAuthSignInResult.failure(
          'Invalid authentication token. Please try again.',
          code: 'invalid_token',
        );
      case AuthenticationFailReason.blocked:
        return OAuthSignInResult.failure(
          'Your account has been suspended. Please contact support.',
          code: 'account_blocked',
        );
      default:
        return OAuthSignInResult.failure(
          'Authentication failed. Please try again.',
          code: 'auth_failed',
        );
    }
  }
  
  OAuthSignInResult _handleServerpodException(ServerpodClientException e) {
    if (e.statusCode == 409) {
      // Conflict - account exists with different provider
      return OAuthSignInResult.accountExistsWithDifferentProvider(
        e.message ?? 'another provider',
      );
    } else if (e.statusCode >= 500) {
      return OAuthSignInResult.failure(
        'Server error. Please try again later.',
        code: 'server_error',
      );
    }
    return OAuthSignInResult.failure(
      'Connection error. Please check your internet.',
      code: 'connection_error',
    );
  }
}
```

**Step 2: Update Secure Storage for OAuth Providers**

Add provider tracking to `lib/services/secure_storage_service.dart`:

```dart
static const String _authProviderKey = 'auth_provider';

/// Saves the authentication provider (email, google, apple).
Future<void> saveAuthProvider(String provider) async {
  await _storage.write(key: _authProviderKey, value: provider);
}

/// Gets the authentication provider.
Future<String?> getAuthProvider() async {
  return await _storage.read(key: _authProviderKey);
}

/// Extended saveAuthData to include provider.
Future<void> saveAuthData({
  required String authToken,
  required String refreshToken,
  required int userId,
  String? email,
  String provider = 'email',
}) async {
  await Future.wait([
    saveAuthToken(authToken),
    saveRefreshToken(refreshToken),
    saveUserId(userId),
    if (email != null) saveUserEmail(email),
    saveAuthProvider(provider),
  ]);
}
```

Your OAuth integration with Serverpod is now complete. The server will verify tokens and create sessions securely.

