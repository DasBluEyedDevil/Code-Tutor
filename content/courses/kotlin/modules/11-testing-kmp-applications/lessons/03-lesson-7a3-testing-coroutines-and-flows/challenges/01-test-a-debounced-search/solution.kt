class SearchViewModelTest {
    private lateinit var repository: FakeNoteRepository
    
    @BeforeTest
    fun setup() {
        repository = FakeNoteRepository()
        repository.addTestNote("Kotlin Guide", "Learn Kotlin")
        repository.addTestNote("Swift Tutorial", "iOS development")
        repository.addTestNote("Kotlin Coroutines", "Async programming")
    }
    
    @Test
    fun `debounce prevents rapid searches`() = runTest {
        val viewModel = SearchViewModel(
            repository = repository,
            dispatcher = StandardTestDispatcher(testScheduler)
        )
        
        // Rapid typing
        viewModel.search("K")
        advanceTimeBy(100)
        viewModel.search("Ko")
        advanceTimeBy(100)
        viewModel.search("Kot")
        advanceTimeBy(100)
        
        // Results should still be empty (debounce not elapsed)
        assertTrue(viewModel.results.value.isEmpty())
        
        // Wait for debounce
        advanceTimeBy(300)
        
        // Now search should execute with final query "Kot"
        assertEquals(2, viewModel.results.value.size)  // "Kotlin Guide" and "Kotlin Coroutines"
    }
    
    @Test
    fun `search executes after debounce period`() = runTest {
        val viewModel = SearchViewModel(
            repository = repository,
            dispatcher = StandardTestDispatcher(testScheduler)
        )
        
        viewModel.search("Swift")
        
        // Before debounce
        advanceTimeBy(200)
        assertTrue(viewModel.results.value.isEmpty())
        
        // After debounce
        advanceTimeBy(150)
        assertEquals(1, viewModel.results.value.size)
        assertEquals("Swift Tutorial", viewModel.results.value[0].title)
    }
    
    @Test
    fun `new search cancels previous`() = runTest {
        // Simulate slow search
        repository.searchDelay = 500
        
        val viewModel = SearchViewModel(
            repository = repository,
            dispatcher = StandardTestDispatcher(testScheduler)
        )
        
        viewModel.search("Kotlin")
        advanceTimeBy(350)  // Past debounce, search started
        
        // New search while old one is running
        viewModel.search("Swift")
        advanceTimeBy(350)  // Past debounce for new search
        advanceTimeBy(500)  // Complete new search
        
        // Should have Swift results, not Kotlin
        assertEquals(1, viewModel.results.value.size)
        assertEquals("Swift Tutorial", viewModel.results.value[0].title)
    }
}