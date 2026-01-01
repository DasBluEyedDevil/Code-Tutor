---
type: "EXAMPLE"
title: "Custom Registration Endpoint"
---


**Extended Registration with Validation**

Create a custom endpoint that wraps the built-in registration:



```dart
// server/lib/src/endpoints/auth_endpoint.dart
import 'package:serverpod/serverpod.dart';
import 'package:serverpod_auth_server/serverpod_auth_server.dart' as auth;
import '../generated/protocol.dart';

/// Custom authentication endpoints with extended validation
class AuthEndpoint extends Endpoint {
  
  /// Register a new user with email and password
  /// 
  /// Returns the created user profile on success.
  /// Throws [AuthException] on validation failures.
  Future<AuthResponse> registerWithEmail(
    Session session, {
    required String email,
    required String password,
    required String displayName,
    String? username,
  }) async {
    // 1. Validate input
    _validateEmail(email);
    _validatePassword(password);
    _validateDisplayName(displayName);
    
    if (username != null) {
      await _validateUsername(session, username);
    }
    
    // 2. Check if email is already registered
    final existingUser = await auth.Users.findUserByEmail(session, email);
    if (existingUser != null) {
      throw AuthException(
        code: AuthErrorCode.emailAlreadyExists,
        message: 'An account with this email already exists',
      );
    }
    
    // 3. Create the user via Serverpod auth
    final authResult = await auth.Emails.createUser(
      session,
      email,
      password,
    );
    
    if (authResult == null) {
      throw AuthException(
        code: AuthErrorCode.registrationFailed,
        message: 'Failed to create account. Please try again.',
      );
    }
    
    // 4. Fetch the created UserInfo
    final userInfo = await auth.Users.findUserByEmail(session, email);
    if (userInfo == null) {
      throw AuthException(
        code: AuthErrorCode.registrationFailed,
        message: 'Account created but user not found',
      );
    }
    
    // 5. Create our custom UserProfile
    final generatedUsername = username ?? _generateUsername(email);
    final profile = UserProfile(
      userInfoId: userInfo.id!,
      username: generatedUsername,
      displayName: displayName,
      email: email,
      isOnline: false,
      isVerified: false,  // Will be true after email verification
      isDeleted: false,
      createdAt: DateTime.now(),
    );
    
    final savedProfile = await UserProfile.db.insertRow(session, profile);
    
    // 6. Create default settings
    await UserSettings.db.insertRow(session, UserSettings(
      userProfileId: savedProfile.id!,
      pushNotificationsEnabled: true,
      emailNotificationsEnabled: true,
      messagePreviewsEnabled: true,
      showOnlineStatus: true,
      showLastSeen: true,
      allowDirectMessages: 'everyone',
      theme: 'system',
      language: 'en',
    ));
    
    // 7. Return success response
    return AuthResponse(
      success: true,
      userProfile: savedProfile,
      message: 'Account created. Please check your email for verification.',
    );
  }
  
  /// Resend verification email
  Future<bool> resendVerificationEmail(
    Session session, {
    required String email,
  }) async {
    final userInfo = await auth.Users.findUserByEmail(session, email);
    
    if (userInfo == null) {
      // Don't reveal if email exists
      return true;
    }
    
    if (userInfo.email != null) {
      // This triggers sendValidationEmail callback
      await auth.Emails.initiateEmailVerification(session, userInfo);
    }
    
    return true;
  }
  
  /// Verify email with code
  Future<AuthResponse> verifyEmail(
    Session session, {
    required String email,
    required String code,
  }) async {
    final result = await auth.Emails.verifyEmailValidationCode(
      session,
      email,
      code,
    );
    
    if (result == null) {
      throw AuthException(
        code: AuthErrorCode.invalidVerificationCode,
        message: 'Invalid or expired verification code',
      );
    }
    
    // Update our UserProfile
    final profile = await UserProfile.db.findFirstRow(
      session,
      where: (t) => t.email.equals(email),
    );
    
    if (profile != null) {
      await UserProfile.db.updateRow(
        session,
        profile.copyWith(
          isVerified: true,
          updatedAt: DateTime.now(),
        ),
      );
    }
    
    return AuthResponse(
      success: true,
      userProfile: profile,
      message: 'Email verified successfully',
    );
  }
  
  // Validation helpers
  
  void _validateEmail(String email) {
    final emailRegex = RegExp(
      r'^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$',
    );
    
    if (!emailRegex.hasMatch(email)) {
      throw AuthException(
        code: AuthErrorCode.invalidEmail,
        message: 'Please enter a valid email address',
      );
    }
  }
  
  void _validatePassword(String password) {
    if (password.length < 8) {
      throw AuthException(
        code: AuthErrorCode.weakPassword,
        message: 'Password must be at least 8 characters',
      );
    }
    
    if (!RegExp(r'[A-Z]').hasMatch(password)) {
      throw AuthException(
        code: AuthErrorCode.weakPassword,
        message: 'Password must contain an uppercase letter',
      );
    }
    
    if (!RegExp(r'[a-z]').hasMatch(password)) {
      throw AuthException(
        code: AuthErrorCode.weakPassword,
        message: 'Password must contain a lowercase letter',
      );
    }
    
    if (!RegExp(r'[0-9]').hasMatch(password)) {
      throw AuthException(
        code: AuthErrorCode.weakPassword,
        message: 'Password must contain a number',
      );
    }
  }
  
  void _validateDisplayName(String name) {
    if (name.trim().isEmpty) {
      throw AuthException(
        code: AuthErrorCode.invalidInput,
        message: 'Display name is required',
      );
    }
    
    if (name.length > 50) {
      throw AuthException(
        code: AuthErrorCode.invalidInput,
        message: 'Display name must be 50 characters or less',
      );
    }
  }
  
  Future<void> _validateUsername(Session session, String username) async {
    // Check format
    if (!RegExp(r'^[a-zA-Z0-9_]{3,30}$').hasMatch(username)) {
      throw AuthException(
        code: AuthErrorCode.invalidUsername,
        message: 'Username must be 3-30 alphanumeric characters or underscores',
      );
    }
    
    // Check availability
    final existing = await UserProfile.db.findFirstRow(
      session,
      where: (t) => t.username.equals(username.toLowerCase()),
    );
    
    if (existing != null) {
      throw AuthException(
        code: AuthErrorCode.usernameTaken,
        message: 'This username is already taken',
      );
    }
  }
  
  String _generateUsername(String email) {
    final base = email.split('@').first.replaceAll(RegExp(r'[^a-zA-Z0-9]'), '');
    final suffix = DateTime.now().millisecondsSinceEpoch % 10000;
    return '${base.toLowerCase()}_$suffix';
  }
}
```
