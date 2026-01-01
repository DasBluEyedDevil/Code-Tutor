---
type: "THEORY"
title: "Exercise 1: Lambda Style Converter"
---


**Goal**: Convert between different lambda styles.

**Task**: Rewrite the following code using:
1. Function references where possible
2. Member references where possible
3. Simplified lambda syntax


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

    // TODO: Rewrite with better lambda styles
    val titles = books.map({ book -> book.title })
    val longBooks = books.filter({ book -> book.pages > 300 })
    val highlyRated = books.filter({ book -> isHighlyRated(book) })
    val authors = books.map({ book -> book.author })
}
```
