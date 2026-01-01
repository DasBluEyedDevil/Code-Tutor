---
type: "EXAMPLE"
title: "Solution: User Service"
---




```kotlin
import arrow.core.*
import arrow.core.raise.either

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
            .toEither()
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
            .toEither()
            .mapLeft { UserError.InvalidData(it) }
            .bind()
        
        val updated = user.copy(email = validEmail)
        repository.save(updated)
    }

    private fun validateCreateRequest(request: CreateUserRequest): ValidatedNel<String, CreateUserRequest> =
        validateName(request.name)
            .zip(validateEmail(request.email)) { n, e -> CreateUserRequest(n, e) }

    private fun validateName(name: String): ValidatedNel<String, String> =
        if (name.isNotBlank()) name.validNel()
        else "Name cannot be blank".invalidNel()

    private fun validateEmail(email: String): ValidatedNel<String, String> =
        if ("@" in email) email.validNel()
        else "Invalid email format".invalidNel()
}
```
