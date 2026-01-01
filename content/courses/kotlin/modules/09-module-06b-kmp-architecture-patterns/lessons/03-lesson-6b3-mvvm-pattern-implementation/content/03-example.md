---
type: "EXAMPLE"
title: "Basic ViewModel Structure"
---

Here's a complete MVVM implementation for a notes feature:

```kotlin
// ========== State Definition ==========
data class NotesUiState(
    val notes: List<NoteUiModel> = emptyList(),
    val isLoading: Boolean = true,
    val error: String? = null,
    val selectedFilter: NoteFilter = NoteFilter.ALL
)

data class NoteUiModel(
    val id: String,
    val title: String,
    val preview: String, // First 50 chars of content
    val formattedDate: String,
    val isPinned: Boolean
)

enum class NoteFilter { ALL, PINNED, RECENT }

// ========== ViewModel ==========
class NotesViewModel(
    private val noteRepository: NoteRepository,
    private val dateFormatter: DateFormatter // Platform-agnostic
) {
    // Internal mutable state
    private val _state = MutableStateFlow(NotesUiState())
    
    // Public immutable state
    val state: StateFlow<NotesUiState> = _state.asStateFlow()
    
    // Coroutine scope - will be provided by platform
    private val scope = CoroutineScope(Dispatchers.Main + SupervisorJob())
    
    init {
        observeNotes()
    }
    
    private fun observeNotes() {
        scope.launch {
            noteRepository.observeAllNotes()
                .catch { e ->
                    _state.update { it.copy(error = e.message, isLoading = false) }
                }
                .collect { notes ->
                    val filtered = filterNotes(notes, _state.value.selectedFilter)
                    val uiModels = filtered.map { it.toUiModel() }
                    _state.update { it.copy(notes = uiModels, isLoading = false) }
                }
        }
    }
    
    fun onFilterChanged(filter: NoteFilter) {
        _state.update { it.copy(selectedFilter = filter) }
    }
    
    fun onDeleteNote(noteId: String) {
        scope.launch {
            noteRepository.deleteNote(noteId)
            // No need to update state - observeNotes will emit new list
        }
    }
    
    fun onTogglePin(noteId: String) {
        scope.launch {
            val note = noteRepository.getNoteById(noteId) ?: return@launch
            noteRepository.saveNote(note.copy(isPinned = !note.isPinned))
        }
    }
    
    fun clearError() {
        _state.update { it.copy(error = null) }
    }
    
    fun onCleared() {
        scope.cancel()
    }
    
    private fun filterNotes(notes: List<Note>, filter: NoteFilter): List<Note> {
        return when (filter) {
            NoteFilter.ALL -> notes
            NoteFilter.PINNED -> notes.filter { it.isPinned }
            NoteFilter.RECENT -> notes.filter { it.isRecent() }
        }
    }
    
    private fun Note.toUiModel() = NoteUiModel(
        id = id,
        title = title,
        preview = content.take(50) + if (content.length > 50) "..." else "",
        formattedDate = dateFormatter.format(updatedAt),
        isPinned = isPinned
    )
}
```
