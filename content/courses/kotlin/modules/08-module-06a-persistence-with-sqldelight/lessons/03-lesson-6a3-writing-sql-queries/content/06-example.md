---
type: "EXAMPLE"
title: "Complete Notes Repository"
---

A complete repository using SQLDelight:

```kotlin
// Note.sq
CREATE TABLE Note (
    id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    title TEXT NOT NULL,
    content TEXT NOT NULL,
    is_pinned INTEGER NOT NULL DEFAULT 0,
    created_at INTEGER NOT NULL,
    updated_at INTEGER NOT NULL
);

getAllNotes:
SELECT * FROM Note ORDER BY is_pinned DESC, updated_at DESC;

getPinnedNotes:
SELECT * FROM Note WHERE is_pinned = 1 ORDER BY updated_at DESC;

getNoteById:
SELECT * FROM Note WHERE id = ?;

searchNotes:
SELECT * FROM Note 
WHERE title LIKE '%' || :query || '%' 
   OR content LIKE '%' || :query || '%'
ORDER BY updated_at DESC;

insertNote:
INSERT INTO Note(title, content, is_pinned, created_at, updated_at)
VALUES (?, ?, ?, ?, ?);

updateNote:
UPDATE Note SET title = ?, content = ?, updated_at = ? WHERE id = ?;

togglePin:
UPDATE Note SET is_pinned = NOT is_pinned WHERE id = ?;

deleteNote:
DELETE FROM Note WHERE id = ?;

-- NoteRepository.kt
class NoteRepository(private val database: AppDatabase) {
    private val queries = database.noteQueries
    
    fun getAllNotes(): List<Note> = 
        queries.getAllNotes().executeAsList()
    
    fun getPinnedNotes(): List<Note> = 
        queries.getPinnedNotes().executeAsList()
    
    fun getNoteById(id: Long): Note? = 
        queries.getNoteById(id).executeAsOneOrNull()
    
    fun searchNotes(query: String): List<Note> = 
        queries.searchNotes(query).executeAsList()
    
    fun addNote(title: String, content: String, isPinned: Boolean = false) {
        val now = Clock.System.now().toEpochMilliseconds()
        queries.insertNote(
            title = title,
            content = content,
            is_pinned = if (isPinned) 1L else 0L,
            created_at = now,
            updated_at = now
        )
    }
    
    fun updateNote(id: Long, title: String, content: String) {
        queries.updateNote(
            title = title,
            content = content,
            updated_at = Clock.System.now().toEpochMilliseconds(),
            id = id
        )
    }
    
    fun togglePin(id: Long) = queries.togglePin(id)
    
    fun deleteNote(id: Long) = queries.deleteNote(id)
}
```
