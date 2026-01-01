---
type: "WARNING"
title: "Test Naming and Organization"
---

### Naming Convention

Use backticks for readable test names:

```kotlin
// ❌ Poor: Unclear what's being tested
@Test
fun test1() { }

@Test
fun testAddNote() { }

// ✅ Good: Describes behavior being tested
@Test
fun `addNote with valid data returns success`() { }

@Test
fun `addNote with empty title returns validation error`() { }

@Test
fun `deleteNote removes note from database`() { }
```

### Arrange-Act-Assert Pattern

```kotlin
@Test
fun `updating note changes its content`() = runTest {
    // Arrange: Set up test conditions
    repository.addTestNote("Original Title", "Original Content")
    val note = repository.getAll().first()
    
    // Act: Perform the action being tested
    val updatedNote = note.copy(content = "Updated Content")
    repository.update(updatedNote)
    
    // Assert: Verify the expected outcome
    val retrieved = repository.getById(note.id)
    assertNotNull(retrieved)
    assertEquals("Updated Content", retrieved.content)
}
```