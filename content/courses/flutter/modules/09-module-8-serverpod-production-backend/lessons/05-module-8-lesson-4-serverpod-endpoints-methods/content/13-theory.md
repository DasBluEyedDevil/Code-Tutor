---
type: "THEORY"
title: "Authentication in Endpoints"
---

**Serverpod provides built-in authentication support.**

**Checking if User is Authenticated:**

```dart
Future<User> getCurrentUser(Session session) async {
  // Get the authenticated user's ID
  final userId = await session.auth.authenticatedUserId;

  if (userId == null) {
    throw NotAuthenticatedException();
  }

  final user = await User.db.findById(session, userId);
  if (user == null) {
    throw Exception('User record not found');
  }

  return user;
}
```

**Requiring Authentication:**

```dart
// All methods in this endpoint require authentication
class SecureEndpoint extends Endpoint {
  @override
  bool get requireLogin => true;  // Enforces auth for all methods

  Future<String> getSecretData(Session session) async {
    // Only authenticated users can call this
    return 'Top secret!';
  }
}
```

**Role-Based Access:**

```dart
Future<void> deleteUser(Session session, int userId) async {
  // Check if current user is admin
  final currentUserId = await session.auth.authenticatedUserId;
  final currentUser = await User.db.findById(session, currentUserId!);

  if (currentUser?.role != 'admin') {
    throw UnauthorizedException('Only admins can delete users');
  }

  await User.db.deleteWhere(
    session,
    where: (t) => t.id.equals(userId),
  );
}
```

**Scopes (for fine-grained permissions):**

```dart
@override
Set<Scope> get requiredScopes => {Scope('users:write')};

Future<User> createUser(Session session, User user) async {
  // Only users with 'users:write' scope can call this
  return await User.db.insertRow(session, user);
}
```

