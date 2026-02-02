---
type: "WARNING"
title: "Common Mistakes"
---


**1. Forgetting `async` When Reading Body**

```dart
// WRONG - Will cause runtime error
Response onRequest(RequestContext context) {
  final body = await context.request.json(); // ERROR: await in non-async
  return Response.json(body: body);
}

// CORRECT - Mark function as async, return Future<Response>
Future<Response> onRequest(RequestContext context) async {
  final body = await context.request.json();
  return Response.json(body: body as Map<String, dynamic>);
}
```

**2. Not Handling Different HTTP Methods**

```dart
// WRONG - Only handles GET, POST/PUT/DELETE return same data
Response onRequest(RequestContext context) {
  return Response.json(body: {'users': []});
}

// CORRECT - Check method and respond appropriately
Future<Response> onRequest(RequestContext context) async {
  if (context.request.method == HttpMethod.get) {
    return Response.json(body: {'users': []});
  }
  return Response(statusCode: 405); // Method Not Allowed
}
```

**3. Wrong Status Codes**

| Situation | Wrong | Correct |
|-----------|-------|--------|
| Resource created | 200 | 201 |
| Resource deleted | 200 | 204 |
| Not found | 500 | 404 |
| Bad input | 404 | 400 |
| Unauthorized | 400 | 401 |

**4. Forgetting Type Casting**

```dart
// May cause issues
final body = await context.request.json();
final name = body['name']; // type: dynamic

// Better - explicit casting
final body = await context.request.json() as Map<String, dynamic>;
final name = body['name'] as String;
```

