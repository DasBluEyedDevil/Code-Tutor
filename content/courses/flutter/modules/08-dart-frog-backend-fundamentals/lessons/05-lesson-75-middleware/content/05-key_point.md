---
type: "KEY_POINT"
title: "Common Middleware Patterns"
---


**1. Request Logging**
```dart
Handler middleware(Handler handler) {
  return (context) async {
    print('[${DateTime.now()}] ${context.request.method} ${context.request.uri}');
    final response = await handler(context);
    print('[${DateTime.now()}] Response: ${response.statusCode}');
    return response;
  };
}
```

**2. Authentication Check**
```dart
Handler middleware(Handler handler) {
  return (context) async {
    final authHeader = context.request.headers['Authorization'];
    
    if (authHeader == null || !authHeader.startsWith('Bearer ')) {
      return Response.json(
        body: {'error': 'Unauthorized - No token provided'},
        statusCode: 401,
      );
    }
    
    // Token exists, continue to route handler
    return handler(context);
  };
}
```

**3. Error Handling Wrapper**
```dart
Handler middleware(Handler handler) {
  return (context) async {
    try {
      return await handler(context);
    } catch (e, stackTrace) {
      print('Error: $e\n$stackTrace');
      return Response.json(
        body: {'error': 'Internal server error'},
        statusCode: 500,
      );
    }
  };
}
```

**4. Dependency Injection (Provider)**
```dart
// Provide a database connection to all routes
Handler middleware(Handler handler) {
  return handler.use(
    provider<DatabaseConnection>((context) => myDbConnection),
  );
}

// In your route, access it with:
// final db = context.read<DatabaseConnection>();
```

**Key Takeaways**:
- Middleware runs before AND after route handlers
- Use `_middleware.dart` naming convention
- Scope middleware to specific folders for fine-grained control
- Chain middleware for layered processing
- Return early (without calling handler) to block requests

