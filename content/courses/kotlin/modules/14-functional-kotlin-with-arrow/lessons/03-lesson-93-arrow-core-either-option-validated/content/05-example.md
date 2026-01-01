---
type: "EXAMPLE"
title: "Working with Either"
---


Basic operations on Either values:



```kotlin
import arrow.core.*

// Creating Either values
val success: Either<String, Int> = 42.right()
val failure: Either<String, Int> = "Error".left()

// Extracting values
val value1: Int? = success.getOrNull()  // 42
val value2: Int? = failure.getOrNull()  // null

val value3: Int = success.getOrElse { 0 }  // 42
val value4: Int = failure.getOrElse { 0 }  // 0

// Checking state
val isRight: Boolean = success.isRight()  // true
val isLeft: Boolean = failure.isLeft()     // true

// Pattern matching with fold
fun describe(either: Either<String, Int>): String =
    either.fold(
        ifLeft = { error -> "Error: $error" },
        ifRight = { value -> "Success: $value" }
    )

// Using when (with sealed types)
fun handleUser(result: Either<UserError, User>): String =
    when (result) {
        is Either.Left -> when (val error = result.value) {
            is UserError.NotFound -> "User ${error.id} not found"
            is UserError.InvalidEmail -> "Invalid email: ${error.email}"
            is UserError.AlreadyExists -> "User exists: ${error.email}"
            UserError.Unauthorized -> "Not authorized"
        }
        is Either.Right -> "Found user: ${result.value.name}"
    }
```
