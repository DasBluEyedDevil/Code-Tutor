---
type: "THEORY"
title: "Why Koin for KMP?"
---

### DI Options for Kotlin

| Framework | Kotlin Multiplatform | Compile-time | Learning Curve |
|-----------|---------------------|--------------|----------------|
| **Koin** | ✅ Full support | ❌ Runtime | Low |
| Dagger/Hilt | ❌ Android only | ✅ | High |
| Kodein | ✅ Full support | ❌ Runtime | Medium |
| kotlin-inject | ✅ Partial | ✅ | Medium |

### Why Koin Wins for KMP

1. **First-class KMP support**: Designed for multiplatform from the start
2. **Simple DSL**: Human-readable module definitions
3. **No code generation**: Works anywhere Kotlin runs
4. **Koin Annotations**: Optional compile-time safety (Koin 4.0+)
5. **Compose integration**: `koinViewModel()` works seamlessly
6. **Active development**: Regular updates, strong community

```kotlin
// Koin DSL is readable and concise
val appModule = module {
    single { AppDatabase.create(get()) }
    single<NotesRepository> { NotesRepositoryImpl(get()) }
    viewModel { NotesViewModel(get()) }
}
```