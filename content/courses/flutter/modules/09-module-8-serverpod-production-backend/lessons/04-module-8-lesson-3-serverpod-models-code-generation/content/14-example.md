---
type: "EXAMPLE"
title: "Automatic Serialization in Action"
---

Serverpod handles all JSON conversion automatically. Here's how it works in practice.


```dart
// SERVER SIDE - In an endpoint
import 'package:serverpod/serverpod.dart';

class UserEndpoint extends Endpoint {
  // Return a User - Serverpod serializes to JSON automatically
  Future<User> getUser(Session session, int userId) async {
    final user = await User.db.findById(session, userId);
    if (user == null) {
      throw Exception('User not found');
    }
    return user; // Automatically converted to JSON
  }

  // Accept a User - Serverpod deserializes from JSON automatically
  Future<User> createUser(Session session, User user) async {
    // 'user' is already a User object, deserialized from client JSON
    final savedUser = await User.db.insertRow(session, user);
    return savedUser; // Returned as JSON to client
  }
}

// CLIENT SIDE - In Flutter
import 'package:my_project_client/my_project_client.dart';

class UserService {
  final Client client;

  UserService(this.client);

  Future<User> fetchUser(int userId) async {
    // Serverpod handles JSON deserialization
    // You get a fully typed User object!
    final user = await client.user.getUser(userId);

    print(user.name);      // Typed as String
    print(user.email);     // Typed as String
    print(user.createdAt); // Typed as DateTime

    return user;
  }

  Future<User> createUser(String name, String email) async {
    final newUser = User(
      name: name,
      email: email,
      isActive: true,
      createdAt: DateTime.now(),
    );

    // Serverpod handles JSON serialization
    // newUser is sent as JSON, response comes back as User
    return await client.user.createUser(newUser);
  }
}

// You NEVER write toJson() or fromJson() manually!
// No JSON parsing code anywhere!
// Full type safety from Flutter to PostgreSQL!
```
