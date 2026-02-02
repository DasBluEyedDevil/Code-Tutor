---
type: "THEORY"
title: "Understanding Session Context and Authentication"
---


Authentication in Serverpod flows through the `Session` object on the server and the `AuthenticationKeyManager` on the client. Understanding this flow is essential for building secure applications.

**How Authentication Works:**

1. **User Logs In**: Your app calls an authentication endpoint (email/password, social login, etc.)

2. **Server Creates Session**: Serverpod creates an authentication key and stores it in the database

3. **Client Stores Key**: The `FlutterAuthenticationKeyManager` securely stores the key on the device

4. **Subsequent Requests**: Every API call automatically includes the authentication key in headers

5. **Server Validates**: The session object on the server contains the authenticated user's information

**The FlutterAuthenticationKeyManager:**

This component handles secure storage of authentication tokens:

```dart
// The client is configured with an authentication key manager
client = Client(
  'http://localhost:8080/',
  authenticationKeyManager: FlutterAuthenticationKeyManager(),
);
```

The `FlutterAuthenticationKeyManager` uses Flutter's secure storage to persist tokens between app sessions. Users stay logged in even after closing the app.

**Checking Authentication Status:**

```dart
// Check if the user is currently authenticated
final isSignedIn = await client.authenticationKeyManager?.get() != null;

// Or use Serverpod's auth module if you are using it
final sessionInfo = await client.modules.auth.status.status();
if (sessionInfo.isSignedIn) {
  print('Logged in as: ${sessionInfo.userInfo?.userName}');
}
```

**Server-Side Session Access:**

On the server, every endpoint receives a `Session` that knows about the authenticated user:

```dart
class TaskEndpoint extends Endpoint {
  Future<List<Task>> getMyTasks(Session session) async {
    // Get the authenticated user's ID
    final userId = await session.auth.authenticatedUserId;
    
    if (userId == null) {
      throw AuthenticationRequiredException();
    }
    
    // Query only this user's tasks
    return await Task.db.find(
      session,
      where: (t) => t.userId.equals(userId),
    );
  }
}
```

