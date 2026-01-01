---
type: "WARNING"
title: "Important Considerations"
---

### SQL Knowledge Required

SQLDelight uses real SQL, not an ORM abstraction:
```sql
-- You write actual SQL
SELECT n.*, COUNT(c.id) as comment_count
FROM Note n
LEFT JOIN Comment c ON c.note_id = n.id
GROUP BY n.id;
```

If you're not comfortable with SQL:
- Consider learning SQL basics first
- SQLDelight's IntelliJ plugin helps with autocomplete
- Most queries are simple CRUD operations

### No Built-in Sync

SQLDelight is local-only:
- No automatic cloud sync
- You must implement sync logic yourself
- Consider: Supabase, Firebase, custom backend

### Threading Considerations

Database operations can block:
```kotlin
// ❌ Don't do this on Main thread
val notes = database.noteQueries.getAllNotes().executeAsList()

// ✅ Use coroutines with appropriate dispatcher
suspend fun getAllNotes() = withContext(Dispatchers.Default) {
    database.noteQueries.getAllNotes().executeAsList()
}
```