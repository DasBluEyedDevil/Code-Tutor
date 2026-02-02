---
type: "THEORY"
title: "Understanding File-Based Routing"
---


The `routes/` folder is where the magic happens. In Dart Frog, **files become routes**.

**The Rule**: The file path = the URL path.

| File Location | URL Endpoint |
|---------------|-------------|
| `routes/index.dart` | `GET /` |
| `routes/hello.dart` | `GET /hello` |
| `routes/users/index.dart` | `GET /users` |
| `routes/users/[id].dart` | `GET /users/:id` |

**Let's look at the default `routes/index.dart`**:

```dart
import 'package:dart_frog/dart_frog.dart';

Response onRequest(RequestContext context) {
  return Response(body: 'Welcome to Dart Frog!');
}
```

**Breaking It Down**:
- `import 'package:dart_frog/dart_frog.dart'` - Import Dart Frog
- `onRequest` - This function runs when someone visits the route
- `RequestContext context` - Contains info about the request
- `Response(body: ...)` - What we send back to the client

**This is the entire route handler!** No decorators, no registration, no configuration files. Create a file with `onRequest`, and you have a working endpoint.

