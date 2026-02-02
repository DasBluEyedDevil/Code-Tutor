---
type: "THEORY"
title: "Why Annotations?"
---

### DSL Approach (Traditional)

```kotlin
val appModule = module {
    single<NotesRepository> { NotesRepositoryImpl(get()) }
    viewModel { NotesViewModel(get()) }
}
```

**Pros**: Flexible, no code generation
**Cons**: Runtime errors if misconfigured, manual wiring

### Annotation Approach (Modern)

```kotlin
@Single
class NotesRepositoryImpl(
    private val database: AppDatabase
) : NotesRepository

@KoinViewModel
class NotesViewModel(
    private val repository: NotesRepository
)
```

**Pros**: Compile-time safety, less boilerplate, auto-generated modules
**Cons**: Requires KSP, slightly longer build times