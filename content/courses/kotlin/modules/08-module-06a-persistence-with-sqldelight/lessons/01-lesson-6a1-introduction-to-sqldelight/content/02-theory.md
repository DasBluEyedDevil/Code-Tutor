---
type: "THEORY"
title: "What is SQLDelight?"
---

**SQLDelight** is a Kotlin Multiplatform library that generates typesafe Kotlin APIs from SQL statements.

```sql
-- Write SQL
CREATE TABLE Note (
    id INTEGER PRIMARY KEY,
    title TEXT NOT NULL,
    content TEXT NOT NULL
);

getAllNotes:
SELECT * FROM Note;
```

```kotlin
// SQLDelight generates type-safe Kotlin code
val notes: List<Note> = database.noteQueries.getAllNotes().executeAsList()
// Note is a generated data class with id: Long, title: String, content: String
```

### Key Features

| Feature | Description |
|---------|-------------|
| **Type-safe SQL** | Compile-time verification of your queries |
| **Multiplatform** | Works on Android, iOS, Desktop, and JS |
| **Code Generation** | Generates Kotlin data classes and query functions |
| **IDE Support** | SQL syntax highlighting and auto-completion |
| **Reactive Queries** | Flow integration for automatic UI updates |