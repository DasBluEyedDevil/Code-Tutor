---
type: "EXAMPLE"
title: "Real-World: User Registration"
---


Complete railway for user registration:



```kotlin
sealed interface RegistrationError {
    data class InvalidInput(val errors: NonEmptyList<String>) : RegistrationError
    data class EmailTaken(val email: String) : RegistrationError
    data class WeakPassword(val reason: String) : RegistrationError
    data class DatabaseError(val cause: Throwable) : RegistrationError
}

data class RegistrationRequest(
    val email: String,
    val password: String,
    val name: String
)

fun registerUser(request: RegistrationRequest): Either<RegistrationError, User> = either {
    // Track 1: Validate input (accumulate errors)
    val validated = validateInput(request)
        .mapLeft { RegistrationError.InvalidInput(it) }
        .bind()
    
    // Track 2: Check email availability
    val emailAvailable = checkEmailAvailable(validated.email)
        .mapLeft { RegistrationError.EmailTaken(validated.email) }
        .bind()
    
    // Track 3: Check password strength
    val strongPassword = checkPasswordStrength(validated.password)
        .mapLeft { RegistrationError.WeakPassword(it) }
        .bind()
    
    // Track 4: Create user
    val user = createUser(validated.name, validated.email, strongPassword)
        .mapLeft { RegistrationError.DatabaseError(it) }
        .bind()
    
    // Track 5: Send welcome email (don't fail on this)
    sendWelcomeEmail(user).getOrElse {
        logWarning("Failed to send welcome email: $it")
    }
    
    user
}

fun validateInput(request: RegistrationRequest): Either<NonEmptyList<String>, RegistrationRequest> =
    validateEmail(request.email)
        .zip(validateName(request.name)) { e, n -> request }
        .toEither()

fun checkEmailAvailable(email: String): Either<Unit, Unit> =
    if (!userRepository.existsByEmail(email)) Unit.right()
    else Unit.left()

fun checkPasswordStrength(password: String): Either<String, String> =
    when {
        password.length < 8 -> "Password too short".left()
        !password.any { it.isDigit() } -> "Password needs a digit".left()
        else -> password.right()
    }
```
