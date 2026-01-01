---
type: "THEORY"
title: "SQLDelight Flow Extension"
---

### Setup

Add the coroutines extension:
```kotlin
implementation("app.cash.sqldelight:coroutines-extensions:2.0.2")
```

### Converting Query to Flow

```kotlin
import app.cash.sqldelight.coroutines.asFlow
import app.cash.sqldelight.coroutines.mapToList
import app.cash.sqldelight.coroutines.mapToOne
import app.cash.sqldelight.coroutines.mapToOneOrNull

class NoteRepository(private val database: AppDatabase) {
    private val queries = database.noteQueries
    
    // List of items
    fun observeAllNotes(): Flow<List<Note>> {
        return queries.getAllNotes()
            .asFlow()
            .mapToList(Dispatchers.IO)
    }
    
    // Single item (throws if not found)
    fun observeNoteById(id: Long): Flow<Note> {
        return queries.getNoteById(id)
            .asFlow()
            .mapToOne(Dispatchers.IO)
    }
    
    // Single item (null if not found)
    fun observeNoteByIdOrNull(id: Long): Flow<Note?> {
        return queries.getNoteById(id)
            .asFlow()
            .mapToOneOrNull(Dispatchers.IO)
    }
}
```