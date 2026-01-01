class NotesSyncManagerIntegrationTest {
    private lateinit var driver: SqlDriver
    private lateinit var database: AppDatabase
    private lateinit var repository: NoteRepositoryImpl
    private lateinit var mockClient: HttpClient
    private lateinit var apiClient: NotesApiClient
    private lateinit var syncManager: NotesSyncManager
    
    @BeforeTest
    fun setup() {
        driver = JdbcSqliteDriver(JdbcSqliteDriver.IN_MEMORY)
        AppDatabase.Schema.create(driver)
        database = AppDatabase(driver)
        repository = NoteRepositoryImpl(database.noteQueries)
    }
    
    @AfterTest
    fun teardown() {
        driver.close()
    }
    
    private fun setupMockApi(notes: List<NoteDto>) {
        mockClient = HttpClient(MockEngine) {
            install(ContentNegotiation) { json() }
            engine {
                addHandler { respond(
                    content = Json.encodeToString(notes),
                    status = HttpStatusCode.OK,
                    headers = headersOf("Content-Type", "application/json")
                )}
            }
        }
        apiClient = NotesApiClient(mockClient, "https://api.example.com")
        syncManager = NotesSyncManager(apiClient, repository)
    }
    
    @Test
    fun `sync adds new notes to empty database`() = runTest {
        val remoteNotes = listOf(
            NoteDto(1, "Remote Note 1", "Content 1", 1000),
            NoteDto(2, "Remote Note 2", "Content 2", 2000)
        )
        setupMockApi(remoteNotes)
        
        val result = syncManager.sync()
        
        assertTrue(result.isSuccess)
        assertEquals(2, result.getOrNull())
        
        val localNotes = repository.getAll()
        assertEquals(2, localNotes.size)
        assertEquals("Remote Note 1", localNotes.find { it.id == 1L }?.title)
    }
    
    @Test
    fun `sync updates existing notes if remote is newer`() = runTest {
        // Add local note with old timestamp
        repository.insert(Note(1, "Old Title", "Old Content", createdAt = 500, updatedAt = 500))
        
        // Remote has newer version
        val remoteNotes = listOf(
            NoteDto(1, "Updated Title", "Updated Content", updatedAt = 1000)
        )
        setupMockApi(remoteNotes)
        
        syncManager.sync()
        
        val localNote = repository.getById(1)
        assertEquals("Updated Title", localNote?.title)
        assertEquals("Updated Content", localNote?.content)
    }
    
    @Test
    fun `sync does not overwrite if local is newer`() = runTest {
        // Add local note with newer timestamp
        repository.insert(Note(1, "Local Title", "Local Content", createdAt = 2000, updatedAt = 2000))
        
        // Remote has older version
        val remoteNotes = listOf(
            NoteDto(1, "Old Remote Title", "Old Remote Content", updatedAt = 1000)
        )
        setupMockApi(remoteNotes)
        
        syncManager.sync()
        
        val localNote = repository.getById(1)
        assertEquals("Local Title", localNote?.title)  // Unchanged
    }
    
    @Test
    fun `sync handles API errors gracefully`() = runTest {
        mockClient = HttpClient(MockEngine) {
            engine {
                addHandler { respond(
                    content = "Server Error",
                    status = HttpStatusCode.InternalServerError
                )}
            }
        }
        apiClient = NotesApiClient(mockClient, "https://api.example.com")
        syncManager = NotesSyncManager(apiClient, repository)
        
        val result = syncManager.sync()
        
        assertTrue(result.isFailure)
        // Database should be unchanged
        assertTrue(repository.getAll().isEmpty())
    }
}