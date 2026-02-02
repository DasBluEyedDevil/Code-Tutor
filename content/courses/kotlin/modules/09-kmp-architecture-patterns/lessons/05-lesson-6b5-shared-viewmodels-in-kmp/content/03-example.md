---
type: "EXAMPLE"
title: "Pure Kotlin ViewModel (Recommended)"
---

Create ViewModels that work on both platforms:

```kotlin
// ========== commonMain/viewmodel/BaseViewModel.kt ==========
abstract class BaseViewModel {
    protected val scope = CoroutineScope(Dispatchers.Main + SupervisorJob())
    
    open fun onCleared() {
        scope.cancel()
    }
}

// ========== commonMain/viewmodel/NotesViewModel.kt ==========
class NotesViewModel(
    private val noteRepository: NoteRepository
) : BaseViewModel() {
    
    private val _state = MutableStateFlow(NotesState())
    val state: StateFlow<NotesState> = _state.asStateFlow()
    
    init {
        loadNotes()
    }
    
    private fun loadNotes() {
        scope.launch {
            _state.update { it.copy(isLoading = true) }
            noteRepository.observeAllNotes()
                .catch { e -> _state.update { it.copy(error = e.message, isLoading = false) } }
                .collect { notes ->
                    _state.update { it.copy(notes = notes, isLoading = false) }
                }
        }
    }
    
    fun addNote(title: String, content: String) {
        scope.launch {
            val note = Note(
                id = UUID.randomUUID().toString(),
                title = title,
                content = content,
                createdAt = Clock.System.now().toEpochMilliseconds(),
                updatedAt = Clock.System.now().toEpochMilliseconds()
            )
            noteRepository.saveNote(note)
        }
    }
    
    fun deleteNote(noteId: String) {
        scope.launch {
            noteRepository.deleteNote(noteId)
        }
    }
}

data class NotesState(
    val notes: List<Note> = emptyList(),
    val isLoading: Boolean = true,
    val error: String? = null
)
```
