---
type: "THEORY"
title: "Error Handling Patterns"
---


Robust error handling is essential for production apps. Network calls can fail for many reasons, and your app must handle each gracefully.

**Types of Errors You Will Encounter:**

1. **Network Errors**: Device is offline, server is unreachable, connection timeout
2. **Server Errors**: 500 Internal Server Error, database failures, unhandled exceptions
3. **Client Errors**: 400 Bad Request, 401 Unauthorized, 403 Forbidden, 404 Not Found
4. **Validation Errors**: Invalid input data, business rule violations
5. **Authentication Errors**: Expired tokens, revoked access

**Serverpod's Exception Hierarchy:**

Serverpod throws specific exceptions that you can catch:

```dart
try {
  await client.user.getUser(id);
} on ServerpodClientException catch (e) {
  // Server returned an error response
  print('Server error: ${e.message}');
  print('Status code: ${e.statusCode}');
} on SocketException catch (e) {
  // Network connectivity issue
  print('Network error: Cannot reach server');
} on TimeoutException catch (e) {
  // Request took too long
  print('Request timed out');
} catch (e) {
  // Unexpected error
  print('Unknown error: $e');
}
```

**Custom Server Exceptions:**

You can define custom exceptions on the server that the client can catch:

```dart
// Server-side: Define a custom exception
class TaskNotFoundException implements SerializableException {
  final int taskId;
  TaskNotFoundException(this.taskId);
  
  @override
  String get message => 'Task $taskId was not found';
}

// Server endpoint throws it
Future<Task> getTask(Session session, int id) async {
  final task = await Task.db.findById(session, id);
  if (task == null) {
    throw TaskNotFoundException(id);
  }
  return task;
}

// Client catches it specifically
try {
  final task = await client.task.getTask(999);
} on TaskNotFoundException catch (e) {
  print('Task not found: ${e.taskId}');
} on ServerpodClientException catch (e) {
  print('Other server error: ${e.message}');
}
```

