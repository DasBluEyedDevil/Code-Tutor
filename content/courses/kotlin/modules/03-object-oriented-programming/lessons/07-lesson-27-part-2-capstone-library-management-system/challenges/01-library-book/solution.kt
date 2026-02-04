data class Book(val title: String, val author: String)

class Library {
    private val books = mutableListOf<Book>()
    val size: Int get() = books.size

    fun addBook(book: Book) {
        books.add(book)
    }

    fun findByAuthor(author: String): List<Book> {
        return books.filter { it.author == author }
    }
}

fun main() {
    val library = Library()
    library.addBook(Book("Kotlin in Action", "Dmitry Jemerov"))
    library.addBook(Book("Effective Java", "Joshua Bloch"))
    
    val results = library.findByAuthor("Dmitry Jemerov")
    for (book in results) {
        println("Found: ${book.title} by ${book.author}")
    }
    println("Library has ${library.size} books")
}
