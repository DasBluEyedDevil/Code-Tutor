---
type: "EXAMPLE"
title: "MVI Implementation"
---

Complete MVI implementation for a notes feature:

```kotlin
// ========== Intent (User Actions) ==========
sealed interface NotesIntent {
    data object LoadNotes : NotesIntent
    data object Refresh : NotesIntent
    data class SearchNotes(val query: String) : NotesIntent
    data class DeleteNote(val noteId: String) : NotesIntent
    data class TogglePin(val noteId: String) : NotesIntent
    data class SelectFilter(val filter: NoteFilter) : NotesIntent
    data object DismissError : NotesIntent
}

// ========== State ==========
data class NotesState(
    val notes: List<NoteUiModel> = emptyList(),
    val isLoading: Boolean = true,
    val isRefreshing: Boolean = false,
    val error: String? = null,
    val searchQuery: String = "",
    val selectedFilter: NoteFilter = NoteFilter.ALL
)

// ========== Side Effects ==========
sealed interface NotesSideEffect {
    data class ShowSnackbar(val message: String) : NotesSideEffect
    data class NavigateToNote(val noteId: String) : NotesSideEffect
}

// ========== ViewModel with MVI ==========
class NotesViewModel(
    private val noteRepository: NoteRepository
) {
    private val _state = MutableStateFlow(NotesState())
    val state: StateFlow<NotesState> = _state.asStateFlow()
    
    private val _sideEffects = Channel<NotesSideEffect>(Channel.BUFFERED)
    val sideEffects: Flow<NotesSideEffect> = _sideEffects.receiveAsFlow()
    
    private val scope = CoroutineScope(Dispatchers.Main + SupervisorJob())
    
    init {
        processIntent(NotesIntent.LoadNotes)
    }
    
    fun processIntent(intent: NotesIntent) {
        when (intent) {
            is NotesIntent.LoadNotes -> loadNotes()
            is NotesIntent.Refresh -> refresh()
            is NotesIntent.SearchNotes -> search(intent.query)
            is NotesIntent.DeleteNote -> deleteNote(intent.noteId)
            is NotesIntent.TogglePin -> togglePin(intent.noteId)
            is NotesIntent.SelectFilter -> selectFilter(intent.filter)
            is NotesIntent.DismissError -> reduce { it.copy(error = null) }
        }
    }
    
    private fun loadNotes() {
        reduce { it.copy(isLoading = true) }
        scope.launch {
            noteRepository.observeAllNotes()
                .catch { e -> reduce { it.copy(error = e.message, isLoading = false) } }
                .collect { notes ->
                    reduce { state ->
                        state.copy(
                            notes = notes.filter(state.selectedFilter).map { it.toUiModel() },
                            isLoading = false
                        )
                    }
                }
        }
    }
    
    private fun refresh() {
        reduce { it.copy(isRefreshing = true) }
        scope.launch {
            noteRepository.sync()
                .onSuccess { reduce { it.copy(isRefreshing = false) } }
                .onFailure { e ->
                    reduce { it.copy(isRefreshing = false) }
                    _sideEffects.send(NotesSideEffect.ShowSnackbar("Sync failed: ${e.message}"))
                }
        }
    }
    
    private fun search(query: String) {
        reduce { it.copy(searchQuery = query) }
        // Debounce and filter logic here
    }
    
    private fun deleteNote(noteId: String) {
        scope.launch {
            noteRepository.deleteNote(noteId)
            _sideEffects.send(NotesSideEffect.ShowSnackbar("Note deleted"))
        }
    }
    
    private fun togglePin(noteId: String) {
        scope.launch {
            val note = noteRepository.getNoteById(noteId) ?: return@launch
            noteRepository.saveNote(note.copy(isPinned = !note.isPinned))
        }
    }
    
    private fun selectFilter(filter: NoteFilter) {
        reduce { it.copy(selectedFilter = filter) }
    }
    
    // Helper for state updates
    private inline fun reduce(reducer: (NotesState) -> NotesState) {
        _state.update(reducer)
    }
    
    fun onCleared() {
        scope.cancel()
    }
}
```
