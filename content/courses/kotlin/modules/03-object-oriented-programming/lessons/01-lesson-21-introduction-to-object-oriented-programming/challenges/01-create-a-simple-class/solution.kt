class Book(val title: String, val author: String, val pages: Int)

fun main() {
    val myBook = Book("1984", "George Orwell", 328)
    println("Title: ${myBook.title}")
    println("Author: ${myBook.author}")
    println("Pages: ${myBook.pages}")
}