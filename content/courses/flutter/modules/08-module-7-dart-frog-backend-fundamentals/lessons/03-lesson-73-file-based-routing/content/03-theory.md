---
type: "THEORY"
title: "Dynamic Route Parameters"
---


What if you need routes like `/users/123` or `/products/abc`? Use **dynamic segments** by wrapping the filename in square brackets.

**The Pattern**: `[paramName].dart`



```dart
// routes/users/[id].dart -> GET /users/123, /users/456, /users/any-value
import 'package:dart_frog/dart_frog.dart';

Response onRequest(RequestContext context, String id) {
  return Response(body: 'User ID: $id');
}

// Visiting /users/42 returns: "User ID: 42"
// Visiting /users/john returns: "User ID: john"
```
