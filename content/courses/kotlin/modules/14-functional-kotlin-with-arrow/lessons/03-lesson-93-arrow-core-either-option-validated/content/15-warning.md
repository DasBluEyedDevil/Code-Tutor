---
type: "WARNING"
title: "Common Mistakes"
---


### Using zipOrAccumulate for Sequential Operations

```kotlin
// WRONG - zipOrAccumulate is for independent validations
// Don't put dependent operations in separate lambdas

// RIGHT - Use Either for dependent operations
val result = either {
    val validEmail = validateEmail(email).bind()
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

