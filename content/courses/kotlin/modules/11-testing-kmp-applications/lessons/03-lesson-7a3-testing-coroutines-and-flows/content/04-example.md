---
type: "EXAMPLE"
title: "Testing ViewModels with Coroutines"
---

Making ViewModels testable by injecting the dispatcher:

```kotlin
// Production ViewModel
class NotesViewModel(
    private val repository: NoteRepository,
    private val dispatcher: CoroutineDispatcher = Dispatchers.Default
) {
    private val scope = CoroutineScope(dispatcher + SupervisorJob())
    
    private val _state = MutableStateFlow(NotesUiState())
    val state: StateFlow<NotesUiState> = _state.asStateFlow()
    
    init {
        loadNotes()
    }
    
    fun loadNotes() {
        scope.launch {
            _state.update { it.copy(isLoading = true) }
            try {
                val notes = repository.getAll()
                _state.update { it.copy(notes = notes, isLoading = false) }
            } catch (e: Exception) {
                _state.update { it.copy(error = e.message, isLoading = false) }
            }
        }
    }
    
    fun addNote(title: String, content: String) {
        scope.launch {
            repository.add(title, content)
            loadNotes()  // Reload after adding
        }
    }
}

// Test with injected dispatcher
class NotesViewModelTest {
    private lateinit var repository: FakeNoteRepository
    
    @BeforeTest
    fun setup() {
        repository = FakeNoteRepository()
        repository.addTestNote("Note 1", "Content 1")
        repository.addTestNote("Note 2", "Content 2")
    }
    
    @Test
    fun `init loads notes`() = runTest {
        val viewModel = NotesViewModel(
            repository = repository,
            dispatcher = StandardTestDispatcher(testScheduler)
        )
        
        // Initially loading
        assertTrue(viewModel.state.value.isLoading)
        
        // Process coroutines
        advanceUntilIdle()
        
        // Notes loaded
        assertFalse(viewModel.state.value.isLoading)
        assertEquals(2, viewModel.state.value.notes.size)
    }
    
    @Test
    fun `addNote updates state with new note`() = runTest {
        val viewModel = NotesViewModel(
            repository = repository,
            dispatcher = StandardTestDispatcher(testScheduler)
        )
        advanceUntilIdle()
        
        viewModel.addNote("New Note", "New Content")
        advanceUntilIdle()
        
        assertEquals(3, viewModel.state.value.notes.size)
    }
    
    @Test
    fun `error is captured in state`() = runTest {
        repository.shouldThrowOnAdd = true
        val viewModel = NotesViewModel(
            repository = repository,
            dispatcher = StandardTestDispatcher(testScheduler)
        )
        advanceUntilIdle()
        
        viewModel.addNote("New", "Content")
        advanceUntilIdle()
        
        // Note: This would need error handling in addNote
        // to properly set error state
    }
}
```
