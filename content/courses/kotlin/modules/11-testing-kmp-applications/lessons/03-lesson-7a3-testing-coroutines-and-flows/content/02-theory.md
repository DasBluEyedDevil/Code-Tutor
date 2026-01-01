---
type: "THEORY"
title: "The Problem with Testing Async Code"
---

### Naive Approach (Don't Do This)

```kotlin
// ❌ Flaky test - race condition
@Test
fun `loads notes on init`() {
    val viewModel = NotesViewModel(fakeRepo)
    
    // Problem: loadNotes() runs in a coroutine
    // This assertion runs before the coroutine completes!
    assertEquals(3, viewModel.state.value.notes.size)
}
```

### Bad Fix (Don't Do This Either)

```kotlin
// ❌ Still flaky, slow, unreliable
@Test
fun `loads notes on init`() {
    val viewModel = NotesViewModel(fakeRepo)
    
    Thread.sleep(1000)  // Hope it's enough...
    
    assertEquals(3, viewModel.state.value.notes.size)
}
```

### Correct Approach

```kotlin
// ✅ Deterministic, fast, reliable
@Test
fun `loads notes on init`() = runTest {
    val viewModel = NotesViewModel(
        fakeRepo,
        dispatcher = StandardTestDispatcher(testScheduler)
    )
    
    advanceUntilIdle()  // Process all pending coroutines
    
    assertEquals(3, viewModel.state.value.notes.size)
}
```