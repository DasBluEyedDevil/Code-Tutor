---
type: "EXAMPLE"
title: "Configuring the Auth Module on the Server"
---

The auth module needs to be configured in your server's main entry point. This connects the authentication endpoints and handlers to your Serverpod server.



```dart
// my_project_server/lib/server.dart

import 'package:serverpod/serverpod.dart';
import 'package:serverpod_auth_server/serverpod_auth_server.dart' as auth;

import 'src/generated/endpoints.dart';
import 'src/generated/protocol.dart';

void run(List<String> args) async {
  // Initialize Serverpod
  final pod = Serverpod(
    args,
    Protocol(),
    Endpoints(),
  );

  // Configure the auth module
  auth.AuthConfig.set(auth.AuthConfig(
    // Enable sending validation emails
    sendValidationEmail: (session, email, validationCode) async {
      // In production, integrate with an email service like SendGrid
      // For development, just print the code
      print('Validation code for $email: $validationCode');
      return true;
    },
    // Enable sending password reset emails
    sendPasswordResetEmail: (session, userInfo, validationCode) async {
      print('Password reset code for ${userInfo.email}: $validationCode');
      return true;
    },
    // Configure password requirements
    minPasswordLength: 8,
    // Allow users to create accounts
    allowUnsecureRandom: false, // Use secure random for tokens
    // Session configuration
    userCanEditUserImage: true,
    userCanEditUserName: true,
    userCanEditFullName: true,
    // Callback when a user is created
    onUserCreated: (session, userInfo) async {
      print('New user created: ${userInfo.email}');
      // You can perform additional setup here
      // Like creating default settings, sending welcome email, etc.
    },
    // Callback when a user signs in
    onUserWillLogin: (session, userInfo) async {
      // Return true to allow login, false to block
      // Useful for checking if user is banned, suspended, etc.
      return true;
    },
  ));

  // Start the server
  await pod.start();
}

// Note: The auth module automatically adds these endpoints:
// - auth.email.createAccount
// - auth.email.signIn
// - auth.signOut
// - auth.email.initiatePasswordReset
// - auth.email.resetPassword
// - auth.getUserInfo
// - auth.status (check session status)
```
