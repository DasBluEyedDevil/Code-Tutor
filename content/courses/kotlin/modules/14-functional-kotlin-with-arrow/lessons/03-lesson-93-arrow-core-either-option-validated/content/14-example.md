---
type: "EXAMPLE"
title: "Combining Types"
---


Real-world pattern: Validated for input, Either for processing:



```kotlin
import arrow.core.*
import arrow.core.raise.either

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
    val validated = validateRegistration(username, email, password, age)
        .toEither()
        .mapLeft { errors -> RegistrationError.ValidationErrors(errors) }
        .bind()
    
    // Step 2: Check business rules (short-circuit on first error)
    val emailExists = checkEmailExists(validated.email).bind()
    ensure(!emailExists) { RegistrationError.DuplicateEmail(validated.email) }
    
    // Step 3: Save to database
    val user = saveUser(validated)
        .mapLeft { e -> RegistrationError.DatabaseError(e) }
        .bind()
    
    user
}
```
