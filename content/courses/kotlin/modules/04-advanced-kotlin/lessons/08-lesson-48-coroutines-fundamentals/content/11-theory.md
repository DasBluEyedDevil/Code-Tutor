---
type: "THEORY"
title: "Deferred - Async Results"
---


`Deferred<T>` is a `Job` that returns a result.

### Basic Usage


### Multiple Async Operations


### Error Handling with Deferred


---



```kotlin
fun main() = runBlocking {
    val deferred = async {
        delay(500)
        throw RuntimeException("Error!")
    }

    try {
        deferred.await()  // Exception thrown here
    } catch (e: Exception) {
        println("Caught: ${e.message}")
    }
}
```
