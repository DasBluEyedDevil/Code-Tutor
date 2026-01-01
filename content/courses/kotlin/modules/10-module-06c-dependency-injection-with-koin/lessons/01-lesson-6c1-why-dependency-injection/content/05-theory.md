---
type: "THEORY"
title: "DI Frameworks: Why Not Do It Manually?"
---

You might think: "I can just pass dependencies through constructors myself!"

For small apps, manual DI works. But as apps grow:

```kotlin
// Manual DI becomes unwieldy
fun createNotesScreen(): NotesScreen {
    val database = createDatabase(context)
    val notesDao = database.notesDao()
    val apiClient = createApiClient()
    val notesApi = NotesApi(apiClient)
    val notesRepository = NotesRepositoryImpl(notesDao, notesApi)
    val userPreferences = UserPreferences(context)
    val viewModel = NotesViewModel(notesRepository, userPreferences)
    return NotesScreen(viewModel)
}
```

**Problems:**
- Who calls this?
- What about singletons vs new instances?
- What about different configurations for debug/release?
- What about platform-specific implementations?

**DI Frameworks solve:**
- Automatic wiring of dependencies
- Lifecycle management (singleton, factory, scoped)
- Module organization
- Configuration by environment
- Platform-specific bindings