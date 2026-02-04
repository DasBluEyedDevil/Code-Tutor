---
type: "EXAMPLE"
title: "Handling Accumulated Errors"
---


Handling EitherNel results:



```kotlin
// Usage - shows all errors at once!
val result: EitherNel<String, Registration> =
    validateRegistration("ab", "invalid", "123", 16)

result.fold(
    ifLeft = { errors ->
        println("Errors:")
        errors.forEach { error ->
            println("  - $error")
        }
    },
    ifRight = { registration ->
        println("Registration successful: $registration")
    }
)

// Output:
// Errors:
//   - Username must be at least 3 characters
//   - Invalid email format
//   - Password must be at least 8 characters
//   - Must be 18 or older

// EitherNel IS an Either, so all Either operations work
val message: String = result.fold(
    { errors -> "Failed: ${errors.joinToString(", ")}" },
    { registration -> "Welcome, ${registration.username}!" }
)
```
