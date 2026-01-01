---
type: "EXAMPLE"
title: "Auth Middleware for Protected Routes"
---


Create middleware that protects routes by requiring valid JWT:



```dart
// routes/api/_middleware.dart
import 'package:dart_frog/dart_frog.dart';
import '../../lib/utils/jwt_helper.dart';

// This middleware runs BEFORE any route under /api/
Handler middleware(Handler handler) {
  return (context) async {
    // Get the Authorization header
    final authHeader = context.request.headers['Authorization'];
    
    // Check if header exists and starts with 'Bearer '
    if (authHeader == null || !authHeader.startsWith('Bearer ')) {
      return Response.json(
        body: {
          'error': 'Missing or invalid Authorization header',
          'hint': 'Include header: Authorization: Bearer <your-token>',
        },
        statusCode: 401, // Unauthorized
      );
    }
    
    // Extract the token (remove 'Bearer ' prefix)
    final token = authHeader.substring(7); // 'Bearer '.length = 7
    
    // Verify the token
    final payload = verifyToken(token);
    
    if (payload == null) {
      return Response.json(
        body: {
          'error': 'Invalid or expired token',
          'hint': 'Please login again to get a new token',
        },
        statusCode: 401, // Unauthorized
      );
    }
    
    // Token is valid! Inject user info into context
    // Routes can access this with context.read<Map<String, dynamic>>()
    return handler(
      context.provide<Map<String, dynamic>>(() => payload),
    );
  };
}

// Now in any protected route:
// Future<Response> onRequest(RequestContext context) async {
//   final user = context.read<Map<String, dynamic>>();
//   final userId = user['userId'];
//   // This user is guaranteed to be authenticated!
// }
```
