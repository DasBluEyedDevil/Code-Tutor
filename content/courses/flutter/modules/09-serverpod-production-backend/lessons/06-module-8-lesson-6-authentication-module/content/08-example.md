---
type: "EXAMPLE"
title: "Implementing Email/Password Auth in Flutter"
---

Here is how to implement email/password authentication in your Flutter app using Serverpod's auth module.



```dart
// lib/services/auth_service.dart

import 'package:serverpod_auth_client/serverpod_auth_client.dart';
import 'package:serverpod_auth_email_flutter/serverpod_auth_email_flutter.dart';
import 'package:my_project_client/my_project_client.dart';

class AuthService {
  final Client client;
  final SessionManager sessionManager;

  AuthService({required this.client, required this.sessionManager});

  /// Check if user is currently signed in
  bool get isSignedIn => sessionManager.isSignedIn;

  /// Get current user info (null if not signed in)
  UserInfo? get currentUser => sessionManager.signedInUser;

  /// Stream of auth state changes
  Stream<SessionManager> get authStateChanges => sessionManager.streamingController.stream;

  /// Create a new account with email and password
  Future<UserInfo?> createAccount({
    required String email,
    required String password,
    required String userName,
  }) async {
    try {
      // Create the account
      final userInfo = await EmailAccountController.createAccount(
        client: client,
        userName: userName,
        email: email,
        password: password,
      );

      if (userInfo != null) {
        // Account created successfully
        // User needs to verify email before signing in
        return userInfo;
      }
      return null;
    } catch (e) {
      print('Error creating account: $e');
      rethrow;
    }
  }

  /// Sign in with email and password
  Future<UserInfo?> signIn({
    required String email,
    required String password,
  }) async {
    try {
      final userInfo = await EmailAccountController.signIn(
        client: client,
        email: email,
        password: password,
      );

      if (userInfo != null) {
        // Sign in successful
        // Session is automatically managed by SessionManager
        return userInfo;
      }
      return null;
    } catch (e) {
      print('Error signing in: $e');
      rethrow;
    }
  }

  /// Sign out the current user
  Future<void> signOut() async {
    await sessionManager.signOut();
  }

  /// Verify email with the code sent to user
  Future<bool> verifyEmail({
    required String email,
    required String verificationCode,
  }) async {
    try {
      final result = await EmailAccountController.verifyEmail(
        client: client,
        email: email,
        verificationCode: verificationCode,
      );
      return result;
    } catch (e) {
      print('Error verifying email: $e');
      return false;
    }
  }

  /// Initiate password reset (sends email with code)
  Future<bool> initiatePasswordReset(String email) async {
    try {
      final result = await EmailAccountController.initiatePasswordReset(
        client: client,
        email: email,
      );
      return result;
    } catch (e) {
      print('Error initiating password reset: $e');
      return false;
    }
  }

  /// Complete password reset with verification code
  Future<bool> resetPassword({
    required String email,
    required String verificationCode,
    required String newPassword,
  }) async {
    try {
      final result = await EmailAccountController.resetPassword(
        client: client,
        email: email,
        verificationCode: verificationCode,
        password: newPassword,
      );
      return result;
    } catch (e) {
      print('Error resetting password: $e');
      return false;
    }
  }
}
```
