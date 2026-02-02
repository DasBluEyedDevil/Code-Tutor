---
type: "THEORY"
title: "Job and SupervisorJob"
---

### Job
Every coroutine has a `Job` that represents its lifecycle:

```kotlin
val job: Job = scope.launch {
    // coroutine work
}

// Check state
job.isActive    // Is it still running?
job.isCompleted // Has it finished?
job.isCancelled // Was it cancelled?

// Control
job.cancel()    // Request cancellation
job.join()      // Wait for completion
job.cancelAndJoin() // Cancel and wait
```

### SupervisorJob
A `SupervisorJob` doesn't cancel other children when one child fails:

```kotlin
// Regular Job - one failure cancels everything
val scope1 = CoroutineScope(Dispatchers.Default + Job())

// SupervisorJob - failures are isolated
val scope2 = CoroutineScope(Dispatchers.Default + SupervisorJob())

// With SupervisorJob:
scope2.launch { throw Exception("Boom!") }  // This fails
scope2.launch { delay(1000); println("OK") } // This still runs!
```

**Best Practice**: Use `SupervisorJob` for scopes that launch independent work (like ViewModels).