---
type: "EXAMPLE"
title: "Creating Results"
---


Multiple ways to create Result values:



```kotlin
// Using runCatching - wraps exceptions
fun parseNumber(s: String): Result<Int> = runCatching {
    s.toInt()
}

// Success case
val success: Result<Int> = Result.success(42)

// Failure case
val failure: Result<Int> = Result.failure(IllegalArgumentException("Invalid"))

// From nullable with custom error
fun findUser(id: Long): Result<User> = runCatching {
    userRepository.findById(id)
        ?: throw NoSuchElementException("User $id not found")
}

// Combining with require/check
fun divide(a: Int, b: Int): Result<Double> = runCatching {
    require(b != 0) { "Cannot divide by zero" }
    a.toDouble() / b
}
```
