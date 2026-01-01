---
type: "THEORY"
title: "Password Security"
---


### Hashing with bcrypt

❌ **NEVER** (Plaintext):

❌ **BAD** (Simple hash):

✅ **GOOD** (bcrypt):

### Password Strength Requirements


---



```kotlin
object PasswordValidator {
    data class ValidationResult(
        val isValid: Boolean,
        val errors: List<String>
    )

    fun validate(password: String): ValidationResult {
        val errors = mutableListOf<String>()

        if (password.length < 8) {
            errors.add("Password must be at least 8 characters")
        }

        if (!password.any { it.isUpperCase() }) {
            errors.add("Password must contain an uppercase letter")
        }

        if (!password.any { it.isLowerCase() }) {
            errors.add("Password must contain a lowercase letter")
        }

        if (!password.any { it.isDigit() }) {
            errors.add("Password must contain a number")
        }

        if (!password.any { "!@#$%^&*()_+-=[]{}|;:,.<>?".contains(it) }) {
            errors.add("Password must contain a special character")
        }

        // Check against common passwords
        if (isCommonPassword(password)) {
            errors.add("Password is too common")
        }

        return ValidationResult(errors.isEmpty(), errors)
    }

    private fun isCommonPassword(password: String): Boolean {
        val common = setOf(
            "password", "12345678", "qwerty", "abc123",
            "password123", "admin", "letmein"
        )
        return password.lowercase() in common
    }
}
```
