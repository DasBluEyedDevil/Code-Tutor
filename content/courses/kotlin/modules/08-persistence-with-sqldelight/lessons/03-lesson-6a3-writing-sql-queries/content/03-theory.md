---
type: "THEORY"
title: "Using Generated Code"
---

SQLDelight generates Kotlin code from your SQL. Here's how to use it:

### Generated Classes

```kotlin
// SQLDelight generates from your schema:
data class Note(
    val id: Long,
    val title: String,
    val content: String,
    val created_at: Long,
    val updated_at: Long
)

// And a Queries class:
class NoteQueries {
    fun getAllNotes(): Query<Note>
    fun getNoteById(id: Long): Query<Note>
    fun insertNote(title: String, content: String, created_at: Long, updated_at: Long)
    fun updateNote(title: String, content: String, updated_at: Long, id: Long)
    fun deleteNote(id: Long)
}
```

### Executing Queries

```kotlin
val database: AppDatabase = createDatabase(driverFactory)
val noteQueries = database.noteQueries

// SELECT - returns Query<T>
val allNotes: List<Note> = noteQueries.getAllNotes().executeAsList()
val singleNote: Note? = noteQueries.getNoteById(1).executeAsOneOrNull()
val firstNote: Note = noteQueries.getNoteById(1).executeAsOne() // throws if empty

// INSERT/UPDATE/DELETE - returns nothing
noteQueries.insertNote(
    title = "My Note",
    content = "Hello World",
    created_at = System.currentTimeMillis(),
    updated_at = System.currentTimeMillis()
)

noteQueries.deleteNote(id = 1)
```