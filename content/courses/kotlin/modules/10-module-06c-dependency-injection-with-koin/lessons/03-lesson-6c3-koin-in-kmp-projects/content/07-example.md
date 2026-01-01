---
type: "EXAMPLE"
title: "Complete KMP Koin Setup"
---

Here's a full working example:

```kotlin
// ========== commonMain/kotlin/di/Modules.kt ==========

// Shared module - no platform dependencies
val commonModule = module {
    // Repository (needs platform-provided database)
    singleOf(::NotesRepositoryImpl) { bind<NotesRepository>() }
    
    // Use cases
    factory { GetAllNotesUseCase(get()) }
    factory { CreateNoteUseCase(get()) }
    
    // ViewModels
    viewModel { NotesListViewModel(get()) }
    viewModel { params -> NoteDetailViewModel(params.get(), get()) }
}

// ========== commonMain/kotlin/di/KoinHelper.kt ==========

fun initKoin(platformModule: Module): KoinApplication {
    return startKoin {
        modules(platformModule, commonModule)
    }
}

// iOS helper (Swift can't call startKoin directly)
object KoinHelper {
    fun start() {
        initKoin(platformModule)
    }
    
    // Helper to get instances from Swift
    fun getNotesViewModel(): NotesListViewModel {
        return KoinPlatform.getKoin().get()
    }
}

// ========== androidMain/kotlin/di/PlatformModule.kt ==========

actual val platformModule = module {
    single {
        AndroidSqliteDriver(
            schema = AppDatabase.Schema,
            context = get(),
            name = "notes.db"
        )
    }
    single { AppDatabase(get()) }
}

// ========== iosMain/kotlin/di/PlatformModule.kt ==========

actual val platformModule = module {
    single {
        NativeSqliteDriver(
            schema = AppDatabase.Schema,
            name = "notes.db"
        )
    }
    single { AppDatabase(get()) }
}

// ========== Android Application ==========

class NotesApp : Application() {
    override fun onCreate() {
        super.onCreate()
        initKoin(platformModule) {
            androidContext(this@NotesApp)
        }
    }
}

// ========== iOS AppDelegate.swift ==========
// import shared
// KoinHelper().start()
```
