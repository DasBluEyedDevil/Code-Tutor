---
type: "EXAMPLE"
title: "Arrow's Raise with Context Parameters"
---


Arrow uses context parameters for effect handling:



```kotlin
import arrow.core.raise.*

// Define error types
sealed interface UserError {
    data class NotFound(val id: Long) : UserError
    data class InvalidEmail(val email: String) : UserError
    data object Unauthorized : UserError
}

// Functions using Raise context parameter
context(raise: Raise<UserError>)
suspend fun getUser(id: Long): User {
    raise.ensure(id > 0) { UserError.NotFound(id) }

    return userRepository.findById(id)
        ?: raise.raise(UserError.NotFound(id))
}

context(raise: Raise<UserError>)
suspend fun validateEmail(email: String): String {
    raise.ensure(email.contains("@")) { UserError.InvalidEmail(email) }
    raise.ensure(!email.startsWith("test@")) { UserError.InvalidEmail(email) }
    return email
}

context(raise: Raise<UserError>)
suspend fun updateUserEmail(userId: Long, newEmail: String): User {
    val user = getUser(userId)  // Raise context is available
    val validEmail = validateEmail(newEmail)
    return userRepository.save(user.copy(email = validEmail))
}

// Execute with either { } to get Either result
suspend fun main() {
    val result: Either<UserError, User> = either {
        updateUserEmail(123, "new@example.com")
    }

    result.fold(
        ifLeft = { error -> println("Error: $error") },
        ifRight = { user -> println("Updated: $user") }
    )
}
```
