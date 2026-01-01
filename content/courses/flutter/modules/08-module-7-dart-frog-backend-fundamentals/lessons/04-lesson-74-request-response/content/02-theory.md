---
type: "THEORY"
title: "Reading Request Data"
---


Here's how to extract different types of data from incoming requests:



```dart
import 'package:dart_frog/dart_frog.dart';

Response onRequest(RequestContext context) {
  // 1. HTTP Method - What action is being requested?
  final method = context.request.method;
  // Returns: HttpMethod.get, HttpMethod.post, HttpMethod.put, etc.
  
  // 2. Query Parameters - Data in the URL after ?
  // Example URL: /users?name=John&age=25
  final name = context.request.uri.queryParameters['name']; // 'John'
  final age = context.request.uri.queryParameters['age'];   // '25'
  
  // 3. Headers - Metadata sent with the request
  final authHeader = context.request.headers['Authorization'];
  final contentType = context.request.headers['Content-Type'];
  
  // 4. Full URL information
  final fullPath = context.request.uri.path;  // '/users'
  final fullUrl = context.request.uri.toString(); // Full URL with query
  
  return Response(body: 'Method: $method, Name: $name');
}
```
