---
type: "THEORY"
title: "Exercise: Build a Validation Pipeline"
---


**Goal**: Create a user registration validator using Result.

**Requirements**:
```kotlin
data class RegistrationRequest(
    val username: String,
    val email: String,
    val password: String,
    val age: Int
)

data class ValidatedUser(
    val username: String,
    val email: String,
    val passwordHash: String,
    val age: Int
)

fun validateRegistration(request: RegistrationRequest): Result<ValidatedUser>
```

**Validation Rules**:
- Username: 3-20 characters, alphanumeric
- Email: Must contain @
- Password: At least 8 characters
- Age: 18 or older

---

