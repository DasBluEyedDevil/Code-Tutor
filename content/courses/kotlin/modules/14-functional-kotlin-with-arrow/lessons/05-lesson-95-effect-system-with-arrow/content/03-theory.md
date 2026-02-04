---
type: "THEORY"
title: "What is Raise?"
---


### The Problem with Either Everywhere

```kotlin
// Every function must return Either
fun getUser(id: Long): Either<UserError, User>
fun validateUser(user: User): Either<UserError, User>
fun saveUser(user: User): Either<UserError, User>

// Lots of .bind() calls
either {
    val user = getUser(id).bind()
    val validated = validateUser(user).bind()
    val saved = saveUser(validated).bind()
    saved
}
```

### Raise Makes It Cleaner

```kotlin
// Functions declare they can raise errors via context parameter
context(raise: Raise<UserError>)
fun getUser(id: Long): User

context(raise: Raise<UserError>)
fun validateUser(user: User): User

// No .bind() needed!
either {
    val user = getUser(id)
    val validated = validateUser(user)
    val saved = saveUser(validated)
    saved
}
```

> **Context Parameters (Kotlin 2.2+)**: Arrow's Raise DSL uses Kotlin's *context parameters* feature. The syntax `context(raise: Raise<E>)` provides a named parameter that the compiler passes implicitly. Enable with `-Xcontext-parameters` in your build. Earlier Arrow examples used the deprecated *context receivers* syntax `context(Raise<E>)` (without a parameter name) -- if you encounter that in older code, migrate to the named form.

---

