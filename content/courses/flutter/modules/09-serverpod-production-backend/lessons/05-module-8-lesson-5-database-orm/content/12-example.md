---
type: "EXAMPLE"
title: "Create Operations"
---

Here is how to insert new records into the database:



```dart
import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';

class UserEndpoint extends Endpoint {
  
  /// Insert a single user
  Future<User> createUser(Session session, User user) async {
    // The user object comes from the client
    // We set server-controlled fields here
    final userToInsert = user.copyWith(
      createdAt: DateTime.now(),
      isActive: true,
      isVerified: false,
      postCount: 0,
      followerCount: 0,
    );
    
    // insertRow returns the user WITH the generated id
    final savedUser = await User.db.insertRow(session, userToInsert);
    
    session.log('Created user: ${savedUser.id} (${savedUser.email})');
    
    return savedUser;
  }
  
  /// Insert multiple users at once (batch insert)
  Future<List<User>> createUsers(Session session, List<User> users) async {
    // Prepare all users with server-controlled fields
    final now = DateTime.now();
    final usersToInsert = users.map((u) => u.copyWith(
      createdAt: now,
      isActive: true,
      isVerified: false,
      postCount: 0,
      followerCount: 0,
    )).toList();
    
    // Batch insert is more efficient than multiple insertRow calls
    final savedUsers = await User.db.insert(session, usersToInsert);
    
    session.log('Created ${savedUsers.length} users');
    
    return savedUsers;
  }
}
```
