---
type: "WARNING"
title: "Common ROP Mistakes"
---


### Catching Everything

```kotlin
// WRONG - hides the error type
fun processOrder(order: Order): Either<Throwable, Order> = either {
    // Error type is too generic!
}

// RIGHT - specific error types
fun processOrder(order: Order): Either<OrderError, Order> = either {
    // Caller knows exactly what can go wrong
}
```

### Not Handling Errors

```kotlin
// WRONG - ignoring the Either
processOrder(order)  // Result discarded!

// RIGHT - always handle the result
processOrder(order).fold(
    ifLeft = { handleError(it) },
    ifRight = { handleSuccess(it) }
)
```

### Mixing Exceptions and Either

```kotlin
// WRONG - throws inside either block
either {
    val user = getUser(id).bind()
    if (user.isBanned) throw IllegalStateException()  // Escapes!
}

// RIGHT - use ensure or raise
either {
    val user = getUser(id).bind()
    ensure(!user.isBanned) { UserError.Banned(user.id) }
}
```

---

