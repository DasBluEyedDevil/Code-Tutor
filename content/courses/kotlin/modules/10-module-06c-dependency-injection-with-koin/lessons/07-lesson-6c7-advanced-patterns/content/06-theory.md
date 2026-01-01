---
type: "THEORY"
title: "Multi-Module App Organization"
---

For large apps with Gradle modules:

```
app/
├── :core               # Core utilities, base classes
├── :feature:notes      # Notes feature
├── :feature:settings   # Settings feature
├── :feature:profile    # Profile feature
└── :shared             # KMP shared code
```

### Each Feature Exports Its Module

```kotlin
// feature/notes/di/NotesModule.kt
val notesModule = module {
    singleOf(::NotesRepositoryImpl) { bind<NotesRepository>() }
    viewModel { NotesListViewModel(get()) }
    viewModel { params -> NoteDetailViewModel(params.get(), get()) }
}

// feature/settings/di/SettingsModule.kt
val settingsModule = module {
    singleOf(::SettingsRepositoryImpl) { bind<SettingsRepository>() }
    viewModel { SettingsViewModel(get()) }
}
```

### App Combines All Modules

```kotlin
// app/di/AppModule.kt
val appModules = listOf(
    coreModule,
    platformModule,
    // Feature modules
    notesModule,
    settingsModule,
    profileModule
)

class App : Application() {
    override fun onCreate() {
        super.onCreate()
        startKoin {
            androidContext(this@App)
            modules(appModules)
        }
    }
}
```