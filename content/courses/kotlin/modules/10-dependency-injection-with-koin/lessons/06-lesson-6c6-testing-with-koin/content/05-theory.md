---
type: "THEORY"
title: "Overriding Modules for Tests"
---

Use `loadKoinModules` with `override = true` to replace specific dependencies:

```kotlin
class NotesViewModelTest : KoinTest {
    
    @BeforeTest
    fun setUp() {
        startKoin {
            modules(commonModule, platformModule)
        }
        
        // Override just the repository with a fake
        loadKoinModules(
            module {
                single<NotesRepository>(override = true) { 
                    FakeNotesRepository() 
                }
            }
        )
    }
}
```

### declare() for In-Test Overrides

```kotlin
class NotesViewModelTest : KoinTest {
    
    @Test
    fun `test with custom mock`() = runTest {
        // Override for this specific test
        val mockRepo = mockk<NotesRepository> {
            coEvery { getAllNotes() } returns listOf(
                Note("1", "Mocked", "Content", Clock.System.now())
            )
        }
        
        declare<NotesRepository> { mockRepo }
        
        val viewModel: NotesListViewModel = get()
        viewModel.loadNotes()
        
        assertEquals("Mocked", viewModel.state.value.notes[0].title)
    }
}
```