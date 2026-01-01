---
type: "THEORY"
title: "Step-by-Step Implementation"
---


### Phase 1: Book Status and Types (30 minutes)

Let's start by defining our book status and types:


### Phase 2: Book Classes (45 minutes)


### Phase 3: Member and Loan (30 minutes)


### Phase 4: Library Manager (60 minutes)


### Phase 5: Main Application (30 minutes)


---



```kotlin
// Main.kt
import java.time.LocalDate

fun main() {
    println("╔════════════════════════════════════════╗")
    println("║     Welcome to LibraryHub System      ║")
    println("╚════════════════════════════════════════╝\n")

    // Initialize library with books
    println("📚 Adding books to library...")
    Library.addBook(PhysicalBook(
        isbn = "978-0-13-468599-1",
        title = "Effective Java",
        author = "Joshua Bloch",
        publishYear = 2018,
        shelfLocation = "A1-15",
        condition = BookCondition.GOOD
    ))

    Library.addBook(PhysicalBook(
        isbn = "978-0-13-597764-5",
        title = "Clean Code",
        author = "Robert C. Martin",
        publishYear = 2008,
        shelfLocation = "A1-20",
        condition = BookCondition.FAIR
    ))

    Library.addBook(DigitalBook(
        isbn = "978-1-61729-655-2",
        title = "Kotlin in Action",
        author = "Dmitry Jemerov",
        publishYear = 2017,
        fileSize = 15.5,
        format = FileFormat.PDF
    ))

    Library.addBook(DigitalBook(
        isbn = "978-1-78899-367-8",
        title = "Programming Kotlin",
        author = "Venkat Subramaniam",
        publishYear = 2019,
        fileSize = 12.3,
        format = FileFormat.EPUB
    ))

    Library.addBook(PhysicalBook(
        isbn = "978-0-13-490733-2",
        title = "Design Patterns",
        author = "Gang of Four",
        publishYear = 1994,
        shelfLocation = "B2-10",
        condition = BookCondition.GOOD
    ))

    // Register members
    println("\n👥 Registering members...")
    Library.registerMember(Member(
        memberId = "M001",
        name = "Alice Johnson",
        email = "alice@example.com",
        membershipType = MembershipType.PREMIUM,
        joinDate = LocalDate.now().minusMonths(6).toString()
    ))

    Library.registerMember(Member(
        memberId = "M002",
        name = "Bob Smith",
        email = "bob@example.com",
        membershipType = MembershipType.BASIC,
        joinDate = LocalDate.now().minusMonths(3).toString()
    ))

    Library.registerMember(Member(
        memberId = "M003",
        name = "Carol Davis",
        email = "carol@example.com",
        membershipType = MembershipType.STUDENT,
        joinDate = LocalDate.now().minusWeeks(2).toString()
    ))

    // Display initial state
    Library.printStatistics()
    Library.displayAllBooks()
    Library.displayAllMembers()

    // Simulate borrowing
    println("\n" + "=".repeat(50))
    println("📖 Borrowing Operations")
    println("=".repeat(50))

    Library.borrowBook("978-0-13-468599-1", "M001")  // Alice borrows Effective Java
    Library.borrowBook("978-1-61729-655-2", "M001")  // Alice borrows Kotlin in Action
    Library.borrowBook("978-0-13-597764-5", "M002")  // Bob borrows Clean Code
    Library.borrowBook("978-1-78899-367-8", "M003")  // Carol borrows Programming Kotlin

    // Try to borrow unavailable book
    println()
    Library.borrowBook("978-0-13-468599-1", "M002")  // Should fail - already borrowed

    // Display updated state
    Library.printStatistics()

    // Search functionality
    println("\n" + "=".repeat(50))
    println("🔍 Search Results for 'Kotlin'")
    println("=".repeat(50))
    val kotlinBooks = Library.searchBooks("Kotlin")
    kotlinBooks.forEach { book ->
        println("\n${book.title} by ${book.author}")
        println("Status: ${book.status.getDescription()}")
    }

    // Return books
    println("\n" + "=".repeat(50))
    println("📥 Return Operations")
    println("=".repeat(50))

    Library.returnBook("978-0-13-468599-1")  // Alice returns Effective Java
    Library.returnBook("978-1-78899-367-8")  // Carol returns Programming Kotlin

    // Display active loans
    println("\n📋 Active Loans:")
    Library.getActiveLoans().forEach { it.display() }

    // Final statistics
    Library.printStatistics()

    // Member history
    println("\n" + "=".repeat(50))
    println("📜 Alice's Borrowing History")
    println("=".repeat(50))
    val aliceHistory = Library.getMemberHistory("M001")
    aliceHistory.forEach { it.display() }

    println("\n╔════════════════════════════════════════╗")
    println("║   Thank you for using LibraryHub!     ║")
    println("╚════════════════════════════════════════╝")
}
```
