---
type: "THEORY"
title: "Error Handling in Endpoints"
---

**Proper error handling makes your API robust and user-friendly.**

**Throwing Exceptions**

```dart
Future<User> getUser(Session session, int userId) async {
  final user = await User.db.findById(session, userId);

  if (user == null) {
    // This exception reaches the Flutter client
    throw Exception('User with id $userId not found');
  }

  return user;
}
```

**Custom Exception Types**

Serverpod provides `SerializableException` for type-safe errors:

```dart
// Define in protocol/exceptions.yaml
exception: UserNotFoundException
fields:
  userId: int
  message: String
```

```dart
// Use in your endpoint
Future<User> getUser(Session session, int userId) async {
  final user = await User.db.findById(session, userId);

  if (user == null) {
    throw UserNotFoundException(
      userId: userId,
      message: 'User not found',
    );
  }

  return user;
}
```

**Handling on Client Side**

```dart
// In Flutter
try {
  final user = await client.user.getUser(123);
  print('Got user: ${user.name}');
} on UserNotFoundException catch (e) {
  // Typed exception handling!
  print('User ${e.userId} not found: ${e.message}');
} catch (e) {
  // Generic error handling
  print('Unexpected error: $e');
}
```

