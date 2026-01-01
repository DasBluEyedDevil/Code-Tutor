---
type: "EXAMPLE"
title: "Creating Protected Endpoints"
---

Here is a complete example of an endpoint with both public and protected methods.



```dart
// my_project_server/lib/src/endpoints/profile_endpoint.dart

import 'package:serverpod/serverpod.dart';
import 'package:serverpod_auth_server/serverpod_auth_server.dart';
import '../generated/protocol.dart';

class ProfileEndpoint extends Endpoint {
  /// Get public profile information for any user
  /// This is a PUBLIC endpoint - no authentication required
  Future<PublicProfile?> getPublicProfile(Session session, int userId) async {
    // Anyone can view public profiles
    final user = await Users.findUserByUserId(session, userId);
    
    if (user == null) {
      return null;
    }

    return PublicProfile(
      userId: user.id!,
      userName: user.userName,
      imageUrl: user.imageUrl,
      // Note: We do NOT include email here - that is private
    );
  }

  /// Get the current user's full profile
  /// This is a PROTECTED endpoint - requires authentication
  Future<UserProfile> getMyProfile(Session session) async {
    // Check authentication
    final userId = await session.auth.authenticatedUserId;
    
    if (userId == null) {
      throw Exception('You must be signed in to view your profile');
    }

    // Get the full user info
    final userInfo = await Users.findUserByUserId(session, userId);
    
    if (userInfo == null) {
      throw StateError('User not found in database');
    }

    return UserProfile(
      userId: userInfo.id!,
      email: userInfo.email,
      userName: userInfo.userName,
      fullName: userInfo.fullName,
      imageUrl: userInfo.imageUrl,
      createdAt: userInfo.created,
    );
  }

  /// Update the current user's profile
  /// This is a PROTECTED endpoint
  Future<UserProfile> updateMyProfile(
    Session session, {
    String? userName,
    String? fullName,
  }) async {
    // Check authentication
    final userId = await session.auth.authenticatedUserId;
    
    if (userId == null) {
      throw Exception('You must be signed in to update your profile');
    }

    // Update basic user info if provided
    if (userName != null || fullName != null) {
      final userInfo = await Users.findUserByUserId(session, userId);
      if (userInfo != null) {
        await Users.updateUserInfo(
          session,
          userInfo.id!,
          userName: userName ?? userInfo.userName,
          fullName: fullName ?? userInfo.fullName,
        );
      }
    }

    // Return the updated profile
    return await getMyProfile(session);
  }
}
```
