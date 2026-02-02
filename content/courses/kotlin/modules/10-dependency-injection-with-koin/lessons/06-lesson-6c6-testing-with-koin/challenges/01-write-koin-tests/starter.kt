// Given these classes:

data class Book(val id: String, val title: String, val author: String)

interface BookRepository {
    suspend fun getAllBooks(): List<Book>
    suspend fun addBook(title: String, author: String): Book
    suspend fun deleteBook(id: String)
}

class BookViewModel(private val repository: BookRepository) {
    private val _state = MutableStateFlow(BookUiState())
    val state: StateFlow<BookUiState> = _state.asStateFlow()
    
    fun loadBooks() {
        viewModelScope.launch {
            _state.update { it.copy(isLoading = true) }
            try {
                val books = repository.getAllBooks()
                _state.update { it.copy(books = books, isLoading = false) }
            } catch (e: Exception) {
                _state.update { it.copy(error = e.message, isLoading = false) }
            }
        }
    }
}

data class BookUiState(
    val books: List<Book> = emptyList(),
    val isLoading: Boolean = false,
    val error: String? = null
)

// TODO: Create:
// 1. FakeBookRepository
// 2. Test module
// 3. BookViewModelTest with at least 3 tests