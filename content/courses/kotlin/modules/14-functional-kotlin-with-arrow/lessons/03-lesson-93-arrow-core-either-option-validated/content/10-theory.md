---
type: "THEORY"
title: "Error Accumulation with zipOrAccumulate"
---


### The Problem with Either

Either short-circuits on first error:

```kotlin
// Only shows first error!
either {
    validateName(name).bind()    // Fails here
    validateEmail(email).bind()   // Never checked
    validateAge(age).bind()       // Never checked
}
```

### zipOrAccumulate Collects All Errors

```kotlin
either {
    zipOrAccumulate(
        { validateName(name) },
        { validateEmail(email) },
        { validateAge(age) }
    ) { n, e, a ->
        User(n, e, a)
    }
}
// Returns ALL validation errors at once as EitherNel!
```

---

