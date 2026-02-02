---
type: "THEORY"
title: "Error Handling in Flows"
---

### catch Operator
```kotlin
flow {
    emit(1)
    throw RuntimeException("Error!")
    emit(2) // Never reached
}
.catch { e ->
    println("Caught: ${e.message}")
    emit(-1) // Emit fallback value
}
.collect { println(it) }
// Output: 1, Caught: Error!, -1
```

### onCompletion - Finally Block
```kotlin
flowOf(1, 2, 3)
    .onCompletion { cause ->
        if (cause != null) {
            println("Flow completed with error: $cause")
        } else {
            println("Flow completed successfully")
        }
    }
    .collect { println(it) }
```

### retry
```kotlin
var attempts = 0

flow {
    attempts++
    if (attempts < 3) throw Exception("Retry #$attempts")
    emit("Success!")
}
.retry(3) { e ->
    println("Retrying due to: ${e.message}")
    delay(100)
    true // Return true to retry
}
.collect { println(it) }
```