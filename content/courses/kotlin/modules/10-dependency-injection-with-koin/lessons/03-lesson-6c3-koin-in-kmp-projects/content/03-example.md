---
type: "EXAMPLE"
title: "Common Module"
---

The shared module defines common dependencies:

```kotlin
// commonMain/kotlin/di/CommonModule.kt
import org.koin.dsl.module
import org.koin.core.module.dsl.viewModel
import org.koin.core.module.dsl.singleOf
import org.koin.core.module.dsl.bind

val commonModule = module {
    // Repository depends on platform-provided database
    singleOf(::NotesRepositoryImpl) { bind<NotesRepository>() }
    
    // Use cases
    factory { GetAllNotesUseCase(get()) }
    factory { CreateNoteUseCase(get()) }
    factory { DeleteNoteUseCase(get()) }
    factory { SearchNotesUseCase(get()) }
    
    // ViewModels
    viewModel { NotesListViewModel(get()) }
    viewModel { parameters ->
        NoteDetailViewModel(
            noteId = parameters.get(),
            repository = get()
        )
    }
}

// Network module (shared Ktor client)
val networkModule = module {
    single { createHttpClient() }
    single { NotesApi(get()) }
}

fun createHttpClient(): HttpClient {
    return HttpClient {
        install(ContentNegotiation) {
            json(Json {
                ignoreUnknownKeys = true
                prettyPrint = true
            })
        }
        install(Logging) {
            level = LogLevel.INFO
        }
    }
}
```
