---
type: "WARNING"
title: "Security: SQL Injection"
---


**SQL Injection** is one of the most dangerous security vulnerabilities. It allows attackers to execute arbitrary SQL commands on your database.

**The Attack**:
If you build SQL queries by concatenating user input:
```dart
// DANGEROUS! Never do this!
final name = request.queryParameters['name']; // User provides: "'; DROP TABLE users; --"
final query = "SELECT * FROM users WHERE name = '$name'";
// Resulting query: SELECT * FROM users WHERE name = ''; DROP TABLE users; --'
// This DELETES your entire users table!
```

**The Solution: Parameterized Queries**

```dart
// BAD - SQL injection vulnerable!
await db.execute(
  "SELECT * FROM users WHERE name = '$name'",
);

// GOOD - parameterized query (SAFE!)
await db.execute(
  r'SELECT * FROM users WHERE name = $1',
  parameters: [name],
);
```

**Why Parameterized Queries Work**:
- The database treats parameters as DATA, not as SQL code
- Even if the user enters `'; DROP TABLE users; --`, it's treated as a literal string to search for
- The SQL structure is fixed - parameters can only fill in values

**Rule of Thumb**: NEVER concatenate user input into SQL strings. ALWAYS use parameterized queries.

**More Examples**:
```dart
// BAD - All of these are vulnerable
await db.execute("INSERT INTO users (name) VALUES ('$name')");
await db.execute('DELETE FROM users WHERE id = $id'); // $id is Dart interpolation!
await db.execute('UPDATE users SET email = "$email" WHERE id = $id');

// GOOD - All of these are safe
await db.execute(r'INSERT INTO users (name) VALUES ($1)', parameters: [name]);
await db.execute(r'DELETE FROM users WHERE id = $1', parameters: [id]);
await db.execute(r'UPDATE users SET email = $1 WHERE id = $2', parameters: [email, id]);

// Note: The 'r' prefix makes it a raw string, so $1 is literal text, not Dart interpolation
```

