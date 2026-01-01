---
type: "THEORY"
title: "Exception Propagation Rules"
---

### With Regular Job (coroutineScope)
One failure cancels everything:

```kotlin
coroutineScope {
    launch {
        delay(1000)
        println("First") // Never prints - cancelled
    }
    launch {
        throw Exception("Second failed!")
    }
}
// Output: Exception in second cancels first
```

### With SupervisorJob (supervisorScope)
Failures are isolated:

```kotlin
supervisorScope {
    launch {
        delay(100)
        println("First completes") // ✅ Prints
    }
    launch {
        throw Exception("Second failed!")
    }
}
// Output: First completes
// Exception is still thrown, but first wasn't cancelled
```

### Visual Summary
```
Regular Job:                SupervisorJob:
    Parent                      Parent
   /      \                    /      \
Child1   Child2            Child1   Child2
           ↓                          ↓
        Exception               Exception
           ↓                          ↓
    Cancels Parent          Only Child2 fails
           ↓
    Cancels Child1
```