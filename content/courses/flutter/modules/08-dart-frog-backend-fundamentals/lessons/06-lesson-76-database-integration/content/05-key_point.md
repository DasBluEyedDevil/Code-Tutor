---
type: "KEY_POINT"
title: "Database Best Practices"
---


**1. Use Environment Variables for Credentials**
```dart
// DON'T hardcode credentials!
final password = 'my_password'; // BAD!

// DO use environment variables
import 'dart:io';

final host = Platform.environment['DB_HOST'] ?? 'localhost';
final database = Platform.environment['DB_NAME'] ?? 'myapp';
final username = Platform.environment['DB_USER'] ?? 'postgres';
final password = Platform.environment['DB_PASSWORD'] ?? '';
```

**2. Connection Pooling for Production**
For high-traffic apps, use a connection pool instead of a single connection:
```dart
// Single connection - OK for development
final db = await Connection.open(endpoint);

// Connection pool - BETTER for production
final pool = Pool.withEndpoints([endpoint], settings: PoolSettings(
  maxConnectionCount: 10,
));
```

**3. Always Use Parameterized Queries**
Prevents SQL injection attacks (see next section).

**4. Close Connections Properly**
```dart
// When shutting down your server:
await db.close();
```

**5. Handle Connection Errors**
```dart
try {
  final db = await Connection.open(endpoint);
} on ServerException catch (e) {
  print('Database connection failed: ${e.message}');
  // Handle gracefully - maybe use fallback or retry
}
```

