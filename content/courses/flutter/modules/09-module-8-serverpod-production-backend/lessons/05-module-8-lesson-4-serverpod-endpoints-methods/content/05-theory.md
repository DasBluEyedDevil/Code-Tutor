---
type: "THEORY"
title: "The Session Parameter"
---

**Every endpoint method receives a Session as its first parameter.**

The Session is your gateway to everything:

**1. Database Access**

```dart
Future<User?> getUser(Session session, int id) async {
  // session provides database connection
  return await User.db.findById(session, id);
}
```

**2. Authentication Info**

```dart
Future<User?> getCurrentUser(Session session) async {
  // Get the authenticated user's ID
  final userId = await session.auth.authenticatedUserId;

  if (userId == null) {
    throw Exception('Not authenticated');
  }

  return await User.db.findById(session, userId);
}
```

**3. Logging**

```dart
Future<void> processOrder(Session session, int orderId) async {
  session.log('Processing order: $orderId');

  // ... process the order ...

  session.log('Order $orderId completed');
}
```

**4. Server Configuration**

```dart
Future<String> getServerInfo(Session session) async {
  final serverId = session.server.serverId;
  return 'Running on server: $serverId';
}
```

**5. Message Passing (for real-time features)**

```dart
Future<void> broadcastMessage(Session session, String message) async {
  // Send to all connected clients
  session.messages.postMessage('chat', message);
}
```

**Key Rule:** Always accept Session as the first parameter. Serverpod provides it automatically.

