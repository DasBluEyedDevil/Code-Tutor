---
type: "EXAMPLE"
title: "Validated for Form Validation"
---


Collect all validation errors:



```kotlin
import arrow.core.*

// ValidatedNel = Validated with NonEmptyList of errors
fun validateUsername(name: String): ValidatedNel<String, String> =
    if (name.length >= 3) name.validNel()
    else "Username must be at least 3 characters".invalidNel()

fun validateEmail(email: String): ValidatedNel<String, String> =
    if ("@" in email) email.validNel()
    else "Invalid email format".invalidNel()

fun validatePassword(pass: String): ValidatedNel<String, String> =
    if (pass.length >= 8) pass.validNel()
    else "Password must be at least 8 characters".invalidNel()

fun validateAge(age: Int): ValidatedNel<String, Int> =
    if (age >= 18) age.validNel()
    else "Must be 18 or older".invalidNel()

data class Registration(val username: String, val email: String, val password: String, val age: Int)

// Combine all validations - collects ALL errors
fun validateRegistration(
    username: String,
    email: String,
    password: String,
    age: Int
): ValidatedNel<String, Registration> =
    validateUsername(username)
        .zip(
            validateEmail(email),
            validatePassword(password),
            validateAge(age)
        ) { u, e, p, a ->
            Registration(u, e, p, a)
        }
```
