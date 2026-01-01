---
type: "THEORY"
title: "Extension Functions as Functional Tools"
---


Extension functions enable functional-style APIs.

### Pipeline Operations


### Collection Extensions


### Higher-Order Extension Functions


---



```kotlin
// Retry logic as extension
fun <T> (() -> T).retry(times: Int): T? {
    repeat(times) { attempt ->
        try {
            return this()
        } catch (e: Exception) {
            if (attempt == times - 1) throw e
            println("Attempt ${attempt + 1} failed, retrying...")
        }
    }
    return null
}

// Measure execution time
fun <T> (() -> T).measureTimeMillis(): Pair<T, Long> {
    val start = System.currentTimeMillis()
    val result = this()
    val elapsed = System.currentTimeMillis() - start
    return result to elapsed
}

// Usage
val (result, time) = {
    Thread.sleep(100)
    "Done"
}.measureTimeMillis()

println("Result: $result, Time: ${time}ms")
```
