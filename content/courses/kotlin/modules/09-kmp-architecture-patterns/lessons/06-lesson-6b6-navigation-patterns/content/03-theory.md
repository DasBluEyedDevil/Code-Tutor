---
type: "THEORY"
title: "Shared Navigation Events Pattern"
---

### The Pattern

ViewModel emits navigation events; platform code handles them:

```kotlin
// ========== commonMain ==========
sealed interface NavigationEvent {
    data class GoToNoteDetail(val noteId: String) : NavigationEvent
    data object GoToSettings : NavigationEvent
    data object GoBack : NavigationEvent
    data class ShowDialog(val message: String) : NavigationEvent
}

class NotesViewModel {
    private val _navigationEvents = Channel<NavigationEvent>(Channel.BUFFERED)
    val navigationEvents: Flow<NavigationEvent> = _navigationEvents.receiveAsFlow()
    
    fun onNoteClicked(noteId: String) {
        scope.launch {
            _navigationEvents.send(NavigationEvent.GoToNoteDetail(noteId))
        }
    }
    
    fun onSettingsClicked() {
        scope.launch {
            _navigationEvents.send(NavigationEvent.GoToSettings)
        }
    }
}
```

### Platform Handling

```kotlin
// Android Compose
@Composable
fun NotesScreen(
    viewModel: NotesViewModel,
    navController: NavController
) {
    LaunchedEffect(Unit) {
        viewModel.navigationEvents.collect { event ->
            when (event) {
                is NavigationEvent.GoToNoteDetail -> {
                    navController.navigate("note/${event.noteId}")
                }
                is NavigationEvent.GoToSettings -> {
                    navController.navigate("settings")
                }
                is NavigationEvent.GoBack -> {
                    navController.popBackStack()
                }
                is NavigationEvent.ShowDialog -> {
                    // Show dialog
                }
            }
        }
    }
}
```