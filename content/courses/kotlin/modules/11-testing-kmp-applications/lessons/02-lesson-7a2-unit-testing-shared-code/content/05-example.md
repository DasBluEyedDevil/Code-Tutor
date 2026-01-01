---
type: "EXAMPLE"
title: "Testing With Fakes"
---

Using our fake repository to test a use case:

```kotlin
// Production code
class AddNoteUseCase(
    private val repository: NoteRepository,
    private val validateNote: ValidateNoteUseCase
) {
    suspend fun execute(title: String, content: String): Result<Long> {
        return when (val validation = validateNote.execute(title, content)) {
            is ValidationResult.Valid -> {
                runCatching { repository.add(title, content) }
            }
            is ValidationResult.Invalid -> {
                Result.failure(ValidationException(validation.errors))
            }
        }
    }
}

class ValidationException(val errors: List<String>) : Exception()

// Test code
class AddNoteUseCaseTest {
    private lateinit var repository: FakeNoteRepository
    private lateinit var addNoteUseCase: AddNoteUseCase
    
    @BeforeTest
    fun setup() {
        repository = FakeNoteRepository()
        addNoteUseCase = AddNoteUseCase(
            repository = repository,
            validateNote = ValidateNoteUseCase()
        )
    }
    
    @Test
    fun `adding valid note succeeds`() = runTest {
        val result = addNoteUseCase.execute("Title", "Content")
        
        assertTrue(result.isSuccess)
        assertEquals(1, repository.getAll().size)
    }
    
    @Test
    fun `adding note with empty title fails validation`() = runTest {
        val result = addNoteUseCase.execute("", "Content")
        
        assertTrue(result.isFailure)
        val exception = result.exceptionOrNull()
        assertTrue(exception is ValidationException)
        assertEquals(0, repository.getAll().size)  // Nothing added
    }
    
    @Test
    fun `repository error is propagated`() = runTest {
        repository.shouldThrowOnAdd = true
        
        val result = addNoteUseCase.execute("Title", "Content")
        
        assertTrue(result.isFailure)
        assertTrue(result.exceptionOrNull()?.message == "Simulated error")
    }
    
    @Test
    fun `note id is returned on success`() = runTest {
        val result = addNoteUseCase.execute("Title", "Content")
        
        assertEquals(1L, result.getOrNull())
    }
}
```
