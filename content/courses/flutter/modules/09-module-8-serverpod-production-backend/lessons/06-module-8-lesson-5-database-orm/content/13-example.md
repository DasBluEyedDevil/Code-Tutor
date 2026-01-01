---
type: "EXAMPLE"
title: "Read Operations"
---

Here is how to query records from the database:



```dart
import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';

class UserEndpoint extends Endpoint {
  
  /// Find a user by their ID
  Future<User?> getUser(Session session, int userId) async {
    // findById returns null if not found
    return await User.db.findById(session, userId);
  }
  
  /// Find all active users with pagination
  Future<List<User>> getActiveUsers(
    Session session, {
    int limit = 20,
    int offset = 0,
  }) async {
    return await User.db.find(
      session,
      where: (t) => t.isActive.equals(true),
      limit: limit,
      offset: offset,
      orderBy: (t) => t.createdAt,
      orderDescending: true, // Newest first
    );
  }
  
  /// Find a user by email (unique field)
  Future<User?> getUserByEmail(Session session, String email) async {
    // findFirstRow returns the first match or null
    return await User.db.findFirstRow(
      session,
      where: (t) => t.email.equals(email),
    );
  }
  
  /// Count users matching a condition
  Future<int> countVerifiedUsers(Session session) async {
    return await User.db.count(
      session,
      where: (t) => t.isVerified.equals(true),
    );
  }
  
  /// Search users by username (partial match)
  Future<List<User>> searchUsers(Session session, String query) async {
    return await User.db.find(
      session,
      where: (t) => t.username.ilike('%$query%'), // Case-insensitive LIKE
      limit: 50,
    );
  }
}
```
