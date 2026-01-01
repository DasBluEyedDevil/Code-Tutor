---
type: "WARNING"
title: "Common Flow Testing Mistakes"
---

### Mistake 1: Forgetting to await all emissions

```kotlin
// ❌ Test may hang or fail unexpectedly
flow.test {
    assertEquals(1, awaitItem())
    // Forgot to await remaining items or complete!
}

// ✅ Always clean up
flow.test {
    assertEquals(1, awaitItem())
    cancelAndIgnoreRemainingEvents()
    // Or: awaitComplete() if flow should complete
}
```

### Mistake 2: Not using testScheduler with StandardTestDispatcher

```kotlin
// ❌ Wrong: Creates separate scheduler
@Test
fun test() = runTest {
    val dispatcher = StandardTestDispatcher()  // Different scheduler!
    // advanceUntilIdle() won't work correctly
}

// ✅ Correct: Use the test's scheduler
@Test
fun test() = runTest {
    val dispatcher = StandardTestDispatcher(testScheduler)
    advanceUntilIdle()  // Now this works
}
```

### Mistake 3: Testing StateFlow without skipping initial state

```kotlin
// StateFlow always has a current value
val stateFlow = MutableStateFlow(0)

stateFlow.test {
    // ❌ This is the initial value, not an emission
    assertEquals(0, awaitItem())
    
    stateFlow.value = 1
    assertEquals(1, awaitItem())  // Actual emission
}
```