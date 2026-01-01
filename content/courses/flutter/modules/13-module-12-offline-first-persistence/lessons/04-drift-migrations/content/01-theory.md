---
type: "THEORY"
title: "Schema Versioning"
---


**Why Migrations Matter:**

When you change your database schema (add columns, rename tables, etc.), existing users have the old schema. Migrations update their database without losing data.

**The schemaVersion:**

Every time you change the schema, increment the version:

```dart
@DriftDatabase(tables: [Notes, Categories])
class AppDatabase extends _$AppDatabase {
  @override
  int get schemaVersion => 2; // Increment when schema changes
}
```

**Migration Triggers:**
- Adding a new table
- Adding a new column
- Removing a column
- Renaming a column or table
- Changing column constraints

