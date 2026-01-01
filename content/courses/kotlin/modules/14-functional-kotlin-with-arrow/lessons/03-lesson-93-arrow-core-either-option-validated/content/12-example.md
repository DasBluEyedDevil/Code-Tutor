---
type: "EXAMPLE"
title: "Using Validated"
---


Handling Validated results:



```kotlin
// Usage - shows all errors at once!
val result = validateRegistration("ab", "invalid", "123", 16)

when (result) {
    is Validated.Valid -> {
        println("Registration successful: ${result.value}")
    }
    is Validated.Invalid -> {
        println("Errors:")
        result.value.forEach { error ->
            println("  - $error")
        }
    }
}

// Output:
// Errors:
//   - Username must be at least 3 characters
//   - Invalid email format
//   - Password must be at least 8 characters
//   - Must be 18 or older

// Convert to Either when done validating
val either: Either<NonEmptyList<String>, Registration> = result.toEither()

// Or use fold
val message: String = result.fold(
    { errors -> "Failed: ${errors.joinToString(", ")}" },
    { registration -> "Welcome, ${registration.username}!" }
)
```
