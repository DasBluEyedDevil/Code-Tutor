---
type: "THEORY"
title: "Testing Flows with Turbine"
---

Turbine is a small library that makes Flow testing easy:

```kotlin
import app.cash.turbine.test

@Test
fun `flow emits values in order`() = runTest {
    val flow = flowOf(1, 2, 3)
    
    flow.test {
        assertEquals(1, awaitItem())
        assertEquals(2, awaitItem())
        assertEquals(3, awaitItem())
        awaitComplete()
    }
}
```

### Turbine Methods

| Method | Description |
|--------|-------------|
| `awaitItem()` | Wait for and return next emission |
| `awaitComplete()` | Assert flow completed |
| `awaitError()` | Assert flow threw exception |
| `cancelAndIgnoreRemainingEvents()` | Cancel and don't check remaining |
| `skipItems(n)` | Skip n emissions |
| `expectNoEvents()` | Assert no emissions |

### Testing StateFlow

```kotlin
@Test
fun `state updates on user action`() = runTest {
    val viewModel = NotesViewModel(fakeRepo, testDispatcher)
    
    viewModel.state.test {
        // Initial state
        val initial = awaitItem()
        assertTrue(initial.isLoading)
        
        advanceUntilIdle()
        
        // After loading
        val loaded = awaitItem()
        assertFalse(loaded.isLoading)
        assertEquals(2, loaded.notes.size)
        
        cancelAndIgnoreRemainingEvents()
    }
}
```