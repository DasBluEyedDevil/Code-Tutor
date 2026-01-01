---
type: "THEORY"
title: "kotlinx-coroutines-test Essentials"
---

### Key Components

```kotlin
import kotlinx.coroutines.test.*

// 1. runTest - Creates a test coroutine scope
@Test
fun myTest() = runTest {
    // All suspend functions run here
}

// 2. TestDispatcher - Controls virtual time
val testDispatcher = StandardTestDispatcher()

// 3. advanceUntilIdle - Process all pending work
advanceUntilIdle()

// 4. advanceTimeBy - Advance virtual time
advanceTimeBy(1000)  // Advance 1 second
```

### Test Dispatcher Types

| Dispatcher | Behavior | Use When |
|-----------|----------|----------|
| `StandardTestDispatcher` | Pauses until you advance | Testing timing/ordering |
| `UnconfinedTestDispatcher` | Runs immediately | Simple tests, no timing concerns |

```kotlin
// StandardTestDispatcher - More control
@Test
fun `test with controlled execution`() = runTest {
    val dispatcher = StandardTestDispatcher(testScheduler)
    
    launch(dispatcher) {
        delay(1000)
        println("After delay")
    }
    
    println("Before advance")
    advanceTimeBy(500)
    println("After 500ms")  // "After delay" not printed yet
    advanceUntilIdle()       // Now it prints
}

// UnconfinedTestDispatcher - Simpler
@Test
fun `test with immediate execution`() = runTest(UnconfinedTestDispatcher()) {
    launch {
        delay(1000)  // Executes immediately in test
        println("This prints right away")
    }
}
```