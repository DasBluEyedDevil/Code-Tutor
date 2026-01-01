---
type: "THEORY"
title: "Full Feature Integration Tests"
---

Test entire features from ViewModel to database:

```kotlin
class AddNoteFeatureTest {
    private lateinit var driver: SqlDriver
    private lateinit var database: AppDatabase
    private lateinit var repository: NoteRepository
    private lateinit var addNoteUseCase: AddNoteUseCase
    private lateinit var viewModel: AddNoteViewModel
    
    @BeforeTest
    fun setup() {
        // Real database (in-memory)
        driver = JdbcSqliteDriver(JdbcSqliteDriver.IN_MEMORY)
        AppDatabase.Schema.create(driver)
        database = AppDatabase(driver)
        
        // Real repository
        repository = NoteRepositoryImpl(database.noteQueries)
        
        // Real use case
        addNoteUseCase = AddNoteUseCase(
            repository = repository,
            validateNote = ValidateNoteUseCase()
        )
        
        // ViewModel with test dispatcher
        viewModel = AddNoteViewModel(
            addNoteUseCase = addNoteUseCase,
            dispatcher = StandardTestDispatcher()
        )
    }
    
    @AfterTest
    fun teardown() {
        driver.close()
    }
    
    @Test
    fun `adding note persists to database and updates state`() = runTest {
        // Act
        viewModel.onTitleChanged("Integration Test")
        viewModel.onContentChanged("This tests the full flow")
        viewModel.saveNote()
        advanceUntilIdle()
        
        // Assert - ViewModel state
        assertTrue(viewModel.state.value.isSaved)
        assertNull(viewModel.state.value.error)
        
        // Assert - Database
        val notes = repository.getAll()
        assertEquals(1, notes.size)
        assertEquals("Integration Test", notes[0].title)
    }
    
    @Test
    fun `validation error prevents database write`() = runTest {
        // Act - try to save with empty title
        viewModel.onTitleChanged("")  // Invalid
        viewModel.onContentChanged("Content")
        viewModel.saveNote()
        advanceUntilIdle()
        
        // Assert - ViewModel shows error
        assertFalse(viewModel.state.value.isSaved)
        assertNotNull(viewModel.state.value.error)
        
        // Assert - Nothing in database
        val notes = repository.getAll()
        assertTrue(notes.isEmpty())
    }
}
```