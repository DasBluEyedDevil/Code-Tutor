---
type: "EXAMPLE"
title: "CORS Middleware"
---


CORS (Cross-Origin Resource Sharing) headers are essential when your Flutter web app or other clients need to access your API from a different domain.

**The Problem**: Browsers block requests to different domains by default for security.
**The Solution**: Add CORS headers to tell browsers it's safe to allow the request.



```dart
// routes/_middleware.dart
import 'package:dart_frog/dart_frog.dart';

Handler middleware(Handler handler) {
  return (context) async {
    // Handle preflight OPTIONS requests
    if (context.request.method == HttpMethod.options) {
      return Response(
        statusCode: 200,
        headers: {
          'Access-Control-Allow-Origin': '*',
          'Access-Control-Allow-Methods': 'GET, POST, PUT, DELETE, OPTIONS',
          'Access-Control-Allow-Headers': 'Origin, Content-Type, Authorization',
        },
      );
    }
    
    // Call the route handler
    final response = await handler(context);
    
    // Add CORS headers to all responses
    return response.copyWith(
      headers: {
        ...response.headers,
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Methods': 'GET, POST, PUT, DELETE, OPTIONS',
        'Access-Control-Allow-Headers': 'Origin, Content-Type, Authorization',
      },
    );
  };
}

// Now any route in this folder (and subfolders) will have CORS headers!
// Example: GET /api/users will include Access-Control-Allow-Origin: *
```
