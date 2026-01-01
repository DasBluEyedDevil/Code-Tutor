---
type: "THEORY"
title: "Assertion Best Practices"
---

### Using kotlin.test Assertions

```kotlin
import kotlin.test.*

// Basic assertions
assertEquals(expected, actual)
assertNotEquals(unexpected, actual)
assertTrue(condition)
assertFalse(condition)
assertNull(value)
assertNotNull(value)

// Type assertions
assertIs<ExpectedType>(value)  // Kotlin 1.5+

// Exception assertions
assertFailsWith<ExpectedException> {
    codeThaThrwos()
}

// Collection assertions
assertContentEquals(listOf(1, 2, 3), actual)
```

### Writing Good Assertions

```kotlin
// ❌ Poor: No message, unclear on failure
assertTrue(notes.size == 3)

// ✅ Better: With message
assertTrue(notes.size == 3, "Expected 3 notes but got ${notes.size}")

// ✅ Best: Use assertEquals for clear failure messages
assertEquals(3, notes.size)
// Failure: Expected <3>, actual <2>

// ❌ Poor: Testing too much at once
assertTrue(
    note.title == "Title" && 
    note.content == "Content" && 
    note.id > 0
)

// ✅ Better: Separate assertions with clear failures
assertEquals("Title", note.title)
assertEquals("Content", note.content)
assertTrue(note.id > 0, "Note ID should be positive")
```