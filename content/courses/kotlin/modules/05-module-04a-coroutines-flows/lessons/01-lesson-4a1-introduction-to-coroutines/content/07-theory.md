---
type: "THEORY"
title: "async and await - Getting Results"
---

While `launch` is fire-and-forget, `async` returns a result:

```kotlin
import kotlinx.coroutines.*

fun main() = runBlocking {
    val deferred: Deferred<String> = async {
        delay(1000)
        "Hello from async!"
    }
    
    println("Waiting for result...")
    val result = deferred.await() // Suspends until result is ready
    println(result)
}
```

### Deferred<T>
- `async` returns a `Deferred<T>` - a promise of a future value
- Call `.await()` to get the result (suspends until ready)
- Can be used for parallel decomposition

### Parallel Execution
```kotlin
fun main() = runBlocking {
    val time = measureTimeMillis {
        val one = async { fetchFirstValue() }  // Starts immediately
        val two = async { fetchSecondValue() } // Starts immediately
        
        println("Results: ${one.await()} + ${two.await()}")
    }
    println("Completed in $time ms") // ~1000ms, not ~2000ms
}

suspend fun fetchFirstValue(): Int {
    delay(1000)
    return 10
}

suspend fun fetchSecondValue(): Int {
    delay(1000)
    return 20
}
```

Both `async` blocks start immediately and run concurrently, so total time is ~1 second, not 2.