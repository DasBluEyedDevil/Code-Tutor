---
type: "THEORY"
title: "What is an ORM and Why Use One?"
---

An **Object-Relational Mapper (ORM)** is a programming technique that converts data between a relational database and an object-oriented programming language. Instead of writing SQL queries as strings, you work with Dart objects.

**Without an ORM (Raw SQL):**

```dart
final result = await db.query(
  "SELECT * FROM users WHERE email = 'john@example.com' AND is_active = true"
);
// Returns raw Map<String, dynamic>
// No type safety, easy to make typos
```

**With Serverpod ORM:**

```dart
final user = await User.db.findFirstRow(
  session,
  where: (t) => t.email.equals('john@example.com') & t.isActive.equals(true),
);
// Returns User? (typed object)
// Compile-time checking, autocomplete works
```

**Benefits of Using an ORM:**

1. **Type Safety**: The compiler catches errors before runtime. If you mistype a column name, you get a compile error, not a runtime crash.

2. **Autocomplete**: Your IDE shows all available columns and methods as you type.

3. **Refactoring**: Rename a field and the IDE updates all usages. With SQL strings, you must find-and-replace manually.

4. **Protection from SQL Injection**: The ORM automatically escapes values, preventing security vulnerabilities.

5. **Database Abstraction**: Your code works with Dart objects. The ORM handles translation to SQL.

