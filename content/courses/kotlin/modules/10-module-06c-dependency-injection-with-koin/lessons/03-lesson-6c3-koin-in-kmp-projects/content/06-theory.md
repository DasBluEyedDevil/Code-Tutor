---
type: "THEORY"
title: "Using Koin in Compose Multiplatform"
---

### KoinApplication Provider

```kotlin
// composeApp/commonMain/kotlin/App.kt
import org.koin.compose.KoinApplication
import org.koin.compose.koinInject
import org.koin.compose.viewmodel.koinViewModel

@Composable
fun App() {
    KoinApplication(application = {
        modules(commonModule, platformModule)
    }) {
        NotesApp()
    }
}
```

### Injecting in Composables

```kotlin
@Composable
fun NotesListScreen() {
    // Inject ViewModel - lifecycle aware
    val viewModel = koinViewModel<NotesListViewModel>()
    val state by viewModel.state.collectAsState()
    
    // Inject any dependency
    val logger: PlatformLogger = koinInject()
    
    LazyColumn {
        items(state.notes) { note ->
            NoteCard(note)
        }
    }
}

@Composable
fun NoteDetailScreen(noteId: String) {
    // ViewModel with parameters
    val viewModel = koinViewModel<NoteDetailViewModel> {
        parametersOf(noteId)
    }
    // ...
}
```