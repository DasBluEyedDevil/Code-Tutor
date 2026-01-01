---
type: "EXAMPLE"
title: "Defining Error Types"
---


Create domain-specific error hierarchies:



```kotlin
import arrow.core.*

// Define your error types
sealed interface UserError {
    data class NotFound(val id: Long) : UserError
    data class InvalidEmail(val email: String) : UserError
    data class AlreadyExists(val email: String) : UserError
    data object Unauthorized : UserError
}

// Your domain types
data class User(val id: Long, val name: String, val email: String)

// Functions return Either
fun getUser(id: Long): Either<UserError, User> =
    if (id <= 0) {
        UserError.NotFound(id).left()
    } else {
        User(id, "John", "john@example.com").right()
    }

fun validateEmail(email: String): Either<UserError, String> =
    if ("@" in email) {
        email.right()
    } else {
        UserError.InvalidEmail(email).left()
    }
```
