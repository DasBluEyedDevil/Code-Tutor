---
type: "THEORY"
title: "Performance Best Practices"
---

### 1. Use single for Heavy Objects

```kotlin
// ✅ Database created once
single { AppDatabase.create() }

// ❌ Database created every time (expensive!)
factory { AppDatabase.create() }
```

### 2. Lazy Load Non-Critical Dependencies

```kotlin
viewModel {
    HomeViewModel(
        essentialRepo = get(),          // Needed immediately
        analyticsLazy = inject(),        // Loaded when first used
        crashReportingLazy = inject()    // Loaded when first used
    )
}
```

### 3. Split Large Modules

```kotlin
// ❌ One huge module with 100 definitions
val everythingModule = module {
    // 100 definitions...
}

// ✅ Split by feature/layer
val modules = listOf(
    coreModule,
    networkModule,
    databaseModule,
    notesModule,
    settingsModule
)
```

### 4. Avoid get() in Hot Paths

```kotlin
class NotesListScreen {
    @Composable
    fun Content() {
        // ❌ get() called on every recomposition
        val logger: Logger = LocalKoinScope.current.get()
        
        // ✅ Remember the value
        val logger: Logger = remember { getKoin().get() }
        
        // ✅ Or use koinInject() which handles this
        val logger: Logger = koinInject()
    }
}
```