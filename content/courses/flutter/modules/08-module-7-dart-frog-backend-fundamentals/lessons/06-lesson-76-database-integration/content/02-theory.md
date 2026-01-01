---
type: "THEORY"
title: "Setting Up PostgreSQL"
---


**Step 1: Add the dependency** to your `pubspec.yaml`:

```yaml
dependencies:
  dart_frog: ^1.0.0
  postgres: ^3.0.0
```

**Step 2: Create a database connection**:



```dart
import 'package:postgres/postgres.dart';

// Create a connection to PostgreSQL
Future<Connection> createDatabaseConnection() async {
  final connection = await Connection.open(
    Endpoint(
      host: 'localhost',        // Database server address
      database: 'myapp',        // Database name
      username: 'postgres',     // Username
      password: 'password',     // Password
    ),
    settings: ConnectionSettings(
      sslMode: SslMode.disable, // For local development
    ),
  );
  
  print('Connected to database!');
  return connection;
}

// Usage:
// final db = await createDatabaseConnection();
// Now you can run queries on 'db'
```
