---
type: "EXAMPLE"
title: "Serverpod Approach - Clean and Type-Safe"
---


Now see how Serverpod simplifies this:



```dart
// Serverpod approach - clean, type-safe, generated
import 'package:my_app_client/my_app_client.dart';

class UserService {
  final Client client;
  
  UserService(this.client);
  
  Future<User?> getUser(int id) async {
    // That's it! One line!
    // - No URL construction
    // - No JSON parsing
    // - No manual type conversion
    // - User class is shared with server
    // - Full IDE autocomplete
    return await client.user.getUser(id);
  }
}

// The User class comes from the generated client package.
// It is the SAME class used on the server.
// If the server changes User, this code gets compile-time errors.

// Want to create a user? Just as simple:
Future<User> createUser(String name, String email) async {
  final user = User(
    name: name,
    email: email,
    createdAt: DateTime.now(),
  );
  return await client.user.createUser(user);
}

// Search users? Same pattern:
Future<List<User>> searchUsers(String query) async {
  return await client.user.searchUsers(query);
}
```
