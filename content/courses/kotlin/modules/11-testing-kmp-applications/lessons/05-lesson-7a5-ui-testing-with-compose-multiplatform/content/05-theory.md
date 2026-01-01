---
type: "THEORY"
title: "Testing with ViewModels"
---

For testing screens with ViewModels, inject fakes:

```kotlin
class NotesScreenTest {
    @get:Rule
    val composeTestRule = createComposeRule()
    
    private lateinit var fakeRepository: FakeNoteRepository
    private lateinit var viewModel: NotesViewModel
    
    @BeforeTest
    fun setup() {
        fakeRepository = FakeNoteRepository()
        viewModel = NotesViewModel(fakeRepository)
    }
    
    @Test
    fun `displays notes from viewmodel`() {
        fakeRepository.addTestNote("Note 1", "Content 1")
        fakeRepository.addTestNote("Note 2", "Content 2")
        
        composeTestRule.setContent {
            NotesScreen(viewModel = viewModel)
        }
        
        composeTestRule.waitUntil {
            composeTestRule
                .onAllNodesWithTag("note_card")
                .fetchSemanticsNodes().size == 2
        }
        
        composeTestRule
            .onNodeWithText("Note 1")
            .assertIsDisplayed()
        
        composeTestRule
            .onNodeWithText("Note 2")
            .assertIsDisplayed()
    }
    
    @Test
    fun `shows loading indicator initially`() {
        fakeRepository.searchDelay = 1000  // Slow loading
        
        composeTestRule.setContent {
            NotesScreen(viewModel = viewModel)
        }
        
        composeTestRule
            .onNodeWithTag("loading_indicator")
            .assertIsDisplayed()
    }
    
    @Test
    fun `shows empty state when no notes`() {
        composeTestRule.setContent {
            NotesScreen(viewModel = viewModel)
        }
        
        composeTestRule.waitUntil {
            !viewModel.state.value.isLoading
        }
        
        composeTestRule
            .onNodeWithText("No notes yet")
            .assertIsDisplayed()
    }
}
```