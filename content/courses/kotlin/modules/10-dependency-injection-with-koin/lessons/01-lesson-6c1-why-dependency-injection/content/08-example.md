---
type: "EXAMPLE"
title: "Before and After: Real-World Comparison"
---

See how DI transforms a typical KMP screen:

```kotlin
// ========== WITHOUT DI ==========

class NotesScreen : Screen {
    // Bad: Creating dependencies internally
    private val database = DatabaseProvider.getDatabase()
    private val apiClient = ApiClient.instance
    private val repository = NotesRepositoryImpl(database, apiClient)
    private val viewModel = NotesViewModel(repository)
    
    @Composable
    fun Content() {
        val state by viewModel.state.collectAsState()
        NotesList(state.notes)
    }
}

// Problems:
// - Can't test with fake data
// - Screen knows about database, API, repository details
// - Changing database affects multiple screens
// - Singletons everywhere

// ========== WITH KOIN DI ==========

// di/AppModule.kt
val appModule = module {
    single<NotesRepository> { NotesRepositoryImpl(get(), get()) }
    viewModel { NotesViewModel(get()) }
}

// Screen only receives what it needs
class NotesScreen : Screen {
    @Composable
    fun Content() {
        val viewModel = koinViewModel<NotesViewModel>()
        val state by viewModel.state.collectAsState()
        NotesList(state.notes)
    }
}

// Benefits:
// - Screen is simple - just UI
// - Dependencies managed centrally
// - Easy to swap implementations
// - Testable with fake modules
```
