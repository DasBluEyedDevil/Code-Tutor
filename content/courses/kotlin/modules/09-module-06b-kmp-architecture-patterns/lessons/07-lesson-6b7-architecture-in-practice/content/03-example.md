---
type: "EXAMPLE"
title: "Dependency Injection Setup"
---

Koin modules wiring everything together:

```kotlin
// ========== di/Modules.kt ==========
val dataModule = module {
    // Database
    single { AppDatabase(get()) }
    single { NoteDao(get()) }
    
    // API
    single {
        HttpClient {
            install(ContentNegotiation) { json() }
        }
    }
    single<NoteApi> { NoteApiImpl(get()) }
    
    // Repository
    single<NoteRepository> { NoteRepositoryImpl(get(), get()) }
}

val presentationModule = module {
    factory { NotesViewModel(get()) }
    factory { (noteId: String) -> NoteDetailViewModel(noteId, get()) }
}

val platformModule = module {
    // Platform-specific: DatabaseDriverFactory, DateFormatter, etc.
}

fun initKoin(platformModule: Module) {
    startKoin {
        modules(dataModule, presentationModule, platformModule)
    }
}

// ========== Usage in Compose ==========
@Composable
fun NotesScreen() {
    val viewModel: NotesViewModel = koinInject()
    
    DisposableEffect(Unit) {
        onDispose { viewModel.onCleared() }
    }
    
    // Screen content
}

@Composable
fun NoteDetailScreen(noteId: String) {
    val viewModel: NoteDetailViewModel = koinInject { parametersOf(noteId) }
    
    DisposableEffect(Unit) {
        onDispose { viewModel.onCleared() }
    }
    
    // Screen content
}
```
