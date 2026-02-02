---
type: "THEORY"
title: "The RequestContext"
---


Every route handler in Dart Frog receives a `RequestContext` object as its first parameter. This is your gateway to everything about the incoming request.

**What RequestContext Contains**:
- **request**: The HTTP request object with method, headers, body, URL
- **read<T>()**: Access to middleware-provided dependencies (covered later)

**The Pattern**:
```dart
Response onRequest(RequestContext context) {
  // context.request gives you access to:
  // - HTTP method (GET, POST, etc.)
  // - Request headers
  // - Request body
  // - URL and query parameters
  
  return Response(body: 'Hello!');
}
```

**Think of RequestContext as an envelope** - it contains everything the client sent to your server, neatly organized and ready to read.

