---
type: "EXAMPLE"
title: "Creating Routes"
---


Let's create our first custom route. In the `routes/` folder, create a new file called `hello.dart`:



```dart
// routes/hello.dart -> GET /hello
import 'package:dart_frog/dart_frog.dart';

Response onRequest(RequestContext context) {
  return Response(body: 'Hello, World!');
}
```
