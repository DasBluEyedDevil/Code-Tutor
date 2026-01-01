---
type: "EXAMPLE"
title: "Section 4: Implementing Apple Sign-In in Flutter"
---

Now let us implement Apple Sign-In in your Flutter app using the sign_in_with_apple package.

**Step 1: Add Dependencies**

Add to your `pubspec.yaml`:

```yaml
dependencies:
  sign_in_with_apple: ^5.0.0
  crypto: ^3.0.3  # For generating nonce
```

Run `flutter pub get`.

**Step 2: Create Apple Auth Service**

Create `lib/services/apple_auth_service.dart`:

```dart
import 'dart:convert';
import 'dart:io';
import 'dart:math';
import 'package:crypto/crypto.dart';
import 'package:flutter/foundation.dart';
import 'package:sign_in_with_apple/sign_in_with_apple.dart';

/// Result of an Apple Sign-In attempt.
class AppleSignInResult {
  final bool success;
  final String? identityToken;
  final String? authorizationCode;
  final String? userIdentifier;
  final String? email;
  final String? givenName;
  final String? familyName;
  final String? errorMessage;
  final String? errorCode;
  
  AppleSignInResult({
    required this.success,
    this.identityToken,
    this.authorizationCode,
    this.userIdentifier,
    this.email,
    this.givenName,
    this.familyName,
    this.errorMessage,
    this.errorCode,
  });
  
  factory AppleSignInResult.success({
    required String identityToken,
    required String authorizationCode,
    required String userIdentifier,
    String? email,
    String? givenName,
    String? familyName,
  }) {
    return AppleSignInResult(
      success: true,
      identityToken: identityToken,
      authorizationCode: authorizationCode,
      userIdentifier: userIdentifier,
      email: email,
      givenName: givenName,
      familyName: familyName,
    );
  }
  
  factory AppleSignInResult.failure(String message, {String? code}) {
    return AppleSignInResult(
      success: false,
      errorMessage: message,
      errorCode: code,
    );
  }
  
  factory AppleSignInResult.cancelled() {
    return AppleSignInResult(
      success: false,
      errorMessage: 'Sign-in was cancelled',
      errorCode: 'cancelled',
    );
  }
  
  factory AppleSignInResult.notSupported() {
    return AppleSignInResult(
      success: false,
      errorMessage: 'Apple Sign-In is not available on this device',
      errorCode: 'not_supported',
    );
  }
  
  /// Returns the full name by combining given and family names.
  String? get fullName {
    if (givenName == null && familyName == null) return null;
    return [givenName, familyName]
        .where((part) => part != null && part.isNotEmpty)
        .join(' ');
  }
}

/// Service handling Apple Sign-In authentication.
class AppleAuthService {
  /// Checks if Apple Sign-In is available on this device.
  /// Returns true on iOS 13+ and macOS 10.15+.
  Future<bool> isAvailable() async {
    // Apple Sign-In is only available on iOS and macOS
    if (!Platform.isIOS && !Platform.isMacOS) {
      return false;
    }
    
    return await SignInWithApple.isAvailable();
  }
  
  /// Initiates Apple Sign-In flow.
  Future<AppleSignInResult> signIn() async {
    // Check availability first
    final isSupported = await isAvailable();
    if (!isSupported) {
      return AppleSignInResult.notSupported();
    }
    
    try {
      // Generate a secure nonce for added security
      final rawNonce = _generateNonce();
      final hashedNonce = _sha256ofString(rawNonce);
      
      // Request credentials from Apple
      final credential = await SignInWithApple.getAppleIDCredential(
        scopes: [
          AppleIDAuthorizationScopes.email,
          AppleIDAuthorizationScopes.fullName,
        ],
        nonce: hashedNonce,
      );
      
      // Validate we have the required tokens
      if (credential.identityToken == null) {
        return AppleSignInResult.failure(
          'Failed to obtain identity token from Apple',
          code: 'missing_token',
        );
      }
      
      if (credential.authorizationCode == null) {
        return AppleSignInResult.failure(
          'Failed to obtain authorization code from Apple',
          code: 'missing_auth_code',
        );
      }
      
      return AppleSignInResult.success(
        identityToken: credential.identityToken!,
        authorizationCode: credential.authorizationCode!,
        userIdentifier: credential.userIdentifier ?? '',
        email: credential.email,
        givenName: credential.givenName,
        familyName: credential.familyName,
      );
    } on SignInWithAppleAuthorizationException catch (e) {
      return _handleAppleError(e);
    } catch (e) {
      if (kDebugMode) {
        print('Apple Sign-In error: $e');
      }
      return AppleSignInResult.failure(
        'An unexpected error occurred during sign-in',
        code: 'unknown_error',
      );
    }
  }
  
  /// Generates a cryptographically secure random nonce.
  String _generateNonce([int length = 32]) {
    const charset = '0123456789ABCDEFGHIJKLMNOPQRSTUVXYZabcdefghijklmnopqrstuvwxyz-._';
    final random = Random.secure();
    return List.generate(
      length, 
      (_) => charset[random.nextInt(charset.length)],
    ).join();
  }
  
  /// Generates SHA256 hash of the input string.
  String _sha256ofString(String input) {
    final bytes = utf8.encode(input);
    final digest = sha256.convert(bytes);
    return digest.toString();
  }
  
  /// Handles Apple Sign-In authorization exceptions.
  AppleSignInResult _handleAppleError(SignInWithAppleAuthorizationException e) {
    switch (e.code) {
      case AuthorizationErrorCode.canceled:
        return AppleSignInResult.cancelled();
      case AuthorizationErrorCode.failed:
        return AppleSignInResult.failure(
          'Sign-in failed. Please try again.',
          code: 'failed',
        );
      case AuthorizationErrorCode.invalidResponse:
        return AppleSignInResult.failure(
          'Invalid response from Apple. Please try again.',
          code: 'invalid_response',
        );
      case AuthorizationErrorCode.notHandled:
        return AppleSignInResult.failure(
          'Sign-in request was not handled. Please try again.',
          code: 'not_handled',
        );
      case AuthorizationErrorCode.notInteractive:
        return AppleSignInResult.failure(
          'Sign-in requires user interaction.',
          code: 'not_interactive',
        );
      default:
        return AppleSignInResult.failure(
          'Authentication failed. Please try again.',
          code: 'unknown',
        );
    }
  }
}
```

**Step 3: Add Riverpod Provider**

Add to your `lib/providers/auth_provider.dart`:

```dart
/// Apple auth service provider
final appleAuthServiceProvider = Provider<AppleAuthService>((ref) {
  return AppleAuthService();
});
```

**Important Note About Apple Sign-In on Android**

Apple Sign-In is not natively available on Android. If you need Apple Sign-In on Android, you must:

1. Implement a web-based OAuth flow
2. Use the Service ID you created earlier
3. Handle the redirect from Apple's web authentication
4. This is significantly more complex and requires a server component

For most apps, showing Apple Sign-In only on iOS devices is acceptable and recommended.

