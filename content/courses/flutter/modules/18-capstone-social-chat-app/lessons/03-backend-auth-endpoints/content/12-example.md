---
type: "EXAMPLE"
title: "Profile Management Endpoints"
---


**Complete Profile Management Implementation**



```dart
// server/lib/src/endpoints/profile_endpoint.dart
import 'package:serverpod/serverpod.dart';
import 'package:serverpod_auth_server/serverpod_auth_server.dart' as auth;
import '../generated/protocol.dart';

/// Profile management endpoints
class ProfileEndpoint extends Endpoint {
  
  /// Update user profile
  Future<UserProfile> updateProfile(
    Session session, {
    String? displayName,
    String? bio,
    String? avatarUrl,
  }) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw AuthException(
        code: AuthErrorCode.unauthenticated,
        message: 'Please log in to update your profile',
      );
    }
    
    final profile = await UserProfile.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(userId),
    );
    
    if (profile == null) {
      throw AuthException(
        code: AuthErrorCode.userNotFound,
        message: 'Profile not found',
      );
    }
    
    // Validate inputs
    if (displayName != null) {
      if (displayName.trim().isEmpty || displayName.length > 50) {
        throw AuthException(
          code: AuthErrorCode.invalidInput,
          message: 'Display name must be 1-50 characters',
        );
      }
    }
    
    if (bio != null && bio.length > 500) {
      throw AuthException(
        code: AuthErrorCode.invalidInput,
        message: 'Bio must be 500 characters or less',
      );
    }
    
    // Update profile
    final updated = profile.copyWith(
      displayName: displayName ?? profile.displayName,
      bio: bio ?? profile.bio,
      avatarUrl: avatarUrl ?? profile.avatarUrl,
      updatedAt: DateTime.now(),
    );
    
    return UserProfile.db.updateRow(session, updated);
  }
  
  /// Change username (rate limited)
  Future<UserProfile> changeUsername(
    Session session, {
    required String newUsername,
  }) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw AuthException(
        code: AuthErrorCode.unauthenticated,
        message: 'Please log in',
      );
    }
    
    // Validate format
    if (!RegExp(r'^[a-zA-Z0-9_]{3,30}$').hasMatch(newUsername)) {
      throw AuthException(
        code: AuthErrorCode.invalidUsername,
        message: 'Username must be 3-30 alphanumeric characters',
      );
    }
    
    final profile = await UserProfile.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(userId),
    );
    
    if (profile == null) {
      throw AuthException(
        code: AuthErrorCode.userNotFound,
        message: 'Profile not found',
      );
    }
    
    // Check if username is taken
    final existing = await UserProfile.db.findFirstRow(
      session,
      where: (t) => t.username.equals(newUsername.toLowerCase()),
    );
    
    if (existing != null && existing.id != profile.id) {
      throw AuthException(
        code: AuthErrorCode.usernameTaken,
        message: 'This username is already taken',
      );
    }
    
    // TODO: Check rate limit (e.g., once per 30 days)
    
    return UserProfile.db.updateRow(
      session,
      profile.copyWith(
        username: newUsername.toLowerCase(),
        updatedAt: DateTime.now(),
      ),
    );
  }
  
  /// Change password
  Future<bool> changePassword(
    Session session, {
    required String currentPassword,
    required String newPassword,
  }) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw AuthException(
        code: AuthErrorCode.unauthenticated,
        message: 'Please log in',
      );
    }
    
    // Get user info
    final userInfo = await auth.Users.findUserByUserId(session, userId);
    if (userInfo == null || userInfo.email == null) {
      throw AuthException(
        code: AuthErrorCode.userNotFound,
        message: 'User not found',
      );
    }
    
    // Verify current password
    final verified = await auth.Emails.authenticate(
      session,
      userInfo.email!,
      currentPassword,
    );
    
    if (verified == null) {
      throw AuthException(
        code: AuthErrorCode.invalidCredentials,
        message: 'Current password is incorrect',
      );
    }
    
    // Validate new password
    _validatePassword(newPassword);
    
    // Update password
    await auth.Emails.changePassword(
      session,
      userId,
      currentPassword,
      newPassword,
    );
    
    // TODO: Invalidate all other sessions
    // TODO: Send email notification about password change
    
    return true;
  }
  
  /// Request password reset
  Future<bool> requestPasswordReset(
    Session session, {
    required String email,
  }) async {
    // Don't reveal if email exists
    final userInfo = await auth.Users.findUserByEmail(session, email);
    
    if (userInfo != null) {
      await auth.Emails.initiatePasswordReset(session, userInfo);
    }
    
    // Always return true to prevent enumeration
    return true;
  }
  
  /// Reset password with code
  Future<bool> resetPassword(
    Session session, {
    required String email,
    required String resetCode,
    required String newPassword,
  }) async {
    _validatePassword(newPassword);
    
    final result = await auth.Emails.resetPassword(
      session,
      email,
      resetCode,
      newPassword,
    );
    
    if (!result) {
      throw AuthException(
        code: AuthErrorCode.invalidResetCode,
        message: 'Invalid or expired reset code',
      );
    }
    
    return true;
  }
  
  /// Request account deletion
  Future<AccountDeletionRequest> requestAccountDeletion(
    Session session, {
    required String password,
  }) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw AuthException(
        code: AuthErrorCode.unauthenticated,
        message: 'Please log in',
      );
    }
    
    // Verify password
    final userInfo = await auth.Users.findUserByUserId(session, userId);
    if (userInfo?.email == null) {
      throw AuthException(
        code: AuthErrorCode.userNotFound,
        message: 'User not found',
      );
    }
    
    final verified = await auth.Emails.authenticate(
      session,
      userInfo!.email!,
      password,
    );
    
    if (verified == null) {
      throw AuthException(
        code: AuthErrorCode.invalidCredentials,
        message: 'Password is incorrect',
      );
    }
    
    // Schedule deletion (30 day grace period)
    final deletionDate = DateTime.now().add(Duration(days: 30));
    
    // Mark profile for deletion
    final profile = await UserProfile.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(userId),
    );
    
    if (profile != null) {
      await UserProfile.db.updateRow(
        session,
        profile.copyWith(
          isDeleted: true,
          updatedAt: DateTime.now(),
        ),
      );
    }
    
    // Sign out all sessions
    await session.auth.signOut();
    
    // TODO: Send confirmation email
    // TODO: Store deletion request for scheduled job
    
    return AccountDeletionRequest(
      userId: userId,
      requestedAt: DateTime.now(),
      scheduledDeletionAt: deletionDate,
      canCancelUntil: deletionDate,
    );
  }
  
  /// Cancel account deletion
  Future<bool> cancelAccountDeletion(
    Session session, {
    required String email,
    required String password,
  }) async {
    // Verify credentials
    final authResult = await auth.Emails.authenticate(
      session,
      email,
      password,
    );
    
    if (authResult == null) {
      throw AuthException(
        code: AuthErrorCode.invalidCredentials,
        message: 'Invalid credentials',
      );
    }
    
    final userInfo = await auth.Users.findUserByEmail(session, email);
    if (userInfo == null) {
      return false;
    }
    
    // Unmark for deletion
    final profile = await UserProfile.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(userInfo.id!),
    );
    
    if (profile != null && profile.isDeleted) {
      await UserProfile.db.updateRow(
        session,
        profile.copyWith(
          isDeleted: false,
          updatedAt: DateTime.now(),
        ),
      );
    }
    
    return true;
  }
  
  /// Export user data (GDPR compliance)
  Future<UserDataExport> exportUserData(Session session) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw AuthException(
        code: AuthErrorCode.unauthenticated,
        message: 'Please log in',
      );
    }
    
    final profile = await UserProfile.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(userId),
    );
    
    if (profile == null) {
      throw AuthException(
        code: AuthErrorCode.userNotFound,
        message: 'Profile not found',
      );
    }
    
    // Gather all user data
    final posts = await Post.db.find(
      session,
      where: (t) => t.authorId.equals(profile.id!),
    );
    
    final comments = await Comment.db.find(
      session,
      where: (t) => t.authorId.equals(profile.id!),
    );
    
    final messages = await Message.db.find(
      session,
      where: (t) => t.senderId.equals(profile.id!),
    );
    
    return UserDataExport(
      exportedAt: DateTime.now(),
      profile: profile,
      posts: posts,
      comments: comments,
      messages: messages,
    );
  }
  
  void _validatePassword(String password) {
    if (password.length < 8) {
      throw AuthException(
        code: AuthErrorCode.weakPassword,
        message: 'Password must be at least 8 characters',
      );
    }
    
    if (!RegExp(r'[A-Z]').hasMatch(password) ||
        !RegExp(r'[a-z]').hasMatch(password) ||
        !RegExp(r'[0-9]').hasMatch(password)) {
      throw AuthException(
        code: AuthErrorCode.weakPassword,
        message: 'Password must contain uppercase, lowercase, and numbers',
      );
    }
  }
}
```
