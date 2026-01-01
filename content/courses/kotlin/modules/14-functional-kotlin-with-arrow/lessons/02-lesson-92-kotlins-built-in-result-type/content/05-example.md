---
type: "EXAMPLE"
title: "Extracting Values"
---


Getting values from Result:



```kotlin
val result: Result<Int> = parseNumber("42")

// Get value or throw
val value1: Int = result.getOrThrow()  // Throws if failure

// Get value or null
val value2: Int? = result.getOrNull()  // null if failure

// Get value or default
val value3: Int = result.getOrDefault(0)

// Get value or compute
val value4: Int = result.getOrElse { error ->
    println("Failed: ${error.message}")
    -1  // Fallback value
}

// Get exception or null
val exception: Throwable? = result.exceptionOrNull()

// Check status
val isSuccess: Boolean = result.isSuccess
val isFailure: Boolean = result.isFailure
```
