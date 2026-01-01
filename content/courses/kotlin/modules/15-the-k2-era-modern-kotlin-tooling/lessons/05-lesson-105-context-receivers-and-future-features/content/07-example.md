---
type: "EXAMPLE"
title: "Arrow's Raise with Context Receivers"
---


Arrow uses context receivers for effect handling:



```kotlin
import arrow.core.raise.*

// Define error types
sealed interface UserError {
    data class NotFound(val id: Long) : UserError
    data class InvalidEmail(val email: String) : UserError
    data object Unauthorized : UserError
}

// Functions using Raise context
context(Raise<UserError>)
suspend fun getUser(id: Long): User {
    ensure(id > 0) { UserError.NotFound(id) }
    
    return userRepository.findById(id)
        ?: raise(UserError.NotFound(id))
}

context(Raise<UserError>)
suspend fun validateEmail(email: String): String {
    ensure(email.contains("@")) { UserError.InvalidEmail(email) }
    ensure(!email.startsWith("test@")) { UserError.InvalidEmail(email) }
    return email
}

context(Raise<UserError>)
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
