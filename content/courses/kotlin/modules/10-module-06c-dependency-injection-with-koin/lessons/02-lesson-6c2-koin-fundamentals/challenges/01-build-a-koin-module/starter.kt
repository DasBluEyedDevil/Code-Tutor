import org.koin.dsl.module
import org.koin.core.module.dsl.viewModel

// Given these classes:
class AppDatabase {
    fun booksQueries(): BooksQueries = TODO()
}

interface BooksQueries {
    fun getAll(): List<Book>
    fun getById(id: String): Book?
    fun insert(book: Book)
}

data class Book(val id: String, val title: String, val author: String)

interface BookRepository {
    suspend fun getAllBooks(): List<Book>
    suspend fun getBook(id: String): Book?
    suspend fun saveBook(book: Book)
}

class BookRepositoryImpl(
    private val queries: BooksQueries
) : BookRepository {
    override suspend fun getAllBooks() = queries.getAll()
    override suspend fun getBook(id: String) = queries.getById(id)
    override suspend fun saveBook(book: Book) = queries.insert(book)
}

class BookListViewModel(
    private val repository: BookRepository
) {
    // ...
}

class BookDetailViewModel(
    private val bookId: String,
    private val repository: BookRepository
) {
    // ...
}

// TODO: Create a Koin module that:
// 1. Provides AppDatabase as a singleton
// 2. Provides BooksQueries from the database
// 3. Provides BookRepositoryImpl bound to BookRepository interface
// 4. Provides BookListViewModel
// 5. Provides BookDetailViewModel with bookId parameter

val bookModule = module {
    // Your code here
}