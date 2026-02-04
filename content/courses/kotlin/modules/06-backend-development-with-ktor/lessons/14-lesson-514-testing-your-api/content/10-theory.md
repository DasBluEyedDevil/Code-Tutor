---
type: "THEORY"
title: "Best Practices"
---


### 1. Test Naming Convention


Use backticks for descriptive test names that read like sentences.

### 2. AAA Pattern


### 3. Test Isolation


Each test should be independent and not affect others.

### 4. Test Data Builders


---



```kotlin
object TestDataBuilder {
    fun createUser(
        id: Int = 1,
        email: String = "test@example.com",
        fullName: String = "Test User",
        role: String = "USER"
    ) = User(
        id = id,
        email = email,
        fullName = fullName,
        role = role,
        createdAt = "2025-01-01T00:00:00"
    )

    fun createRegisterRequest(
        email: String = "test@example.com",
        password: String = "SecurePass123!",
        fullName: String = "Test User"
    ) = RegisterRequest(email, password, fullName)
}
```
