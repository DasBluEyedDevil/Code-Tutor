---
type: "EXAMPLE"
title: "Complete Annotated Module"
---

Here's a full example using annotations:

```kotlin
// ========== Data Layer ==========

@Module
@ComponentScan("com.example.app.data")
class DataModule

@Single
class AppDatabase(
    driver: SqlDriver
) {
    val notesQueries = NotesQueries(driver)
}

@Single
@Bind(NotesRepository::class)
class NotesRepositoryImpl(
    private val database: AppDatabase,
    private val api: NotesApi
) : NotesRepository {
    override suspend fun getAllNotes(): List<Note> {
        return database.notesQueries.getAll().executeAsList()
    }
    // ...
}

// ========== Domain Layer ==========

@Module
@ComponentScan("com.example.app.domain")
class DomainModule

@Factory
class GetAllNotesUseCase(
    private val repository: NotesRepository
) {
    suspend operator fun invoke(): List<Note> {
        return repository.getAllNotes()
    }
}

@Factory
class CreateNoteUseCase(
    private val repository: NotesRepository
) {
    suspend operator fun invoke(title: String, content: String): Note {
        return repository.createNote(title, content)
    }
}

// ========== Presentation Layer ==========

@Module
@ComponentScan("com.example.app.presentation")
class PresentationModule

@KoinViewModel
class NotesListViewModel(
    private val getAllNotes: GetAllNotesUseCase,
    private val createNote: CreateNoteUseCase
) : ViewModel() {
    // ...
}

// ========== App Initialization ==========

// Generated: DataModuleModule, DomainModuleModule, PresentationModuleModule

fun initKoin() {
    startKoin {
        modules(
            DataModuleModule().module,
            DomainModuleModule().module,
            PresentationModuleModule().module
        )
    }
}
```
