---
type: "EXAMPLE"
title: "Ensure and EnsureNotNull"
---


Built-in validation helpers:



```kotlin
import arrow.core.raise.*

sealed interface ValidationError {
    data class EmptyField(val field: String) : ValidationError
    data class InvalidFormat(val field: String, val value: String) : ValidationError
    data class OutOfRange(val field: String, val value: Int) : ValidationError
}

data class ValidatedUser(val name: String, val email: String, val age: Int)

context(Raise<ValidationError>)
fun validateUser(name: String?, email: String?, age: Int?): ValidatedUser {
    // ensureNotNull - fail if null
    val validName = ensureNotNull(name) {
        ValidationError.EmptyField("name")
    }
    
    val validEmail = ensureNotNull(email) {
        ValidationError.EmptyField("email")
    }
    
    val validAge = ensureNotNull(age) {
        ValidationError.EmptyField("age")
    }
    
    // ensure - fail if condition is false
    ensure(validName.isNotBlank()) {
        ValidationError.EmptyField("name")
    }
    
    ensure("@" in validEmail) {
        ValidationError.InvalidFormat("email", validEmail)
    }
    
    ensure(validAge in 0..150) {
        ValidationError.OutOfRange("age", validAge)
    }
    
    return ValidatedUser(validName, validEmail, validAge)
}
```
