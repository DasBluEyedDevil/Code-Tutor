---
type: "THEORY"
title: "Exercises"
---


### Exercise 1: Concurrent API Calls (Medium)

Simulate fetching data from multiple APIs concurrently.

**Requirements**:
- Create 3 suspend functions that simulate API calls (1-2 second delays)
- Fetch all data concurrently
- Print total time taken
- Handle potential errors

**Solution**:


### Exercise 2: Progress Reporter (Medium)

Create a progress reporter that runs while a long task executes.

**Requirements**:
- Long-running task (5 seconds)
- Progress reporter updates every 500ms
- Stop progress when task completes
- Show final result

**Solution**:


### Exercise 3: Retry Logic (Hard)

Implement retry logic for a failing operation.

**Requirements**:
- Suspend function that may fail
- Retry up to 3 times with exponential backoff
- Return result on success or throw after max retries
- Log each attempt

**Solution**:


---



```kotlin
import kotlinx.coroutines.*
import kotlin.random.Random

class RetryException(message: String) : Exception(message)

suspend fun unreliableOperation(): String {
    delay(500)

    // 70% chance of failure
    if (Random.nextInt(100) < 70) {
        throw RetryException("Operation failed")
    }

    return "Success!"
}

suspend fun <T> retryWithBackoff(
    maxRetries: Int = 3,
    initialDelay: Long = 100,
    maxDelay: Long = 2000,
    factor: Double = 2.0,
    operation: suspend () -> T
): T {
    var currentDelay = initialDelay

    repeat(maxRetries) { attempt ->
        try {
            println("Attempt ${attempt + 1}...")
            return operation()
        } catch (e: Exception) {
            println("Failed: ${e.message}")

            if (attempt == maxRetries - 1) {
                throw e
            }

            println("Retrying in ${currentDelay}ms...")
            delay(currentDelay)

            currentDelay = (currentDelay * factor).toLong().coerceAtMost(maxDelay)
        }
    }

    throw RetryException("Max retries exceeded")
}

fun main() = runBlocking {
    try {
        val result = retryWithBackoff {
            unreliableOperation()
        }
        println("\n$result")
    } catch (e: Exception) {
        println("\nGave up after max retries: ${e.message}")
    }
}

// Possible output:
// Attempt 1...
// Failed: Operation failed
// Retrying in 100ms...
// Attempt 2...
// Failed: Operation failed
// Retrying in 200ms...
// Attempt 3...
// Success!
```
