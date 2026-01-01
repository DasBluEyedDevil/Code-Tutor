---
type: "THEORY"
title: "Handling Different HTTP Methods"
---


Real APIs need to handle multiple HTTP methods on the same endpoint. Use a switch statement to route to the right logic:



```dart
import 'package:dart_frog/dart_frog.dart';

Future<Response> onRequest(RequestContext context) async {
  // Route based on HTTP method
  switch (context.request.method) {
    
    case HttpMethod.get:
      // GET /users - Return list of users
      return Response.json(
        body: {
          'users': [
            {'id': '1', 'name': 'Alice'},
            {'id': '2', 'name': 'Bob'},
          ],
        },
      );
    
    case HttpMethod.post:
      // POST /users - Create a new user
      final body = await context.request.json() as Map<String, dynamic>;
      return Response.json(
        body: {
          'message': 'User created',
          'user': body,
        },
        statusCode: 201, // 201 = Created
      );
    
    case HttpMethod.put:
      // PUT - Update entire resource
      final body = await context.request.json() as Map<String, dynamic>;
      return Response.json(body: {'updated': body});
    
    case HttpMethod.patch:
      // PATCH - Partial update
      final body = await context.request.json() as Map<String, dynamic>;
      return Response.json(body: {'patched': body});
    
    case HttpMethod.delete:
      // DELETE - Remove resource
      return Response(statusCode: 204); // No content
    
    default:
      // Method not supported
      return Response.json(
        body: {'error': 'Method not allowed'},
        statusCode: 405, // 405 = Method Not Allowed
      );
  }
}
```
