---
type: "EXAMPLE"
title: "Clean Architecture Organization"
---

Organize modules by architectural layer:

```kotlin
// ========== Module Structure ==========
//
// di/
// ├── modules/
// │   ├── CoreModule.kt
// │   ├── DataModule.kt  
// │   ├── DomainModule.kt
// │   ├── PresentationModule.kt
// │   └── PlatformModule.kt (expect/actual)
// ├── KoinInit.kt
// └── ModuleList.kt

// ========== CoreModule.kt ==========
val coreModule = module {
    // Shared utilities
    single { Clock.System }
    single<CoroutineDispatcher>(named(DispatcherType.IO)) { Dispatchers.IO }
    single<CoroutineDispatcher>(named(DispatcherType.MAIN)) { Dispatchers.Main }
}

// ========== DataModule.kt ==========
val dataModule = module {
    // Network
    single { createHttpClient() }
    single { NotesApi(get()) }
    
    // Local storage (uses platform-provided driver)
    single { get<AppDatabase>().notesQueries }
    
    // Repository
    singleOf(::NotesRepositoryImpl) { bind<NotesRepository>() }
}

// ========== DomainModule.kt ==========
val domainModule = module {
    // Use cases - stateless, so factory is appropriate
    factory { GetAllNotesUseCase(get()) }
    factory { GetNoteByIdUseCase(get()) }
    factory { CreateNoteUseCase(get()) }
    factory { UpdateNoteUseCase(get()) }
    factory { DeleteNoteUseCase(get()) }
    factory { SearchNotesUseCase(get()) }
}

// ========== PresentationModule.kt ==========
val presentationModule = module {
    // ViewModels
    viewModel { 
        NotesListViewModel(
            getAllNotes = get(),
            deleteNote = get(),
            dispatcher = get(named(DispatcherType.MAIN))
        )
    }
    
    viewModel { params ->
        NoteDetailViewModel(
            noteId = params.get(),
            getNoteById = get(),
            updateNote = get()
        )
    }
    
    viewModel {
        CreateNoteViewModel(
            createNote = get()
        )
    }
    
    viewModel { params ->
        SearchViewModel(
            initialQuery = params.getOrNull() ?: "",
            searchNotes = get()
        )
    }
}

// ========== ModuleList.kt ==========
val sharedModules = listOf(
    coreModule,
    dataModule,
    domainModule,
    presentationModule
)

fun allModules(platformModule: Module) = 
    sharedModules + platformModule

// ========== KoinInit.kt ==========
fun initKoin(
    platformModule: Module,
    appDeclaration: KoinAppDeclaration = {}
) {
    startKoin {
        appDeclaration()
        modules(allModules(platformModule))
    }
}
```
