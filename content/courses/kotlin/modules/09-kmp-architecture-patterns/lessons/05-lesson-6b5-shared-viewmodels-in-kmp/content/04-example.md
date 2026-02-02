---
type: "EXAMPLE"
title: "Using on Android"
---

Wrap the shared ViewModel for Android lifecycle integration:

```kotlin
// ========== androidMain or androidApp ==========

// Option 1: Extend AndroidX ViewModel
class AndroidNotesViewModel(
    private val sharedViewModel: NotesViewModel
) : ViewModel() {
    
    val state = sharedViewModel.state
    
    fun addNote(title: String, content: String) = sharedViewModel.addNote(title, content)
    fun deleteNote(noteId: String) = sharedViewModel.deleteNote(noteId)
    
    override fun onCleared() {
        super.onCleared()
        sharedViewModel.onCleared()
    }
}

// Option 2: Factory with Koin
class NotesViewModelFactory(
    private val noteRepository: NoteRepository
) : ViewModelProvider.Factory {
    override fun <T : ViewModel> create(modelClass: Class<T>): T {
        val sharedViewModel = NotesViewModel(noteRepository)
        @Suppress("UNCHECKED_CAST")
        return AndroidNotesViewModel(sharedViewModel) as T
    }
}

// Usage in Activity/Fragment
class NotesFragment : Fragment() {
    private val viewModel: AndroidNotesViewModel by viewModels {
        NotesViewModelFactory(get()) // Koin inject
    }
}

// Option 3: Compose with Koin (simplest)
@Composable
fun NotesScreen() {
    val viewModel = remember { koinInject<NotesViewModel>() }
    
    DisposableEffect(Unit) {
        onDispose { viewModel.onCleared() }
    }
    
    val state by viewModel.state.collectAsState()
    // UI here
}
```
