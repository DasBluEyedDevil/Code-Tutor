---
type: "WARNING"
title: "Common Testing Pitfalls in KMP"
---

### Pitfall 1: Testing on Simulator Only

```kotlin
// ❌ Slow: Running all tests on iOS simulator
./gradlew :shared:iosSimulatorArm64Test  // Takes 5+ minutes

// ✅ Fast: Run on JVM, iOS only for platform-specific
./gradlew :shared:jvmTest  // Takes seconds
```

### Pitfall 2: Using Platform-Specific Mocking Libraries

```kotlin
// ❌ Mockito doesn't work in commonTest
val mockRepo = mock<NoteRepository>()  // Compiler error!

// ✅ Use hand-written fakes or KMP-compatible libraries
class FakeNoteRepository : NoteRepository {
    private val notes = mutableListOf<Note>()
    override suspend fun getAll() = notes.toList()
}
```

### Pitfall 3: Not Testing Coroutines Properly

```kotlin
// ❌ Tests pass but race conditions exist
@Test
fun badTest() {
    val viewModel = NotesViewModel(repo)
    viewModel.loadNotes()
    assertEquals(expected, viewModel.state.value)  // Flaky!
}

// ✅ Use runTest and advanceUntilIdle
@Test
fun goodTest() = runTest {
    val viewModel = NotesViewModel(repo, testScheduler)
    viewModel.loadNotes()
    advanceUntilIdle()  // Wait for coroutines
    assertEquals(expected, viewModel.state.value)
}
```

### Pitfall 4: Testing Implementation Instead of Behavior

```kotlin
// ❌ Testing implementation details
@Test
fun `addNote calls database insert`() {
    repo.addNote("Title", "Content")
    verify(database).insert(any())  // Fragile test
}

// ✅ Testing behavior
@Test
fun `addNote makes note retrievable`() {
    repo.addNote("Title", "Content")
    val notes = repo.getAll()
    assertEquals(1, notes.size)
    assertEquals("Title", notes[0].title)
}
```