---
type: "THEORY"
title: "CoroutineExceptionHandler"
---

For `launch`, use CoroutineExceptionHandler:

```kotlin
val handler = CoroutineExceptionHandler { _, exception ->
    println("Caught $exception")
    // Log to crash reporting service
    // Show error UI
}

val scope = CoroutineScope(Dispatchers.Main + handler)

scope.launch {
    throw RuntimeException("Boom!")
}
// Output: Caught java.lang.RuntimeException: Boom!
```

### Important Rules

1. **Only works with launch**, not async
2. **Must be in scope or root coroutine** - not child coroutines
3. **Doesn't prevent cancellation** - siblings still get cancelled

```kotlin
// ❌ Handler on child doesn't work
scope.launch {
    launch(handler) { // Handler ignored here!
        throw Exception("Boom!")
    }
}

// ✅ Handler on scope works
val scope = CoroutineScope(Dispatchers.Main + handler)
scope.launch {
    throw Exception("Boom!") // Handler catches this
}
```