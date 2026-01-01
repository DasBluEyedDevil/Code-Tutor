---
type: "WARNING"
title: "Common Mistakes"
---


### Using Validated for Sequential Operations

```kotlin
// WRONG - Validated is for parallel/independent validations
val result = validateEmail(email).andThen { 
    checkEmailNotTaken(it)  // This depends on email being valid!
}

// RIGHT - Use Either for dependent operations
val result = either {
    val validEmail = validateEmail(email).toEither().bind()
    val available = checkEmailNotTaken(validEmail).bind()
    available
}
```

### Mixing Left and Right

```kotlin
// Remember: Left = Error, Right = Success
fun findUser(id: Long): Either<UserError, User> =
    if (id > 0) User(id, "John").right()  // Success goes RIGHT
    else UserError.NotFound(id).left()     // Error goes LEFT
```

---

