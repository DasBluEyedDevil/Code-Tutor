---
type: "THEORY"
title: "Test-Driven Development (TDD)"
---


### The TDD Cycle


### Example: Building a Password Validator

**Step 1: Write the test (Red)**:

Test fails (class doesn't exist yet) ❌

**Step 2: Minimal implementation (Green)**:

Test passes ✅

**Step 3: Add more tests**:

**Step 4: Implement to pass all tests**:

**Benefits of TDD**:
- Forces you to think about design before implementation
- Ensures code is testable
- Provides immediate feedback
- Creates a safety net for refactoring

---



```kotlin
class PasswordValidator {
    fun isValid(password: String): Boolean {
        if (password.length < 8) return false
        if (!password.any { it.isUpperCase() }) return false
        if (!password.any { it.isDigit() }) return false
        return true
    }

    fun getErrors(password: String): List<String> {
        val errors = mutableListOf<String>()

        if (password.length < 8) {
            errors.add("Password must be at least 8 characters")
        }
        if (!password.any { it.isUpperCase() }) {
            errors.add("Password must contain an uppercase letter")
        }
        if (!password.any { it.isDigit() }) {
            errors.add("Password must contain a number")
        }

        return errors
    }
}
```
