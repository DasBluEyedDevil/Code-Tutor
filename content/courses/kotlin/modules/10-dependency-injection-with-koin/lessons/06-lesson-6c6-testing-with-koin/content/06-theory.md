---
type: "THEORY"
title: "Integration Testing"
---

Test multiple layers together with real implementations:

```kotlin
// Integration test module - uses real implementations but fake data sources
val integrationTestModule = module {
    // Real implementations
    singleOf(::NotesRepositoryImpl) { bind<NotesRepository>() }
    factory { GetAllNotesUseCase(get()) }
    factory { CreateNoteUseCase(get()) }
    viewModel { NotesListViewModel(get()) }
    
    // Fake data layer
    single { createInMemoryDatabase() }
    single { FakeNotesApi() }
}

class NotesIntegrationTest : KoinTest {
    
    @BeforeTest
    fun setUp() {
        startKoin {
            modules(integrationTestModule)
        }
    }
    
    @Test
    fun `full flow - create and retrieve note`() = runTest {
        val viewModel: NotesListViewModel = get()
        
        // Create note goes through ViewModel -> UseCase -> Repository -> Database
        viewModel.createNote("Integration Test", "Testing full stack")
        advanceUntilIdle()
        
        // Verify it persisted and can be retrieved
        val notes = viewModel.state.value.notes
        assertEquals(1, notes.size)
        assertEquals("Integration Test", notes[0].title)
    }
}
```