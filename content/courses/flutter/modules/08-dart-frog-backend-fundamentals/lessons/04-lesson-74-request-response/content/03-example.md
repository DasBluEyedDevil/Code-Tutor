---
type: "EXAMPLE"
title: "Reading JSON Body"
---


For POST, PUT, and PATCH requests, clients often send data in the request body as JSON. Reading the body requires an `async` function:



```dart
import 'package:dart_frog/dart_frog.dart';

// Note: Function returns Future<Response> and is marked async
Future<Response> onRequest(RequestContext context) async {
  // Read and parse the JSON body
  // This waits for the entire body to be received
  final body = await context.request.json() as Map<String, dynamic>;
  
  // Extract fields from the JSON
  final name = body['name'] as String;
  final email = body['email'] as String;
  final age = body['age'] as int;
  
  // Example: Client sends {"name": "Alice", "email": "alice@mail.com", "age": 28}
  // name = 'Alice', email = 'alice@mail.com', age = 28
  
  return Response.json(
    body: {
      'received': true,
      'user': {
        'name': name,
        'email': email,
        'age': age,
      },
    },
  );
}
```
