---
type: "THEORY"
title: "Requirements"
---


### 1. Book Management

**Abstract Class: `Book`**
- Properties: `isbn`, `title`, `author`, `publishYear`, `status`
- Abstract method: `getDisplayInfo()`
- Method: `isAvailable()`

**Classes**:
- `PhysicalBook` extends `Book`
  - Additional properties: `shelfLocation`, `condition` (New, Good, Fair, Poor)
  - Implements `getDisplayInfo()`

- `DigitalBook` extends `Book`
  - Additional properties: `fileSize` (MB), `format` (PDF, EPUB, MOBI)
  - Method: `download()`
  - Implements `getDisplayInfo()`

**Book Status** (Sealed Class):
- `Available`
- `Borrowed(memberId, dueDate)`
- `Reserved(memberId)`
- `Maintenance`

### 2. Member Management

**Data Class: `Member`**
- Properties: `memberId`, `name`, `email`, `membershipType`, `joinDate`
- Method: `canBorrow()` - checks if member can borrow more books

**Membership Types** (Enum):
- `BASIC` - Can borrow 3 books
- `PREMIUM` - Can borrow 5 books
- `STUDENT` - Can borrow 4 books with discounted fees

### 3. Loan System

**Data Class: `Loan`**
- Properties: `loanId`, `book`, `member`, `borrowDate`, `dueDate`, `returnDate`, `lateFee`
- Method: `isOverdue()` - checks if loan is past due date
- Method: `calculateLateFee()` - calculates fee based on days overdue

**Interface: `Borrowable`**
- Methods: `borrow(member)`, `returnBook()`

**Interface: `Reservable`**
- Methods: `reserve(member)`, `cancelReservation()`

### 4. Library Manager

**Object: `Library`**
- Manages all books, members, and loans
- Methods:
  - `addBook(book)`
  - `removeBook(isbn)`
  - `registerMember(member)`
  - `borrowBook(isbn, memberId)`
  - `returnBook(isbn)`
  - `reserveBook(isbn, memberId)`
  - `searchBooks(query)` - search by title or author
  - `getOverdueLoans()`
  - `getMemberHistory(memberId)`
  - `printStatistics()`

---

