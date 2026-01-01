---
type: "EXAMPLE"
title: "Solution: Effect-Based Service"
---




```kotlin
import arrow.core.raise.*
import arrow.core.*

sealed interface UserServiceError {
    data class NotFound(val id: Long) : UserServiceError
    data class ValidationFailed(val message: String) : UserServiceError
    data class EmailConflict(val email: String) : UserServiceError
}

class UserService(private val repository: UserRepository) {

    context(Raise<UserServiceError>)
    suspend fun getUser(id: Long): User {
        ensure(id > 0) { UserServiceError.ValidationFailed("Invalid ID: $id") }
        return repository.findById(id) 
            ?: raise(UserServiceError.NotFound(id))
    }

    context(Raise<UserServiceError>)
    suspend fun createUser(name: String, email: String): User {
        ensure(name.isNotBlank()) { 
            UserServiceError.ValidationFailed("Name is required") 
        }
        ensure("@" in email) { 
            UserServiceError.ValidationFailed("Invalid email") 
        }
        
        val exists = repository.existsByEmail(email)
        ensure(!exists) { UserServiceError.EmailConflict(email) }
        
        return repository.save(User(0, name, email))
    }

    context(Raise<UserServiceError>)
    suspend fun updateEmail(userId: Long, newEmail: String): User {
        val user = getUser(userId)
        ensure("@" in newEmail) { 
            UserServiceError.ValidationFailed("Invalid email") 
        }
        
        if (newEmail != user.email) {
            val exists = repository.existsByEmail(newEmail)
            ensure(!exists) { UserServiceError.EmailConflict(newEmail) }
        }
        
        return repository.save(user.copy(email = newEmail))
    }
}

// Usage at boundary
class UserController(private val userService: UserService) {
    
    suspend fun handleGetUser(id: Long): Response = 
        either<UserServiceError, User> {
            userService.getUser(id)
        }.fold(
            ifLeft = { error ->
                when (error) {
                    is UserServiceError.NotFound -> Response.notFound()
                    is UserServiceError.ValidationFailed -> Response.badRequest(error.message)
                    is UserServiceError.EmailConflict -> Response.conflict(error.email)
                }
            },
            ifRight = { user -> Response.ok(user) }
        )
}
```
