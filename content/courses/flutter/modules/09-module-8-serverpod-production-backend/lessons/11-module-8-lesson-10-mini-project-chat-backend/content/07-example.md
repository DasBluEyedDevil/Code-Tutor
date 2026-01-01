---
type: "EXAMPLE"
title: "Step 5: Implement UserEndpoint"
---

The UserEndpoint handles user profiles and integrates with Serverpod's authentication system.



```dart
// File: lib/src/endpoints/user_endpoint.dart

import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';

class UserEndpoint extends Endpoint {
  /// Create a chat user profile after Serverpod authentication.
  /// Called after successful signup to create the extended profile.
  Future<ChatUser> createProfile(
    Session session, {
    required String username,
    String? displayName,
    String? bio,
  }) async {
    // Get authenticated user from Serverpod auth
    final authUserId = await session.auth.authenticatedUserId;
    if (authUserId == null) {
      throw AuthenticationRequiredException();
    }
    
    // Check if profile already exists
    final existing = await ChatUser.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(authUserId),
    );
    
    if (existing != null) {
      throw InvalidParameterException('Profile already exists');
    }
    
    // Validate username uniqueness
    final usernameExists = await ChatUser.db.findFirstRow(
      session,
      where: (t) => t.username.equals(username.toLowerCase()),
    );
    
    if (usernameExists != null) {
      throw InvalidParameterException('Username already taken');
    }
    
    // Create the chat user profile
    final chatUser = ChatUser(
      userInfoId: authUserId,
      username: username.toLowerCase(),
      displayName: displayName ?? username,
      bio: bio,
      isOnline: true,
      lastSeenAt: DateTime.now(),
      createdAt: DateTime.now(),
    );
    
    return await ChatUser.db.insertRow(session, chatUser);
  }
  
  /// Get the current user's profile.
  Future<ChatUser?> getMyProfile(Session session) async {
    final authUserId = await session.auth.authenticatedUserId;
    if (authUserId == null) {
      throw AuthenticationRequiredException();
    }
    
    return await ChatUser.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(authUserId),
    );
  }
  
  /// Get a user's profile by ID.
  Future<ChatUser?> getUserById(Session session, int userId) async {
    await _requireAuth(session);
    return await ChatUser.db.findById(session, userId);
  }
  
  /// Update the current user's profile.
  Future<ChatUser> updateProfile(
    Session session, {
    String? displayName,
    String? bio,
    String? avatarUrl,
  }) async {
    final authUserId = await session.auth.authenticatedUserId;
    if (authUserId == null) {
      throw AuthenticationRequiredException();
    }
    
    final user = await ChatUser.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(authUserId),
    );
    
    if (user == null) {
      throw NotFoundException('Profile not found');
    }
    
    // Update fields if provided
    final updated = user.copyWith(
      displayName: displayName ?? user.displayName,
      bio: bio ?? user.bio,
      avatarUrl: avatarUrl ?? user.avatarUrl,
    );
    
    return await ChatUser.db.updateRow(session, updated);
  }
  
  /// Search for users by username.
  Future<List<ChatUser>> searchUsers(
    Session session,
    String query, {
    int limit = 20,
  }) async {
    await _requireAuth(session);
    
    if (query.length < 2) {
      return []; // Require at least 2 characters
    }
    
    return await ChatUser.db.find(
      session,
      where: (t) => t.username.ilike('%${query.toLowerCase()}%'),
      limit: limit,
      orderBy: (t) => t.username,
    );
  }
  
  /// Update online status (called from streaming endpoint).
  Future<void> setOnlineStatus(
    Session session,
    bool isOnline,
  ) async {
    final authUserId = await session.auth.authenticatedUserId;
    if (authUserId == null) return;
    
    final user = await ChatUser.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(authUserId),
    );
    
    if (user != null) {
      final updated = user.copyWith(
        isOnline: isOnline,
        lastSeenAt: DateTime.now(),
      );
      await ChatUser.db.updateRow(session, updated);
    }
  }
  
  // Helper to require authentication
  Future<int> _requireAuth(Session session) async {
    final authUserId = await session.auth.authenticatedUserId;
    if (authUserId == null) {
      throw AuthenticationRequiredException();
    }
    return authUserId;
  }
}

// Custom exceptions
class AuthenticationRequiredException implements Exception {
  @override
  String toString() => 'Authentication required';
}

class NotFoundException implements Exception {
  final String message;
  NotFoundException(this.message);
  
  @override
  String toString() => message;
}

class InvalidParameterException implements Exception {
  final String message;
  InvalidParameterException(this.message);
  
  @override
  String toString() => message;
}
```
