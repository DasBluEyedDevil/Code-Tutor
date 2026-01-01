---
type: "EXAMPLE"
title: "CRUD Operations"
---


CRUD stands for **C**reate, **R**ead, **U**pdate, **D**elete - the four fundamental database operations.

Assuming you have a `users` table:
```sql
CREATE TABLE users (
  id SERIAL PRIMARY KEY,
  name VARCHAR(100) NOT NULL,
  email VARCHAR(100) UNIQUE NOT NULL,
  created_at TIMESTAMP DEFAULT NOW()
);
```

Here's how to perform CRUD operations:



```dart
import 'package:postgres/postgres.dart';

Future<void> crudExamples(Connection db) async {
  // ============================================
  // CREATE - Insert a new user
  // ============================================
  await db.execute(
    r'INSERT INTO users (name, email) VALUES ($1, $2)',
    parameters: ['John Doe', 'john@example.com'],
  );
  print('User created!');
  
  // ============================================
  // READ - Query all users
  // ============================================
  final allUsers = await db.execute('SELECT * FROM users');
  for (final row in allUsers) {
    // row[0] = id, row[1] = name, row[2] = email
    print('User: id=${row[0]}, name=${row[1]}, email=${row[2]}');
  }
  
  // READ - Query with a condition
  final result = await db.execute(
    r'SELECT * FROM users WHERE email = $1',
    parameters: ['john@example.com'],
  );
  if (result.isNotEmpty) {
    print('Found user: ${result.first[1]}');
  }
  
  // ============================================
  // UPDATE - Modify existing user
  // ============================================
  await db.execute(
    r'UPDATE users SET name = $1 WHERE id = $2',
    parameters: ['Jane Doe', 1],
  );
  print('User updated!');
  
  // ============================================
  // DELETE - Remove a user
  // ============================================
  await db.execute(
    r'DELETE FROM users WHERE id = $1',
    parameters: [1],
  );
  print('User deleted!');
}
```
