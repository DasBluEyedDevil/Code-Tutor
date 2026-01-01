---
type: "EXAMPLE"
title: "Implementing Google Sign-In"
---

Here is how to implement Google Sign-In in your Flutter app.



```dart
// lib/services/google_auth_service.dart

import 'package:google_sign_in/google_sign_in.dart';
import 'package:serverpod_auth_google_flutter/serverpod_auth_google_flutter.dart';
import 'package:my_project_client/my_project_client.dart';

class GoogleAuthService {
  final Client client;
  final SessionManager sessionManager;
  
  // Configure Google Sign-In
  // The serverClientId is from your Google Cloud Console
  final GoogleSignIn _googleSignIn = GoogleSignIn(
    scopes: [
      'email',
      'profile',
    ],
    // Add your web client ID for server-side verification
    serverClientId: 'YOUR_WEB_CLIENT_ID.apps.googleusercontent.com',
  );

  GoogleAuthService({
    required this.client,
    required this.sessionManager,
  });

  /// Sign in with Google
  Future<UserInfo?> signInWithGoogle() async {
    try {
      // Trigger the Google sign-in flow
      final googleUser = await _googleSignIn.signIn();
      
      if (googleUser == null) {
        // User cancelled the sign-in
        return null;
      }

      // Get the authentication details
      final googleAuth = await googleUser.authentication;

      // The ID token is what we send to our server
      final idToken = googleAuth.idToken;
      
      if (idToken == null) {
        throw Exception('Failed to get ID token from Google');
      }

      // Authenticate with Serverpod using the Google token
      final userInfo = await signInWithIdToken(
        client: client,
        sessionManager: sessionManager,
        idToken: idToken,
        email: googleUser.email,
        fullName: googleUser.displayName,
        imageUrl: googleUser.photoUrl,
      );

      return userInfo;
    } catch (e) {
      print('Error signing in with Google: $e');
      rethrow;
    }
  }

  /// Sign out from Google (also signs out from the app)
  Future<void> signOut() async {
    await _googleSignIn.signOut();
    await sessionManager.signOut();
  }

  /// Check if user is signed in with Google
  Future<bool> isSignedIn() async {
    return await _googleSignIn.isSignedIn();
  }
}

// Server-side configuration (in your Serverpod server)
// Add to your AuthConfig setup:
//
// auth.AuthConfig.set(auth.AuthConfig(
//   // ... other config ...
//   googleClientId: 'YOUR_WEB_CLIENT_ID.apps.googleusercontent.com',
// ));
```
