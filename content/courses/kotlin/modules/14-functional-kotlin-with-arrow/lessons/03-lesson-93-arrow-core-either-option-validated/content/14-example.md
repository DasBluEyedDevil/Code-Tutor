---
type: "EXAMPLE"
title: "Combining Types"
---


Real-world pattern: zipOrAccumulate for input, Either for processing:



```kotlin
import arrow.core.*
import arrow.core.raise.*

sealed interface RegistrationError {
    data class ValidationErrors(val errors: NonEmptyList<String>) : RegistrationError
    data class DuplicateEmail(val email: String) : RegistrationError
    data class DatabaseError(val cause: Throwable) : RegistrationError
}

fun registerUser(
    username: String,
    email: String,
    password: String,
    age: Int
): Either<RegistrationError, User> = either {
    // Step 1: Validate inputs (accumulate all errors)
    val registration = withError({ errors: NonEmptyList<String> ->
        RegistrationError.ValidationErrors(errors)
    }) {
        zipOrAccumulate(
            { ensure(username.length >= 3) { "Username must be at least 3 characters" } },
            { ensure("@" in email) { "Invalid email format" } },
            { ensure(password.length >= 8) { "Password must be at least 8 characters" } },
            { ensure(age >= 18) { "Must be 18 or older" } }
        ) { _, _, _, _ ->
            Registration(username, email, password, age)
        }
    }

    // Step 2: Check business rules (short-circuit on first error)
    val emailExists = checkEmailExists(registration.email).bind()
    ensure(!emailExists) { RegistrationError.DuplicateEmail(registration.email) }

    // Step 3: Save to database
    saveUser(registration)
        .mapLeft { e -> RegistrationError.DatabaseError(e) }
        .bind()
}
```
