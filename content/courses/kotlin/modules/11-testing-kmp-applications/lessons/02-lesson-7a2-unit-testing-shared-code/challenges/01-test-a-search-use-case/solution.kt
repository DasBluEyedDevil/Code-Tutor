class SearchNotesUseCaseTest {
    private lateinit var repository: FakeNoteRepository
    private lateinit var useCase: SearchNotesUseCase
    
    @BeforeTest
    fun setup() {
        repository = FakeNoteRepository()
        useCase = SearchNotesUseCase(repository)
        
        // Add test data
        repository.addTestNote("Shopping List", "Buy milk and eggs")
        repository.addTestNote("Meeting Notes", "Discuss project timeline")
        repository.addTestNote("Recipe", "How to make pancakes")
    }
    
    @Test
    fun `empty query returns all notes`() = runTest {
        val result = useCase.execute("")
        
        assertEquals(3, result.size)
    }
    
    @Test
    fun `blank query returns all notes`() = runTest {
        val result = useCase.execute("   ")
        
        assertEquals(3, result.size)
    }
    
    @Test
    fun `query matching title returns matching notes`() = runTest {
        val result = useCase.execute("Shopping")
        
        assertEquals(1, result.size)
        assertEquals("Shopping List", result[0].title)
    }
    
    @Test
    fun `query matching content returns matching notes`() = runTest {
        val result = useCase.execute("pancakes")
        
        assertEquals(1, result.size)
        assertEquals("Recipe", result[0].title)
    }
    
    @Test
    fun `query is case insensitive`() = runTest {
        val result = useCase.execute("MEETING")
        
        assertEquals(1, result.size)
        assertEquals("Meeting Notes", result[0].title)
    }
    
    @Test
    fun `no matches returns empty list`() = runTest {
        val result = useCase.execute("nonexistent")
        
        assertTrue(result.isEmpty())
    }
    
    @Test
    fun `partial match works`() = runTest {
        val result = useCase.execute("milk")
        
        assertEquals(1, result.size)
    }
}