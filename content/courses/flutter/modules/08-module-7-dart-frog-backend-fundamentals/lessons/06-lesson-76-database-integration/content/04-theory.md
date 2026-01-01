---
type: "THEORY"
title: "Providing Database via Middleware"
---


Instead of creating a new database connection for every request (slow and wasteful), use middleware to share a single connection across all routes.

**The Provider Pattern**: Middleware injects the database connection into the request context, and routes access it via `context.read<T>()`.



```dart
// lib/db.dart - Create a shared database connection
import 'package:postgres/postgres.dart';

late Connection dbConnection;

Future<void> initDatabase() async {
  dbConnection = await Connection.open(
    Endpoint(
      host: 'localhost',
      database: 'myapp',
      username: 'postgres',
      password: 'password',
    ),
    settings: ConnectionSettings(sslMode: SslMode.disable),
  );
  print('Database connected!');
}

// routes/_middleware.dart - Provide the connection to all routes
import 'package:dart_frog/dart_frog.dart';
import 'package:postgres/postgres.dart';
import '../lib/db.dart';

Handler middleware(Handler handler) {
  return handler.use(
    provider<Connection>((context) => dbConnection),
  );
}

// routes/users.dart - Use the connection in your route
import 'package:dart_frog/dart_frog.dart';
import 'package:postgres/postgres.dart';

Future<Response> onRequest(RequestContext context) async {
  // Read the database connection from context
  final db = context.read<Connection>();
  
  // Query the database
  final result = await db.execute('SELECT id, name, email FROM users');
  
  // Convert rows to maps for JSON response
  final users = result.map((row) => {
    'id': row[0],
    'name': row[1],
    'email': row[2],
  }).toList();
  
  return Response.json(body: {'users': users});
}
```
