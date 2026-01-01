---
type: "THEORY"
title: "Understanding CoroutineScope"
---

Every coroutine runs in a **scope**. The scope defines:
- **Lifetime**: When the scope is cancelled, all its coroutines are cancelled
- **Context**: Default dispatcher, exception handler, job, etc.

```kotlin
// Creating your own scope
val myScope = CoroutineScope(Dispatchers.Default)

myScope.launch {
    // This coroutine is tied to myScope's lifetime
    delay(1000)
    println("Done!")
}

// When you're done with the scope:
myScope.cancel() // Cancels all coroutines in this scope
```

### The CoroutineScope Interface
```kotlin
public interface CoroutineScope {
    public val coroutineContext: CoroutineContext
}
```

A scope is essentially a holder for a CoroutineContext. The context contains:
- `Job` - Controls the lifecycle
- `Dispatcher` - Which thread(s) to use
- `CoroutineExceptionHandler` - How to handle uncaught exceptions
- Other context elements