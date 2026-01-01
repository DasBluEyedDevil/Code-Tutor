---
type: "EXAMPLE"
title: "Solution: Validation Pipeline"
---




```kotlin
fun validateRegistration(request: RegistrationRequest): Result<ValidatedUser> =
    validateUsername(request.username)
        .mapCatching { username ->
            validateEmail(request.email)
                .getOrThrow()  // Propagate email errors
            username
        }
        .mapCatching { username ->
            validatePassword(request.password)
                .getOrThrow()
            username
        }
        .mapCatching { username ->
            validateAge(request.age)
                .getOrThrow()
            username
        }
        .map { username ->
            ValidatedUser(
                username = username,
                email = request.email,
                passwordHash = hashPassword(request.password),
                age = request.age
            )
        }

private fun validateUsername(username: String): Result<String> = runCatching {
    require(username.length in 3..20) { 
        "Username must be 3-20 characters" 
    }
    require(username.all { it.isLetterOrDigit() }) { 
        "Username must be alphanumeric" 
    }
    username
}

private fun validateEmail(email: String): Result<String> = runCatching {
    require("@" in email) { "Invalid email format" }
    email
}

private fun validatePassword(password: String): Result<String> = runCatching {
    require(password.length >= 8) { 
        "Password must be at least 8 characters" 
    }
    password
}

private fun validateAge(age: Int): Result<Int> = runCatching {
    require(age >= 18) { "Must be 18 or older" }
    age
}

private fun hashPassword(password: String): String = 
    password.hashCode().toString()  // Simplified
```
