// FILE 1: routes/api/users/index.dart
// Handles GET /api/users

import 'package:dart_frog/dart_frog.dart';

Response onRequest(RequestContext context) {
  // TODO: Return a response with user list message
  return Response(body: '');
}

// FILE 2: routes/api/users/[id].dart
// Handles GET /api/users/:id

import 'package:dart_frog/dart_frog.dart';

Response onRequest(RequestContext context, String id) {
  // TODO: Return a response including the user ID
  return Response(body: '');
}

// FILE 3: routes/api/products/index.dart
// Handles GET /api/products

import 'package:dart_frog/dart_frog.dart';

Response onRequest(RequestContext context) {
  // TODO: Return a response with products list message
  return Response(body: '');
}