---
type: "THEORY"
title: "How SQLDelight Works"
---

### The Build Process

```
1. You write .sq files with SQL statements
     ↓
2. SQLDelight Gradle plugin processes them
     ↓
3. Kotlin data classes are generated
     ↓
4. Type-safe query functions are generated
     ↓
5. You use the generated code in your app
```

### Example Workflow

**Step 1: Define your schema (Note.sq)**
```sql
CREATE TABLE Note (
    id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    title TEXT NOT NULL,
    content TEXT NOT NULL,
    created_at INTEGER NOT NULL
);
```

**Step 2: Add named queries**
```sql
getAllNotes:
SELECT * FROM Note ORDER BY created_at DESC;

getNoteById:
SELECT * FROM Note WHERE id = ?;

insertNote:
INSERT INTO Note(title, content, created_at)
VALUES (?, ?, ?);
```

**Step 3: Use generated code**
```kotlin
// Generated: data class Note(val id: Long, val title: String, ...)
// Generated: class NoteQueries { fun getAllNotes(), fun getNoteById(id: Long), ... }

val allNotes: List<Note> = noteQueries.getAllNotes().executeAsList()
val note: Note? = noteQueries.getNoteById(1).executeAsOneOrNull()
noteQueries.insertNote("Title", "Content", Clock.System.now().toEpochMilliseconds())
```