---
type: "EXAMPLE"
title: "Complete Reactive Repository"
---

A full repository with reactive queries:

```kotlin
class NoteRepository(private val database: AppDatabase) {
    private val queries = database.noteQueries
    
    // ===== Reactive Queries (Flow) =====
    
    fun observeAllNotes(): Flow<List<Note>> = 
        queries.getAllNotes()
            .asFlow()
            .mapToList(Dispatchers.IO)
    
    fun observePinnedNotes(): Flow<List<Note>> = 
        queries.getPinnedNotes()
            .asFlow()
            .mapToList(Dispatchers.IO)
    
    fun observeNoteById(id: Long): Flow<Note?> = 
        queries.getNoteById(id)
            .asFlow()
            .mapToOneOrNull(Dispatchers.IO)
    
    fun searchNotes(query: String): Flow<List<Note>> = 
        queries.searchNotes(query)
            .asFlow()
            .mapToList(Dispatchers.IO)
    
    // ===== Mutations (suspend) =====
    
    suspend fun addNote(title: String, content: String) = withContext(Dispatchers.IO) {
        val now = Clock.System.now().toEpochMilliseconds()
        queries.insertNote(title, content, 0, now, now)
    }
    
    suspend fun updateNote(id: Long, title: String, content: String) = withContext(Dispatchers.IO) {
        val now = Clock.System.now().toEpochMilliseconds()
        queries.updateNote(title, content, now, id)
    }
    
    suspend fun togglePin(id: Long) = withContext(Dispatchers.IO) {
        queries.togglePin(id)
    }
    
    suspend fun deleteNote(id: Long) = withContext(Dispatchers.IO) {
        queries.deleteNote(id)
    }
}

// ===== ViewModel Usage =====

class NotesViewModel(private val repository: NoteRepository) {
    private val scope = CoroutineScope(Dispatchers.Main + SupervisorJob())
    
    private val _uiState = MutableStateFlow<NotesUiState>(NotesUiState.Loading)
    val uiState: StateFlow<NotesUiState> = _uiState.asStateFlow()
    
    init {
        // Single subscription - UI stays in sync automatically
        scope.launch {
            repository.observeAllNotes()
                .catch { e -> _uiState.value = NotesUiState.Error(e.message) }
                .collect { notes ->
                    _uiState.value = NotesUiState.Success(notes)
                }
        }
    }
    
    // No need to refresh after these - Flow handles it!
    fun addNote(title: String, content: String) {
        scope.launch { repository.addNote(title, content) }
    }
    
    fun deleteNote(id: Long) {
        scope.launch { repository.deleteNote(id) }
    }
}

sealed class NotesUiState {
    object Loading : NotesUiState()
    data class Success(val notes: List<Note>) : NotesUiState()
    data class Error(val message: String?) : NotesUiState()
}
```
