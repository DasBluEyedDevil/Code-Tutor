---
type: "EXAMPLE"
title: "Running Raise Effects"
---


Converting Raise to Either or handling directly:



```kotlin
import arrow.core.raise.*
import arrow.core.*

// either { } provides Raise<E> context and returns Either<E, A>
val result: Either<UserError, User> = either {
    getUser(123)  // Has access to Raise<UserError>
}

// fold - handle both cases
fold(
    block = { getUser(123) },
    recover = { error: UserError ->
        when (error) {
            is UserError.NotFound -> println("User not found")
            is UserError.InvalidId -> println("Invalid ID")
            UserError.Unauthorized -> println("Not authorized")
        }
        null
    },
    transform = { user ->
        println("Found: ${user.name}")
        user
    }
)

// recover - handle errors and potentially continue
val recovered: User? = recover(
    block = { getUser(123) },
    recover = { error ->
        when (error) {
            is UserError.NotFound -> createDefaultUser()
            else -> null
        }
    }
)
```
