---
type: "WARNING"
title: "Common Endpoint Mistakes"
---

**Mistake 1: Forgetting the Session Parameter**

```dart
// WRONG - Missing session
Future<User?> getUser(int userId) async { ... }

// CORRECT - Session is required first parameter
Future<User?> getUser(Session session, int userId) async { ... }
```

**Mistake 2: Not Running Generate After Changes**

After adding or modifying endpoints, you MUST run:
```bash
serverpod generate
```

Otherwise, the client won't know about your new methods.

**Mistake 3: Returning Non-Serializable Types**

```dart
// WRONG - HttpRequest is not serializable
Future<HttpRequest> getRequest(Session session) async { ... }

// CORRECT - Return serializable types only
Future<Map<String, String>> getRequestHeaders(Session session) async { ... }
```

**Mistake 4: Long-Running Operations Without Feedback**

```dart
// BAD - Client waits forever with no feedback
Future<void> processLargeFile(Session session, ByteData file) async {
  // 10 minute operation with no progress updates
}

// BETTER - Use streaming or status polling
Future<String> startProcessing(Session session, ByteData file) async {
  // Start background job, return job ID immediately
  return jobId;
}

Future<ProcessingStatus> getStatus(Session session, String jobId) async {
  // Client can poll this for progress
}
```

**Mistake 5: Exposing Internal Methods**

```dart
class UserEndpoint extends Endpoint {
  // This helper should be private!
  // BAD - Exposed to client
  Future<void> validateEmail(Session session, String email) async { }

  // GOOD - Private helper (not exposed)
  bool _isValidEmail(String email) {
    return email.contains('@');
  }
}
```

Only public methods are exposed to the client. Use private methods (starting with _) for internal logic.

