---
type: "THEORY"
title: "Dependency Injection Explained"
---

**Dependency Injection** = "Don't create your dependencies, receive them"

### The Fix: Constructor Injection

```kotlin
class NotesViewModel(
    private val notesRepository: NotesRepository,  // ✅ Injected
    private val userPreferences: UserPreferences    // ✅ Injected
) {
    fun loadNotes() {
        val notes = notesRepository.getAll()
        // ...
    }
}
```

**Benefits:**

1. **Testable**: Pass fake implementations for testing
2. **Flexible**: Change implementations without touching this class
3. **Explicit**: Constructor shows exactly what's needed
4. **Decoupled**: Depends on interfaces, not concrete classes

### Testing Becomes Easy

```kotlin
class NotesViewModelTest {
    @Test
    fun `loadNotes shows notes from repository`() {
        // Create fake dependencies
        val fakeRepo = FakeNotesRepository()
        fakeRepo.addNote(Note("Test", "Content"))
        
        val viewModel = NotesViewModel(
            notesRepository = fakeRepo,
            userPreferences = FakeUserPreferences()
        )
        
        viewModel.loadNotes()
        
        assertEquals(1, viewModel.state.value.notes.size)
    }
}
```