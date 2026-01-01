---
type: "THEORY"
title: "Creating Test Fakes"
---

Since mocking libraries like Mockito don't work in KMP, we use **hand-written fakes**:

```kotlin
// Production interface
interface NoteRepository {
    suspend fun getAll(): List<Note>
    suspend fun getById(id: Long): Note?
    suspend fun add(title: String, content: String): Long
    suspend fun update(note: Note)
    suspend fun delete(id: Long)
}

// Test fake in commonTest/fakes/
class FakeNoteRepository : NoteRepository {
    private val notes = mutableListOf<Note>()
    private var nextId = 1L
    
    // Control test behavior
    var shouldThrowOnAdd = false
    var addDelay: Long = 0
    
    override suspend fun getAll(): List<Note> = notes.toList()
    
    override suspend fun getById(id: Long): Note? = notes.find { it.id == id }
    
    override suspend fun add(title: String, content: String): Long {
        if (shouldThrowOnAdd) throw Exception("Simulated error")
        if (addDelay > 0) delay(addDelay)
        
        val note = Note(
            id = nextId++,
            title = title,
            content = content,
            createdAt = Clock.System.now().toEpochMilliseconds()
        )
        notes.add(note)
        return note.id
    }
    
    override suspend fun update(note: Note) {
        val index = notes.indexOfFirst { it.id == note.id }
        if (index >= 0) notes[index] = note
    }
    
    override suspend fun delete(id: Long) {
        notes.removeAll { it.id == id }
    }
    
    // Helper methods for tests
    fun addTestNote(title: String, content: String) {
        notes.add(Note(nextId++, title, content, 0))
    }
    
    fun clear() {
        notes.clear()
        nextId = 1
    }
}
```

### Benefits of Fakes Over Mocks

| Fakes | Mocks |
|-------|-------|
| Work in all KMP targets | Often platform-specific |
| Reusable across tests | Usually recreated per test |
| Test behavior, not calls | Test implementation details |
| Simpler to understand | Complex mock setup |
| No runtime magic | Reflection/bytecode manipulation |