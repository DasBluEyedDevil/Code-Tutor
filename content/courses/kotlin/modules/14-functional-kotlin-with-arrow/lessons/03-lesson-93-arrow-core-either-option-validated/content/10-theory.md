---
type: "THEORY"
title: "Validated - Accumulating Errors"
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

### Validated Collects All Errors

```kotlin
validateName(name)
    .zip(validateEmail(email), validateAge(age)) { n, e, a ->
        User(n, e, a)
    }
// Returns ALL validation errors at once!
```

---

