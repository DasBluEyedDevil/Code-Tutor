---
type: "THEORY"
title: "Solution 1"
---



---



```kotlin
// Password validator
object PasswordValidator {
    private val commonPasswords = setOf(
        "password123", "qwerty123", "admin123",
        // ... load from file
    )

    fun validate(password: String): ValidationResult {
        val errors = mutableListOf<String>()

        if (password.length < 12) {
            errors.add("Password must be at least 12 characters")
        }

        if (!password.any { it.isUpperCase() }) {
            errors.add("Must contain uppercase letter")
        }

        if (!password.any { it.isLowerCase() }) {
            errors.add("Must contain lowercase letter")
        }

        if (!password.any { it.isDigit() }) {
            errors.add("Must contain number")
        }

        if (!password.any { "!@#$%^&*()".contains(it) }) {
            errors.add("Must contain special character")
        }

        if (password.lowercase() in commonPasswords) {
            errors.add("Password is too common")
        }

        return ValidationResult(errors.isEmpty(), errors)
    }
}

// Email validator with DNS check
object EmailValidator {
    fun validate(email: String): ValidationResult {
        val errors = mutableListOf<String>()

        if (!basicValidation(email)) {
            errors.add("Invalid email format")
            return ValidationResult(false, errors)
        }

        val domain = email.substringAfter("@")
        if (!hasMXRecord(domain)) {
            errors.add("Email domain does not exist")
        }

        return ValidationResult(errors.isEmpty(), errors)
    }

    private fun basicValidation(email: String): Boolean {
        val pattern = Regex("^[A-Za-z0-9+_.-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,}$")
        return email.matches(pattern)
    }

    private fun hasMXRecord(domain: String): Boolean {
        return try {
            val attributes = InitialDirContext().getAttributes(
                "dns:/$domain",
                arrayOf("MX")
            )
            attributes.get("MX") != null
        } catch (e: Exception) {
            false
        }
    }
}

// Rate limiter
class RateLimiter(private val maxAttempts: Int, private val windowMs: Long) {
    // Note: ConcurrentHashMap is correct here for thread-safe concurrent access
    // mutableMapOf() is NOT thread-safe and would cause race conditions
    private val attempts = ConcurrentHashMap<String, MutableList<Long>>()

    fun isAllowed(key: String): Boolean {
        val now = System.currentTimeMillis()
        val userAttempts = attempts.getOrPut(key) { mutableListOf() }

        // Remove old attempts
        userAttempts.removeIf { it < now - windowMs }

        if (userAttempts.size >= maxAttempts) {
            return false
        }

        userAttempts.add(now)
        return true
    }
}

// Registration service
class RegistrationService(
    private val userRepository: UserRepository,
    private val emailService: EmailService,
    private val rateLimiter: RateLimiter
) {
    suspend fun register(
        email: String,
        password: String,
        ipAddress: String
    ): Result<User> {
        // Rate limiting
        if (!rateLimiter.isAllowed(ipAddress)) {
            return Result.failure(RateLimitException("Too many registration attempts"))
        }

        // Validate email
        val emailValidation = EmailValidator.validate(email)
        if (!emailValidation.isValid) {
            return Result.failure(ValidationException(emailValidation.errors))
        }

        // Check uniqueness
        if (userRepository.existsByEmail(email)) {
            return Result.failure(ValidationException("Email already registered"))
        }

        // Validate password
        val passwordValidation = PasswordValidator.validate(password)
        if (!passwordValidation.isValid) {
            return Result.failure(ValidationException(passwordValidation.errors))
        }

        // Hash password
        val passwordHash = BCrypt.hashpw(password, BCrypt.gensalt(12))

        // Create user (unverified)
        val user = User(
            id = UUID.randomUUID().toString(),
            email = email,
            passwordHash = passwordHash,
            emailVerified = false,
            createdAt = System.currentTimeMillis()
        )

        userRepository.save(user)

        // Send verification email
        val verificationToken = generateVerificationToken(user.id)
        emailService.sendVerificationEmail(email, verificationToken)

        return Result.success(user)
    }

    private fun generateVerificationToken(userId: String): String {
        val token = UUID.randomUUID().toString()
        // Save token with expiration (24 hours)
        return token
    }
}

data class ValidationResult(val isValid: Boolean, val errors: List<String>)
```
