---
type: "THEORY"
title: "Structured Concurrency"
---


Structured concurrency ensures coroutines have a clear lifecycle and don't leak.

### The Principle

Coroutines should:
1. Have a clear parent-child relationship
2. Be automatically cancelled when parent is cancelled
3. Complete or fail together as a unit


### `coroutineScope` - Structured Concurrency Builder

`coroutineScope` creates a scope that completes only when all children complete:


If any child fails, all siblings are cancelled:


### `supervisorScope` - Independent Children

`supervisorScope` allows children to fail independently:


---



```kotlin
suspend fun fetchWithSupervision() = supervisorScope {
    launch {
        delay(500)
        println("Task 1 completed")
    }

    launch {
        delay(300)
        throw RuntimeException("Task 2 failed!")
    }

    launch {
        delay(700)
        println("Task 3 completed")  // Still executes
    }
}

fun main() = runBlocking {
    try {
        fetchWithSupervision()
        delay(1000)
    } catch (e: Exception) {
        println("Caught: ${e.message}")
    }
}
// Output:
// Task 1 completed
// Task 3 completed
```
