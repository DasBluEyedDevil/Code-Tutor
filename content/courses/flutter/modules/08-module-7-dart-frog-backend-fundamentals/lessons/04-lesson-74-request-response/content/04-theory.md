---
type: "THEORY"
title: "Response Types"
---


Dart Frog provides several ways to send responses back to clients:



```dart
import 'package:dart_frog/dart_frog.dart';

// 1. Plain Text Response
Response plainText() {
  return Response(body: 'Hello, World!');
  // Content-Type: text/plain
}

// 2. JSON Response (most common for APIs)
Response jsonData() {
  return Response.json(
    body: {
      'message': 'Success',
      'count': 42,
      'items': ['a', 'b', 'c'],
    },
  );
  // Content-Type: application/json
}

// 3. Custom Status Code
Response notFound() {
  return Response.json(
    body: {'error': 'User not found'},
    statusCode: 404,
  );
}

Response created() {
  return Response.json(
    body: {'id': '123', 'message': 'User created'},
    statusCode: 201, // 201 = Created
  );
}

// 4. Custom Headers
Response withHeaders() {
  return Response.json(
    body: {'token': 'abc123'},
    headers: {
      'X-Custom-Header': 'custom-value',
      'Cache-Control': 'no-cache',
    },
  );
}

// 5. Empty Response (for DELETE operations)
Response deleted() {
  return Response(statusCode: 204); // 204 = No Content
}
```
