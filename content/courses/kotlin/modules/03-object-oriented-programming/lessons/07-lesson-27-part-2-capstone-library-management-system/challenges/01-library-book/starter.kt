// Define a Book data class with title and author

// Define a Library class with addBook and findByAuthor methods

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
