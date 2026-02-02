---
type: "WARNING"
title: "Platform Dependency Anti-Patterns"
---

### ❌ Leaking platform types to common code

```kotlin
// commonMain - BAD
class NotesRepository(
    val context: Context  // Android type in common code!
) { ... }
```

**Fix**: Use interfaces or expect/actual.

### ❌ Large platform modules

```kotlin
// Don't put everything in one file
val platformModule = module {
    // 50+ definitions...
}
```

**Fix**: Split by feature:
```kotlin
val platformDatabaseModule = module { ... }
val platformNetworkModule = module { ... }
val platformAnalyticsModule = module { ... }

val platformModules = listOf(
    platformDatabaseModule,
    platformNetworkModule,
    platformAnalyticsModule
)
```

### ❌ Duplicate definitions across platforms

```kotlin
// androidMain
val platformModule = module {
    single { NotesRepositoryImpl(get()) }  // Same as iOS!
}

// iosMain
val platformModule = module {
    single { NotesRepositoryImpl(get()) }  // Duplicated!
}
```

**Fix**: Keep shared implementations in commonModule.