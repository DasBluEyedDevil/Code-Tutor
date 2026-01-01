---
type: "THEORY"
title: "Exception Handling in Coroutines"
---


Exception handling in coroutines has special rules.

### Try-Catch in Coroutines


### Try-Catch Outside Launch (Doesn't Work!)


### Exception Handling with Async


### CoroutineExceptionHandler

Global exception handler for coroutines:


### SupervisorJob for Independent Failures


---



```kotlin
fun main() = runBlocking {
    val supervisor = SupervisorJob()
    val scope = CoroutineScope(Dispatchers.Default + supervisor)

    val job1 = scope.launch {
        delay(500)
        println("Job 1 completed")
    }

    val job2 = scope.launch {
        delay(300)
        throw RuntimeException("Job 2 failed!")
    }

    val job3 = scope.launch {
        delay(700)
        println("Job 3 completed")
    }

    joinAll(job1, job2, job3)
    supervisor.cancel()
}
// Output:
// Job 1 completed
// Job 3 completed
```
