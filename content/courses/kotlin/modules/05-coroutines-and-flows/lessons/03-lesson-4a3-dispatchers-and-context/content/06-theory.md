---
type: "THEORY"
title: "CoroutineContext Explained"
---

A `CoroutineContext` is a set of elements that define coroutine behavior:

```kotlin
// Context is a collection of elements
val context: CoroutineContext = 
    Dispatchers.Default +           // Dispatcher
    SupervisorJob() +                // Job
    CoroutineName("MyCoroutine") +   // Name (for debugging)
    CoroutineExceptionHandler { _, e -> log(e) } // Error handler

launch(context) {
    println(coroutineContext[CoroutineName]) // MyCoroutine
}
```

### Context Elements

| Element | Purpose |
|---------|--------|
| `Job` | Lifecycle management |
| `CoroutineDispatcher` | Thread selection |
| `CoroutineName` | Debugging identifier |
| `CoroutineExceptionHandler` | Uncaught exception handling |

### Context Inheritance
```kotlin
val scope = CoroutineScope(Dispatchers.Main + CoroutineName("Parent"))

scope.launch { // Inherits Main + "Parent"
    launch(Dispatchers.IO) { // Overrides dispatcher, keeps name
        println(coroutineContext[CoroutineName]) // Still "Parent"
    }
    
    launch(CoroutineName("Child")) { // Overrides name, keeps Main
        println(coroutineContext[CoroutineName]) // "Child"
    }
}
```