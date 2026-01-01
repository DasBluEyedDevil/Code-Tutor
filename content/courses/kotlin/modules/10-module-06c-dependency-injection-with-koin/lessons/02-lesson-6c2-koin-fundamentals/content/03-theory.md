---
type: "THEORY"
title: "Defining Modules"
---

A **module** is a container for dependency definitions:

```kotlin
import org.koin.dsl.module

val appModule = module {
    // Define dependencies here
}
```

### Definition Types

| Type | Behavior | Use Case |
|------|----------|----------|
| `single { }` | Creates once, reuses | Database, HttpClient, Repository |
| `factory { }` | Creates new each time | ViewModel (in some cases), Use cases |
| `viewModel { }` | Lifecycle-aware | ViewModels with Compose |
| `scoped { }` | Lives within a scope | Session-specific data |

```kotlin
val appModule = module {
    // Singleton - one instance for the app
    single { AppDatabase.create() }
    
    // Factory - new instance each time
    factory { CreateNoteUseCase(get()) }
    
    // ViewModel - managed lifecycle
    viewModel { NotesViewModel(get(), get()) }
}
```