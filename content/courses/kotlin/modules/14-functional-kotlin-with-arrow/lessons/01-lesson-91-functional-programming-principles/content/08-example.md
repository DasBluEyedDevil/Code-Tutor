---
type: "EXAMPLE"
title: "Function Composition"
---


Build complex operations from simple functions:



```kotlin
// Simple functions
val trim: (String) -> String = { it.trim() }
val lowercase: (String) -> String = { it.lowercase() }
val removeSpaces: (String) -> String = { it.replace(" ", "") }

// Manual composition
val sanitize: (String) -> String = { removeSpaces(lowercase(trim(it))) }

// Usage
val input = "  Hello World  "
println(sanitize(input))  // "helloworld"

// Composing with infix functions
infix fun <A, B, C> ((A) -> B).andThen(g: (B) -> C): (A) -> C = { a ->
    g(this(a))
}

val sanitize2 = trim andThen lowercase andThen removeSpaces

// Composing in reverse (mathematical composition)
infix fun <A, B, C> ((B) -> C).compose(g: (A) -> B): (A) -> C = { a ->
    this(g(a))
}

val sanitize3 = removeSpaces compose lowercase compose trim
```
