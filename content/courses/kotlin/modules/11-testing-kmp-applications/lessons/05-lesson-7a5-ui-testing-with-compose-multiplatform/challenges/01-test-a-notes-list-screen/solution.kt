class NotesListScreenTest {
    @get:Rule
    val composeTestRule = createComposeRule()
    
    private lateinit var fakeRepo: FakeNoteRepository
    private lateinit var viewModel: NotesListViewModel
    private var clickedNote: Note? = null
    
    @BeforeTest
    fun setup() {
        fakeRepo = FakeNoteRepository()
        clickedNote = null
    }
    
    private fun createViewModel(): NotesListViewModel {
        return NotesListViewModel(fakeRepo, StandardTestDispatcher())
    }
    
    @Test
    fun `shows loading indicator while loading`() {
        fakeRepo.searchDelay = 10000  // Very slow
        viewModel = createViewModel()
        
        composeTestRule.setContent {
            NotesListScreen(
                viewModel = viewModel,
                onNoteClick = {}
            )
        }
        
        composeTestRule
            .onNodeWithTag("loading")
            .assertIsDisplayed()
    }
    
    @Test
    fun `shows empty state when no notes`() = runTest {
        viewModel = createViewModel()
        advanceUntilIdle()
        
        composeTestRule.setContent {
            NotesListScreen(
                viewModel = viewModel,
                onNoteClick = {}
            )
        }
        
        composeTestRule
            .onNodeWithTag("empty_state")
            .assertIsDisplayed()
        
        composeTestRule
            .onNodeWithText("No notes found")
            .assertIsDisplayed()
    }
    
    @Test
    fun `displays notes when available`() = runTest {
        fakeRepo.addTestNote("First Note", "Content 1")
        fakeRepo.addTestNote("Second Note", "Content 2")
        viewModel = createViewModel()
        advanceUntilIdle()
        
        composeTestRule.setContent {
            NotesListScreen(
                viewModel = viewModel,
                onNoteClick = {}
            )
        }
        
        composeTestRule.waitUntil {
            composeTestRule.onAllNodesWithTagPrefix("note_")
                .fetchSemanticsNodes().size == 2
        }
        
        composeTestRule
            .onNodeWithText("First Note")
            .assertIsDisplayed()
        
        composeTestRule
            .onNodeWithText("Second Note")
            .assertIsDisplayed()
    }
    
    @Test
    fun `search filters notes`() = runTest {
        fakeRepo.addTestNote("Kotlin Guide", "Learn Kotlin")
        fakeRepo.addTestNote("Swift Guide", "Learn Swift")
        viewModel = createViewModel()
        advanceUntilIdle()
        
        composeTestRule.setContent {
            NotesListScreen(
                viewModel = viewModel,
                onNoteClick = {}
            )
        }
        
        // Type in search
        composeTestRule
            .onNodeWithTag("search_bar")
            .performTextInput("Kotlin")
        
        advanceUntilIdle()  // Wait for debounce
        composeTestRule.waitForIdle()
        
        composeTestRule
            .onNodeWithText("Kotlin Guide")
            .assertIsDisplayed()
        
        composeTestRule
            .onNodeWithText("Swift Guide")
            .assertDoesNotExist()
    }
    
    @Test
    fun `clicking note calls onNoteClick with correct note`() = runTest {
        fakeRepo.addTestNote("Click Me", "Content")
        viewModel = createViewModel()
        advanceUntilIdle()
        
        composeTestRule.setContent {
            NotesListScreen(
                viewModel = viewModel,
                onNoteClick = { clickedNote = it }
            )
        }
        
        composeTestRule.waitForIdle()
        
        composeTestRule
            .onNodeWithText("Click Me")
            .performClick()
        
        assertNotNull(clickedNote)
        assertEquals("Click Me", clickedNote?.title)
    }
}