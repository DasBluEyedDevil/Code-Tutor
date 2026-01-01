---
type: "EXAMPLE"
title: "Complete Module Example"
---

Here's a well-organized Koin module:

```kotlin
// di/AppModule.kt
import org.koin.dsl.module
import org.koin.core.module.dsl.viewModel
import org.koin.core.module.dsl.singleOf
import org.koin.core.module.dsl.bind

// Data layer module
val dataModule = module {
    // Database
    single { AppDatabase.create() }
    single { get<AppDatabase>().notesQueries }
    
    // Network
    single { createHttpClient() }
    single { NotesApi(get()) }
    
    // Repository: implementation bound to interface
    singleOf(::NotesRepositoryImpl) { bind<NotesRepository>() }
}

// Domain layer module
val domainModule = module {
    // Use cases - created fresh each time
    factory { GetAllNotesUseCase(get()) }
    factory { CreateNoteUseCase(get()) }
    factory { DeleteNoteUseCase(get()) }
    factory { SearchNotesUseCase(get()) }
}

// Presentation layer module
val presentationModule = module {
    viewModel { NotesListViewModel(get()) }
    viewModel { parameters -> 
        NoteDetailViewModel(
            noteId = parameters.get(),
            repository = get()
        )
    }
}

// Combine all modules
val allModules = listOf(
    dataModule,
    domainModule,
    presentationModule
)
```
