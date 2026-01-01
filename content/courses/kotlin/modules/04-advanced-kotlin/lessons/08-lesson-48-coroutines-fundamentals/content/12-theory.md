---
type: "THEORY"
title: "Common Patterns"
---


### Pattern 1: Parallel Decomposition

Execute multiple independent tasks concurrently:


### Pattern 2: Sequential with Suspending


### Pattern 3: Timeout


### Pattern 4: Lazy Async


---



```kotlin
fun main() = runBlocking {
    val deferred = async(start = CoroutineStart.LAZY) {
        println("Computing...")
        delay(1000)
        42
    }

    println("Created async")
    delay(2000)
    println("Starting computation")
    val result = deferred.await()  // Starts computation here
    println("Result: $result")
}
```
