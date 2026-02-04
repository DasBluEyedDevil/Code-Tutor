---
type: "EXAMPLE"
title: "Error Accumulation for Form Validation"
---


Collect all validation errors with `zipOrAccumulate`:



```kotlin
import arrow.core.*
import arrow.core.raise.*

data class Registration(val username: String, val email: String, val password: String, val age: Int)

// EitherNel = Either<NonEmptyList<E>, A> -- collects all errors
fun validateRegistration(
    username: String,
    email: String,
    password: String,
    age: Int
): EitherNel<String, Registration> = either {
    zipOrAccumulate(
        { ensure(username.length >= 3) { "Username must be at least 3 characters" } },
        { ensure("@" in email) { "Invalid email format" } },
        { ensure(password.length >= 8) { "Password must be at least 8 characters" } },
        { ensure(age >= 18) { "Must be 18 or older" } }
    ) { _, _, _, _ ->
        Registration(username, email, password, age)
    }
}
```
