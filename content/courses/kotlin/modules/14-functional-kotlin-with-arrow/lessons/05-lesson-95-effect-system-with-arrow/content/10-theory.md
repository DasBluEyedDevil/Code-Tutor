---
type: "THEORY"
title: "Raise vs Either Return"
---


### When to Use Raise (context parameter)

1. **Internal implementation**: Functions called within `either { }` blocks
2. **Many operations**: When you'd have many `.bind()` calls
3. **Clean imperative style**: When you want code that reads naturally

### When to Return Either

1. **Public API**: Library boundaries where callers might not use Raise
2. **Interop**: When working with code that expects Either
3. **Explicit error handling**: When you want the type signature to show errors

### Conversion

```kotlin
// Either-returning function
fun getUser(id: Long): Either<UserError, User>

// Inside Raise context
context(raise: Raise<UserError>)
fun doWork() {
    val user = getUser(123).bind()  // Either -> Raise
}

// Raise function
context(raise: Raise<UserError>)
fun getUser(id: Long): User

// Get Either from Raise
val result: Either<UserError, User> = either {
    getUser(123)  // Raise -> Either
}
```

---

