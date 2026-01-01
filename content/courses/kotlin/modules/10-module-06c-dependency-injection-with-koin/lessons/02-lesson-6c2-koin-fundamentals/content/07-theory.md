---
type: "THEORY"
title: "Parameters: Runtime Values"
---

Some dependencies need runtime values (like a note ID):

```kotlin
val presentationModule = module {
    // ViewModel that needs a noteId at creation time
    viewModel { parameters ->
        NoteDetailViewModel(
            noteId = parameters.get<String>(),
            repository = get()
        )
    }
}
```

### Passing Parameters

```kotlin
// In Compose
@Composable
fun NoteDetailScreen(noteId: String) {
    val viewModel = koinViewModel<NoteDetailViewModel> {
        parametersOf(noteId)
    }
    // ...
}
```

### Multiple Parameters

```kotlin
viewModel { parameters ->
    SearchViewModel(
        initialQuery = parameters.get<String>(),
        category = parameters.get<Category>(),
        repository = get()
    )
}

// Usage
val viewModel = koinViewModel<SearchViewModel> {
    parametersOf("kotlin", Category.PROGRAMMING)
}
```