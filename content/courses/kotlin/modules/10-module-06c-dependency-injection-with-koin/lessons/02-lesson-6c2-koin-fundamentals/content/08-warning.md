---
type: "WARNING"
title: "Common Koin Mistakes"
---

### ❌ Circular Dependencies

```kotlin
val module = module {
    single { ServiceA(get()) }  // Needs ServiceB
    single { ServiceB(get()) }  // Needs ServiceA
}
// Runtime error: StackOverflowException
```

**Fix**: Redesign to break the cycle, often with an interface or lazy injection.

### ❌ Missing Dependencies

```kotlin
val module = module {
    single { NotesRepository(get()) }  // Needs Database
    // Forgot to define Database!
}
// Runtime error when resolving NotesRepository
```

**Fix**: Use Koin's `checkModules()` in tests to catch early.

### ❌ Confusing single vs factory for ViewModels

```kotlin
// ❌ Wrong: ViewModel reused across screens
single { NotesViewModel(get()) }

// ✅ Right: Use viewModel { } for proper lifecycle
viewModel { NotesViewModel(get()) }
```