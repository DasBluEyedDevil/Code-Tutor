---
type: "THEORY"
title: "When to Use Each Approach"
---

### Use Fakes When:

✅ Testing data flow through your app
✅ You need to reuse the same test double in many tests
✅ You're testing business logic that depends on state
✅ You want maximum portability across platforms

```kotlin
// Fake is great for stateful tests
val fakeRepo = FakeNoteRepository()
fakeRepo.addTestNote("Title", "Content")

val viewModel = NotesViewModel(fakeRepo)
viewModel.deleteNote(1)

assertTrue(fakeRepo.getAll().isEmpty())  // State changed
```

### Use Mocks When:

✅ Verifying a specific method was called
✅ Testing that parameters are passed correctly
✅ Complex stubbing scenarios
✅ One-off test scenarios

```kotlin
// Mock is great for verification
val mockAnalytics = mock<Analytics>()

val viewModel = NotesViewModel(fakeRepo, mockAnalytics)
viewModel.addNote("Title", "Content")

verify { mockAnalytics.trackEvent("note_added", any()) }
```

### Hybrid Approach (Recommended)

Use fakes for data repositories, mocks for side-effect services:

```kotlin
class ViewModelTest {
    // Fake for data
    private val fakeNoteRepo = FakeNoteRepository()
    
    // Mock for side effects (if needed)
    private val mockAnalytics = mock<Analytics>()
    
    @Test
    fun `addNote tracks analytics event`() = runTest {
        val viewModel = NotesViewModel(fakeNoteRepo, mockAnalytics)
        
        viewModel.addNote("Title", "Content")
        advanceUntilIdle()
        
        // Verify data was saved (fake)
        assertEquals(1, fakeNoteRepo.getAll().size)
        
        // Verify tracking happened (mock)
        verifySuspend { mockAnalytics.trackEvent("note_created", any()) }
    }
}
```