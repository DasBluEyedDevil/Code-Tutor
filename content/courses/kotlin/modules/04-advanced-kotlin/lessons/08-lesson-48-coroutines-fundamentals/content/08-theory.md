---
type: "THEORY"
title: "Coroutine Scope"
---


Every coroutine runs inside a scope. Scopes define lifecycle and context.

### What is a Scope?


### Creating Custom Scopes


### Structured Concurrency

Child coroutines are automatically cancelled when parent scope is cancelled:


---



```kotlin
fun main() = runBlocking {
    val parentJob = launch {
        launch {
            repeat(10) {
                delay(500)
                println("Child 1: $it")
            }
        }

        launch {
            repeat(10) {
                delay(500)
                println("Child 2: $it")
            }
        }
    }

    delay(1500)
    println("Cancelling parent")
    parentJob.cancel()  // Cancels all children too
    delay(1000)
}
// Output:
// Child 1: 0
// Child 2: 0
// Child 1: 1
// Child 2: 1
// Cancelling parent
```
