---
type: "THEORY"
title: "Why Testing Matters More in KMP"
---

In traditional single-platform development, bugs affect one platform. In KMP, bugs in shared code affect **all platforms simultaneously**.

### The Multiplier Effect

| Bug Location | Impact |
|-------------|--------|
| Android-only code | 1 platform |
| iOS-only code | 1 platform |
| **Shared KMP code** | **All platforms** |

A single bug in your repository layer can crash Android, iOS, Desktop, and Web simultaneously. Testing your shared code is **3-4x more valuable** than platform-specific testing.

### Real-World Example

```kotlin
// Shared repository with a subtle bug
class NoteRepository(private val database: AppDatabase) {
    suspend fun deleteNote(noteId: Long) {
        database.noteQueries.deleteNote(noteId)
        // Bug: forgot to handle cascade delete of attachments
        // This bug now exists on Android, iOS, and Desktop
    }
}
```

One test catches the bug for all platforms:
```kotlin
@Test
fun `deleteNote should also delete attachments`() = runTest {
    // This test protects Android, iOS, AND Desktop
    repository.addNote("Test", "Content")
    repository.addAttachment(noteId = 1, "image.png")
    
    repository.deleteNote(1)
    
    val attachments = repository.getAttachments(1)
    assertTrue(attachments.isEmpty(), "Attachments should be deleted")
}
```