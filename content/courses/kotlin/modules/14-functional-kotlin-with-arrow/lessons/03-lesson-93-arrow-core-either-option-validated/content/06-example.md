---
type: "EXAMPLE"
title: "Chaining with flatMap"
---


Sequentially compose operations that can fail:



```kotlin
import arrow.core.*

// Each function can fail with UserError
fun getUser(id: Long): Either<UserError, User> = /* ... */
fun validateEmail(email: String): Either<UserError, String> = /* ... */
fun updateEmail(user: User, email: String): Either<UserError, User> = /* ... */

// Chain with flatMap - short-circuits on first error
fun changeUserEmail(userId: Long, newEmail: String): Either<UserError, User> =
    getUser(userId)
        .flatMap { user ->
            validateEmail(newEmail).map { email ->
                user to email
            }
        }
        .flatMap { (user, email) ->
            updateEmail(user, email)
        }

// map transforms Right values (Left passes through)
val lengthResult: Either<String, Int> = "hello".right().map { it.length }  // Right(5)

val failedLength: Either<String, Int> = "error".left<String, String>().map { it.length }  // Left("error")
```
