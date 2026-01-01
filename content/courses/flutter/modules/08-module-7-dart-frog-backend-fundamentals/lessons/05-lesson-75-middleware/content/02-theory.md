---
type: "THEORY"
title: "Creating Middleware"
---


In Dart Frog, middleware is defined in a special file called `_middleware.dart`. The underscore prefix is important - it tells Dart Frog this is middleware, not a route.

**Basic Structure**:



```dart
// routes/_middleware.dart
import 'package:dart_frog/dart_frog.dart';

Handler middleware(Handler handler) {
  return (context) async {
    // ============================================
    // BEFORE the route handler runs
    // ============================================
    print('Request: ${context.request.method} ${context.request.uri}');
    final stopwatch = Stopwatch()..start();
    
    // ============================================
    // Call the next handler (your route)
    // ============================================
    final response = await handler(context);
    
    // ============================================
    // AFTER the route handler completes
    // ============================================
    stopwatch.stop();
    print('Response: ${response.statusCode} in ${stopwatch.elapsedMilliseconds}ms');
    
    return response;
  };
}
```
