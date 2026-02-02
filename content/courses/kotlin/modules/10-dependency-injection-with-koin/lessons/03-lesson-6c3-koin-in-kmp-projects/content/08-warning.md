---
type: "WARNING"
title: "KMP Koin Pitfalls"
---

### ❌ Putting platform types in commonModule

```kotlin
// commonMain - BAD
val commonModule = module {
    single { AndroidSqliteDriver(...) }  // Won't compile on iOS!
}
```

**Fix**: Platform-specific implementations go in platform source sets.

### ❌ Forgetting to provide Android Context

```kotlin
// Android drivers need Context
single { AndroidSqliteDriver(..., context = get()) }  // Where does Context come from?
```

**Fix**: Use `androidContext(this)` in `startKoin`.

### ❌ Multiple startKoin calls

```kotlin
// If you call startKoin twice, you get an error
startKoin { ... }  // First call
startKoin { ... }  // Error: A Koin Application has already been started
```

**Fix**: Call `startKoin` once at app startup. Use `loadKoinModules` to add modules dynamically.