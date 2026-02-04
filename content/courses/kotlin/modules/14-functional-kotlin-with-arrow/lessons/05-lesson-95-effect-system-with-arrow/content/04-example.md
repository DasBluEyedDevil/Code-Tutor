---
type: "EXAMPLE"
title: "Basic Raise Usage"
---


Using Raise for error handling:



```kotlin
import arrow.core.raise.*
import arrow.core.*

sealed interface UserError {
    data class NotFound(val id: Long) : UserError
    data class InvalidId(val id: Long) : UserError
    data object Unauthorized : UserError
}

data class User(val id: Long, val name: String, val email: String)

// Function that can raise UserError
context(raise: Raise<UserError>)
fun getUser(id: Long): User {
    // ensure - like require() but raises typed error
    raise.ensure(id > 0) { UserError.InvalidId(id) }

    // raise - immediately fail with error
    val user = userRepository.findById(id)
        ?: raise.raise(UserError.NotFound(id))

    return user
}

// Composing functions with Raise
context(raise: Raise<UserError>)
fun getUserEmail(id: Long): String {
    val user = getUser(id)  // No .bind()!
    return user.email
}
```
