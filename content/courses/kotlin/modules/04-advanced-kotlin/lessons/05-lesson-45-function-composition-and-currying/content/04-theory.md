---
type: "THEORY"
title: "Function Composition"
---


Combining functions to create new functions.

### Mathematical Foundation

In math: `(f ∘ g)(x) = f(g(x))`


### Generic Composition


### Infix Composition Operator

Make composition more readable with `infix`:


### Practical Example: Data Transformation Pipeline


---



```kotlin
// Individual transformations
val validateEmail: (String) -> String? = { email ->
    if (email.contains("@")) email else null
}

val normalizeEmail: (String) -> String = { email ->
    email.trim().lowercase()
}

val extractDomain: (String) -> String = { email ->
    email.substringAfter("@")
}

// Composition
infix fun <A, B, C> ((A) -> B?).thenIfNotNull(other: (B) -> C): (A) -> C? {
    return { x -> this(x)?.let(other) }
}

val processPipeline = validateEmail thenIfNotNull normalizeEmail

val email1 = processPipeline("  USER@EXAMPLE.COM  ")
println(email1)  // user@example.com

val email2 = processPipeline("invalid")
println(email2)  // null
```
