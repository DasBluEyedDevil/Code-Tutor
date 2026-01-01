// Production code
class SearchNotesUseCase(private val repository: NoteRepository) {
    suspend fun execute(query: String): List<Note> {
        if (query.isBlank()) return repository.getAll()
        
        return repository.getAll().filter { note ->
            note.title.contains(query, ignoreCase = true) ||
            note.content.contains(query, ignoreCase = true)
        }
    }
}

// TODO: Write tests for:
// 1. Empty query returns all notes
// 2. Query matching title returns matching notes
// 3. Query matching content returns matching notes  
// 4. Query is case-insensitive
// 5. No matches returns empty list

class SearchNotesUseCaseTest {
    private lateinit var repository: FakeNoteRepository
    private lateinit var useCase: SearchNotesUseCase
    
    @BeforeTest
    fun setup() {
        repository = FakeNoteRepository()
        useCase = SearchNotesUseCase(repository)
    }
    
    // Add your tests here
}