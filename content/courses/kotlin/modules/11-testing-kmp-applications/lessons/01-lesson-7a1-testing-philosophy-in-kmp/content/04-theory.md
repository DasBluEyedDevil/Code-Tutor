---
type: "THEORY"
title: "What to Test in Each Layer"
---

### Domain Layer (Highest Priority)

**What**: Use cases, business logic, validation rules
**How**: Pure unit tests, no mocking needed
**Where**: `commonTest`

```kotlin
class ValidateEmailUseCaseTest {
    private val useCase = ValidateEmailUseCase()
    
    @Test
    fun `valid email returns success`() {
        assertTrue(useCase("user@example.com").isValid)
    }
    
    @Test
    fun `email without @ returns failure`() {
        assertFalse(useCase("userexample.com").isValid)
    }
}
```

### Data Layer (High Priority)

**What**: Repositories, data mappers, API clients
**How**: Integration tests with test doubles
**Where**: `commonTest` with in-memory database

```kotlin
class NoteRepositoryTest {
    private lateinit var repository: NoteRepository
    private lateinit var database: AppDatabase
    
    @BeforeTest
    fun setup() {
        val driver = JdbcSqliteDriver(JdbcSqliteDriver.IN_MEMORY)
        AppDatabase.Schema.create(driver)
        database = AppDatabase(driver)
        repository = NoteRepositoryImpl(database)
    }
}
```

### Presentation Layer (Medium Priority)

**What**: ViewModels, state management
**How**: Unit tests with fake repositories
**Where**: `commonTest`

```kotlin
class NotesViewModelTest {
    @Test
    fun `loading notes updates state`() = runTest {
        val fakeRepo = FakeNoteRepository()
        val viewModel = NotesViewModel(fakeRepo)
        
        advanceUntilIdle()
        
        assertFalse(viewModel.state.value.isLoading)
    }
}
```

### UI Layer (Lower Priority for Shared)

**What**: Screen behavior, navigation
**How**: Compose UI tests or platform-specific tests
**Where**: `androidTest`, `iosTest`, or `commonTest` with Compose test utilities