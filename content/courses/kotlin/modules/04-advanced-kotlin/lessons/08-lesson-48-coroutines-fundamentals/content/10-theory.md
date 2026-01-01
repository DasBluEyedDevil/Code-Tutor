---
type: "THEORY"
title: "Job - Managing Coroutine Lifecycle"
---


A `Job` represents a coroutine and allows you to manage its lifecycle.

### Job Basics


### Job States


### Cancellation is Cooperative

Coroutines must cooperate to be cancellable:


### Making Code Cancellable


---



```kotlin
import kotlinx.coroutines.isActive

fun main() = runBlocking {
    val job = launch {
        var i = 0
        while (isActive) {  // ✅ Check if still active
            println("Job: $i")
            Thread.sleep(500)
            i++
        }
        println("Cleaning up...")
    }

    delay(1200)
    job.cancel()
    job.join()
}
```
