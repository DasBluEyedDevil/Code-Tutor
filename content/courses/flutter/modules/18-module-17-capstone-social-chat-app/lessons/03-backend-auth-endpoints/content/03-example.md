---
type: "EXAMPLE"
title: "Configuring Auth Module"
---


**Server Configuration**

Configure the auth module in your server setup:



```dart
// server/lib/src/server.dart
import 'package:serverpod/serverpod.dart';
import 'package:serverpod_auth_server/serverpod_auth_server.dart' as auth;

import 'generated/protocol.dart';
import 'generated/endpoints.dart';

void run(List<String> args) async {
  // Initialize Serverpod
  final pod = Serverpod(
    args,
    Protocol(),
    Endpoints(),
  );
  
  // Configure authentication
  auth.AuthConfig.set(auth.AuthConfig(
    // Require email verification before login
    sendValidationEmail: (session, email, validationCode) async {
      // TODO: Integrate with email service (SendGrid, AWS SES, etc.)
      print('Verification code for $email: $validationCode');
      // In production, send actual email:
      // await EmailService.sendVerificationEmail(email, validationCode);
      return true;
    },
    
    // Password reset email
    sendPasswordResetEmail: (session, userInfo, validationCode) async {
      print('Password reset for ${userInfo.email}: $validationCode');
      // await EmailService.sendPasswordResetEmail(userInfo.email!, validationCode);
      return true;
    },
    
    // Password requirements
    minPasswordLength: 8,
    
    // Session configuration
    userInfoCacheLifetime: Duration(minutes: 5),
    
    // Callback when new user is created
    onUserCreated: (session, userInfo) async {
      // Create our custom UserProfile for the new user
      await _createUserProfile(session, userInfo);
    },
    
    // Callback on successful login
    onUserWillLogin: (session, userInfo) async {
      // Update last login timestamp
      await _updateLastLogin(session, userInfo);
      return true; // Allow login
    },
  ));
  
  // Start the server
  await pod.start();
}

/// Creates a UserProfile when a new auth user is created
Future<void> _createUserProfile(
  Session session, 
  auth.UserInfo userInfo,
) async {
  final profile = UserProfile(
    userInfoId: userInfo.id!,
    username: _generateUsername(userInfo),
    displayName: userInfo.fullName ?? userInfo.email?.split('@').first ?? 'User',
    email: userInfo.email ?? '',
    isOnline: false,
    isVerified: userInfo.email != null,
    isDeleted: false,
    createdAt: DateTime.now(),
  );
  
  await UserProfile.db.insertRow(session, profile);
  
  // Create default settings
  final settings = UserSettings(
    userProfileId: profile.id!,
    pushNotificationsEnabled: true,
    emailNotificationsEnabled: true,
    messagePreviewsEnabled: true,
    showOnlineStatus: true,
    showLastSeen: true,
    allowDirectMessages: 'everyone',
    theme: 'system',
    language: 'en',
  );
  
  await UserSettings.db.insertRow(session, settings);
}

/// Generates a unique username from user info
String _generateUsername(auth.UserInfo userInfo) {
  final base = userInfo.email?.split('@').first ?? 
               userInfo.fullName?.toLowerCase().replaceAll(' ', '') ??
               'user';
  final suffix = DateTime.now().millisecondsSinceEpoch % 10000;
  return '${base}_$suffix';
}

/// Updates last login timestamp
Future<void> _updateLastLogin(
  Session session, 
  auth.UserInfo userInfo,
) async {
  final profile = await UserProfile.db.findFirstRow(
    session,
    where: (t) => t.userInfoId.equals(userInfo.id!),
  );
  
  if (profile != null) {
    await UserProfile.db.updateRow(
      session,
      profile.copyWith(
        isOnline: true,
        updatedAt: DateTime.now(),
      ),
    );
  }
}
```
