---
type: "THEORY"
title: "Testing with Real SQLite"
---

For integration tests, use an in-memory SQLite database:

```kotlin
class NoteRepositoryIntegrationTest {
    private lateinit var driver: SqlDriver
    private lateinit var database: AppDatabase
    private lateinit var repository: NoteRepositoryImpl
    
    @BeforeTest
    fun setup() {
        // In-memory database - fast, isolated
        driver = JdbcSqliteDriver(JdbcSqliteDriver.IN_MEMORY)
        AppDatabase.Schema.create(driver)
        database = AppDatabase(driver)
        repository = NoteRepositoryImpl(database.noteQueries)
    }
    
    @AfterTest
    fun teardown() {
        driver.close()
    }
    
    @Test
    fun `full CRUD cycle works`() = runTest {
        // Create
        val noteId = repository.add("Title", "Content")
        assertNotNull(noteId)
        
        // Read
        val note = repository.getById(noteId)
        assertNotNull(note)
        assertEquals("Title", note.title)
        
        // Update
        repository.update(note.copy(title = "Updated"))
        val updated = repository.getById(noteId)
        assertEquals("Updated", updated?.title)
        
        // Delete
        repository.delete(noteId)
        val deleted = repository.getById(noteId)
        assertNull(deleted)
    }
    
    @Test
    fun `search finds matching notes`() = runTest {
        repository.add("Kotlin Guide", "Learn Kotlin basics")
        repository.add("Swift Guide", "Learn Swift basics")
        repository.add("Kotlin Advanced", "Coroutines and flows")
        
        val results = repository.search("Kotlin")
        
        assertEquals(2, results.size)
        assertTrue(results.all { "Kotlin" in it.title })
    }
}
```