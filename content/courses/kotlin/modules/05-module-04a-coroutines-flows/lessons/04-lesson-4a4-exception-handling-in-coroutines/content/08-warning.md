---
type: "WARNING"
title: "Critical: Never Catch CancellationException"
---

```kotlin
// ❌ DANGEROUS - breaks cancellation
try {
    suspendingFunction()
} catch (e: Exception) { // Catches CancellationException too!
    println("Error")
}

// ✅ CORRECT - rethrow CancellationException
try {
    suspendingFunction()
} catch (e: CancellationException) {
    throw e // Always rethrow!
} catch (e: Exception) {
    println("Error: ${e.message}")
}

// ✅ OR use runCatching carefully
runCatching {
    suspendingFunction()
}.onFailure { e ->
    if (e is CancellationException) throw e
    println("Error: ${e.message}")
}
```

**Why?** CancellationException is how coroutines signal they should stop. Catching it prevents proper cleanup and can cause resource leaks.