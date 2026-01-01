---
type: "THEORY"
title: "Solution 1: Lambda Style Converter"
---



**Explanation**:
- Property references (`Book::title`) are cleanest for simple property access
- Function references (`::isHighlyRated`) work when calling existing functions
- Lambda with `it` is fine for simple operations like `it.pages > 300`

---



```kotlin
data class Book(val title: String, val author: String, val pages: Int, val rating: Double)

fun isHighlyRated(book: Book): Boolean = book.rating >= 4.0

fun main() {
    val books = listOf(
        Book("1984", "George Orwell", 328, 4.5),
        Book("Brave New World", "Aldous Huxley", 268, 4.2),
        Book("The Hobbit", "J.R.R. Tolkien", 310, 4.7)
    )

    // Original: books.map({ book -> book.title })
    // Improved: Property reference
    val titles = books.map(Book::title)
    println("Titles: $titles")
    // [1984, Brave New World, The Hobbit]

    // Original: books.filter({ book -> book.pages > 300 })
    // Improved: Lambda with 'it'
    val longBooks = books.filter { it.pages > 300 }
    println("Long books: ${longBooks.map { it.title }}")
    // [1984, The Hobbit]

    // Original: books.filter({ book -> isHighlyRated(book) })
    // Improved: Function reference
    val highlyRated = books.filter(::isHighlyRated)
    println("Highly rated: ${highlyRated.map { it.title }}")
    // [1984, Brave New World, The Hobbit]

    // Original: books.map({ book -> book.author })
    // Improved: Property reference
    val authors = books.map(Book::author)
    println("Authors: $authors")
    // [George Orwell, Aldous Huxley, J.R.R. Tolkien]
}
```
