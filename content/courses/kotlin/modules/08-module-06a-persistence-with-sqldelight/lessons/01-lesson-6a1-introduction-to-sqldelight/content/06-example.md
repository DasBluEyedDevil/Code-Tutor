---
type: "EXAMPLE"
title: "SQLDelight in Action"
---

Here's a complete example showing SQLDelight's type-safety:

```kotlin
// Note.sq (your SQL file)
// ================================
// CREATE TABLE Note (
//     id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
//     title TEXT NOT NULL,
//     content TEXT NOT NULL
// );
//
// getAllNotes:
// SELECT * FROM Note;
//
// getNoteById:
// SELECT * FROM Note WHERE id = ?;

// Repository.kt (using generated code)
// ================================
class NoteRepository(private val database: AppDatabase) {
    
    // Type-safe! noteQueries is generated, getAllNotes() returns Query<Note>
    fun getAllNotes(): List<Note> {
        return database.noteQueries.getAllNotes().executeAsList()
    }
    
    // The parameter type is inferred from the schema (id is Long)
    fun getNoteById(id: Long): Note? {
        return database.noteQueries.getNoteById(id).executeAsOneOrNull()
    }
    
    // Compile error if you pass wrong types!
    fun addNote(title: String, content: String) {
        database.noteQueries.insertNote(title, content)
        // database.noteQueries.insertNote(123, content) // ❌ Compile error!
    }
}

// The generated Note class
// ================================
// data class Note(
//     val id: Long,
//     val title: String,
//     val content: String
// )
```
