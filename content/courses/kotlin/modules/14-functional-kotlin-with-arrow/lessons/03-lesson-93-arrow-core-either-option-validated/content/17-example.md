---
type: "EXAMPLE"
title: "Solution: User Service"
---




```kotlin
import arrow.core.*
import arrow.core.raise.*

sealed interface UserError {
    data class NotFound(val id: Long) : UserError
    data class InvalidData(val errors: NonEmptyList<String>) : UserError
    data class Conflict(val message: String) : UserError
}

data class User(val id: Long, val name: String, val email: String)
data class CreateUserRequest(val name: String, val email: String)

class UserService(private val repository: UserRepository) {

    fun createUser(request: CreateUserRequest): Either<UserError, User> = either {
        // Validate input (accumulate errors)
        val validated = validateCreateRequest(request)
            .mapLeft { UserError.InvalidData(it) }
            .bind()

        // Check email uniqueness
        val exists = repository.existsByEmail(validated.email)
        ensure(!exists) { UserError.Conflict("Email already registered") }

        // Create user
        repository.save(User(0, validated.name, validated.email))
    }

    fun getUser(id: Long): Either<UserError, User> =
        repository.findById(id)
            .toEither { UserError.NotFound(id) }

    fun updateEmail(userId: Long, newEmail: String): Either<UserError, User> = either {
        val user = getUser(userId).bind()
        val validEmail = validateEmail(newEmail)
            .mapLeft { UserError.InvalidData(it) }
            .bind()
        
        val updated = user.copy(email = validEmail)
        repository.save(updated)
    }

    private fun validateCreateRequest(request: CreateUserRequest): EitherNel<String, CreateUserRequest> =
        either {
            zipOrAccumulate(
                { ensure(request.name.isNotBlank()) { "Name cannot be blank" } },
                { ensure("@" in request.email) { "Invalid email format" } }
            ) { _, _ -> request }
        }

    // Individual validations return Either for composability
    private fun validateEmail(email: String): EitherNel<String, String> =
        either {
            ensure(email.contains("@")) { nonEmptyListOf("Invalid email format") }
            email
        }
}
```
