// FILE 1: routes/api/users/index.dart
// Handles GET /api/users

import 'package:dart_frog/dart_frog.dart';

Response onRequest(RequestContext context) {
  return Response.json(
    body: {
      'users': [
        {'id': '1', 'name': 'Alice'},
        {'id': '2', 'name': 'Bob'},
        {'id': '3', 'name': 'Charlie'},
      ],
    },
  );
}

// FILE 2: routes/api/users/[id].dart
// Handles GET /api/users/:id

import 'package:dart_frog/dart_frog.dart';

Response onRequest(RequestContext context, String id) {
  return Response.json(
    body: {
      'id': id,
      'name': 'User $id',
      'email': 'user$id@example.com',
    },
  );
}

// FILE 3: routes/api/products/index.dart
// Handles GET /api/products

import 'package:dart_frog/dart_frog.dart';

Response onRequest(RequestContext context) {
  return Response.json(
    body: {
      'products': [
        {'id': 'p1', 'name': 'Laptop', 'price': 999.99},
        {'id': 'p2', 'name': 'Phone', 'price': 699.99},
        {'id': 'p3', 'name': 'Tablet', 'price': 449.99},
      ],
    },
  );
}