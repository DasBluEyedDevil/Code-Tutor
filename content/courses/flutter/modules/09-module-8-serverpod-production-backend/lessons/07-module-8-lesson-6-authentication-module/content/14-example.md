---
type: "EXAMPLE"
title: "Implementing Apple Sign-In"
---

Here is how to implement Apple Sign-In in your Flutter app.



```dart
// lib/services/apple_auth_service.dart

import 'dart:io';
import 'package:sign_in_with_apple/sign_in_with_apple.dart';
import 'package:serverpod_auth_apple_flutter/serverpod_auth_apple_flutter.dart';
import 'package:my_project_client/my_project_client.dart';

class AppleAuthService {
  final Client client;
  final SessionManager sessionManager;

  AppleAuthService({
    required this.client,
    required this.sessionManager,
  });

  /// Check if Apple Sign-In is available on this device
  Future<bool> isAvailable() async {
    // Only available on iOS, macOS, and some Android devices
    if (!Platform.isIOS && !Platform.isMacOS) {
      return false;
    }
    return await SignInWithApple.isAvailable();
  }

  /// Sign in with Apple
  Future<UserInfo?> signInWithApple() async {
    try {
      // Request credentials from Apple
      final credential = await SignInWithApple.getAppleIDCredential(
        scopes: [
          AppleIDAuthorizationScopes.email,
          AppleIDAuthorizationScopes.fullName,
        ],
        // For web, you need a redirect URI
        // webAuthenticationOptions: WebAuthenticationOptions(
        //   clientId: 'your.service.id',
        //   redirectUri: Uri.parse('https://your-domain.com/callbacks/sign_in_with_apple'),
        // ),
      );

      // The identity token is what we verify server-side
      final identityToken = credential.identityToken;
      
      if (identityToken == null) {
        throw Exception('Failed to get identity token from Apple');
      }

      // Build the user's name
      // Note: Apple only provides name on FIRST sign-in!
      String? fullName;
      if (credential.givenName != null || credential.familyName != null) {
        final givenName = credential.givenName ?? '';
        final familyName = credential.familyName ?? '';
        fullName = '$givenName $familyName'.trim();
      }

      // Authenticate with Serverpod using the Apple token
      final userInfo = await signInWithIdToken(
        client: client,
        sessionManager: sessionManager,
        idToken: identityToken,
        email: credential.email, // May be null on subsequent logins!
        fullName: fullName,
        userIdentifier: credential.userIdentifier,
      );

      return userInfo;
    } on SignInWithAppleAuthorizationException catch (e) {
      if (e.code == AuthorizationErrorCode.canceled) {
        // User cancelled - not an error
        return null;
      }
      rethrow;
    } catch (e) {
      print('Error signing in with Apple: $e');
      rethrow;
    }
  }

  /// Sign out
  Future<void> signOut() async {
    // Note: There is no Apple-specific sign out
    // We just clear our session
    await sessionManager.signOut();
  }
}

// Server-side configuration:
// You need to configure your Apple credentials in AuthConfig
//
// auth.AuthConfig.set(auth.AuthConfig(
//   // ... other config ...
//   appleTeamId: 'YOUR_TEAM_ID',
//   appleKeyId: 'YOUR_KEY_ID',
//   applePrivateKey: 'YOUR_PRIVATE_KEY_CONTENTS',
//   appleBundleId: 'com.yourcompany.yourapp',
// ));
```
