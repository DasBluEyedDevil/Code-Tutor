---
type: "EXAMPLE"
title: "Update Operations"
---

Here is how to modify existing records:



```dart
import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';

class UserEndpoint extends Endpoint {
  
  /// Update a single user
  Future<User> updateUser(Session session, User user) async {
    // The user MUST have an id to be updated
    if (user.id == null) {
      throw ArgumentError('Cannot update user without id');
    }
    
    // Verify the user exists
    final existingUser = await User.db.findById(session, user.id!);
    if (existingUser == null) {
      throw Exception('User not found');
    }
    
    // updateRow replaces the entire row with the new values
    final updatedUser = await User.db.updateRow(session, user);
    
    return updatedUser;
  }
  
  /// Update only specific fields (patch update pattern)
  Future<User> updateUserProfile(
    Session session,
    int userId,
    String? displayName,
    String? bio,
    String? avatarUrl,
  ) async {
    // First, fetch the existing user
    final existingUser = await User.db.findById(session, userId);
    if (existingUser == null) {
      throw Exception('User not found');
    }
    
    // Use copyWith to update only specified fields
    final updatedUser = existingUser.copyWith(
      displayName: displayName ?? existingUser.displayName,
      bio: bio ?? existingUser.bio,
      avatarUrl: avatarUrl ?? existingUser.avatarUrl,
    );
    
    return await User.db.updateRow(session, updatedUser);
  }
  
  /// Record a login (update lastLoginAt)
  Future<void> recordLogin(Session session, int userId) async {
    final user = await User.db.findById(session, userId);
    if (user == null) return;
    
    await User.db.updateRow(
      session,
      user.copyWith(lastLoginAt: DateTime.now()),
    );
  }
  
  /// Batch update: Deactivate all unverified users older than 30 days
  Future<int> deactivateOldUnverifiedUsers(Session session) async {
    final cutoffDate = DateTime.now().subtract(Duration(days: 30));
    
    // Find users to deactivate
    final usersToDeactivate = await User.db.find(
      session,
      where: (t) => 
        t.isVerified.equals(false) & 
        t.createdAt.lessThan(cutoffDate) &
        t.isActive.equals(true),
    );
    
    // Update each one
    for (final user in usersToDeactivate) {
      await User.db.updateRow(
        session,
        user.copyWith(isActive: false),
      );
    }
    
    return usersToDeactivate.length;
  }
}
```
