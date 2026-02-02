---
type: "THEORY"
title: "The Two Types of Coroutine Builders"
---

Exceptions behave differently based on the builder:

### launch - Fire and Forget
```kotlin
scope.launch {
    throw Exception("Boom!") // Propagates to scope immediately
}
```
- Exception propagates UP to parent scope
- Cannot be caught with try-catch around launch
- Uses CoroutineExceptionHandler

### async - Returns Deferred
```kotlin
val deferred = scope.async {
    throw Exception("Boom!") // Stored in Deferred
}
deferred.await() // Exception thrown here!
```
- Exception is stored until `.await()` is called
- Can be caught with try-catch around await
- Allows caller to decide how to handle