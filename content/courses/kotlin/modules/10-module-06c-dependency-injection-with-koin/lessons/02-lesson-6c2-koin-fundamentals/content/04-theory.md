---
type: "THEORY"
title: "The get() Function"
---

`get()` retrieves a dependency from Koin:

```kotlin
val appModule = module {
    single { AppDatabase.create() }
    
    // get() finds AppDatabase and passes it
    single { NotesDao(get()) }
    
    // get() finds NotesDao and passes it
    single<NotesRepository> { NotesRepositoryImpl(get()) }
    
    // get() finds NotesRepository and passes it
    viewModel { NotesViewModel(get()) }
}
```

### How get() Works

1. Koin looks for a matching type in registered modules
2. If found and it's a `single`, returns the existing instance
3. If found and it's a `factory`, creates a new instance
4. If not found, throws an exception at runtime

### Type Safety with Interfaces

```kotlin
val appModule = module {
    // Register implementation with interface type
    single<NotesRepository> { NotesRepositoryImpl(get()) }
    
    // ViewModel depends on interface, not implementation
    viewModel { NotesViewModel(get<NotesRepository>()) }
}
```

The `<NotesRepository>` syntax explicitly tells Koin which type to look for.