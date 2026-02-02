---
type: "THEORY"
title: "Try-Catch in Coroutines"
---

### Inside a Coroutine - Works as Expected
```kotlin
launch {
    try {
        riskyOperation()
    } catch (e: Exception) {
        println("Caught: ${e.message}")
    }
}
```

### Around launch - Does NOT Work
```kotlin
// ❌ This won't catch the exception!
try {
    launch {
        throw Exception("Boom!")
    }
} catch (e: Exception) {
    println("Never printed")
}
```

Why? `launch` returns immediately after starting the coroutine. The exception happens later, asynchronously.

### Around await - DOES Work
```kotlin
val deferred = async {
    throw Exception("Boom!")
}

try {
    deferred.await()
} catch (e: Exception) {
    println("Caught: ${e.message}") // ✅ This works!
}
```