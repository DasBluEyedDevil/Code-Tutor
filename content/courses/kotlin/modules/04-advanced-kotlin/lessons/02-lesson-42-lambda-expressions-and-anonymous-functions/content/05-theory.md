---
type: "THEORY"
title: "Trailing Lambda Syntax"
---


One of Kotlin's most elegant features!

### The Rule

**If a lambda is the last parameter, move it outside the parentheses.**


### Real-World Examples


### Multiple Parameters with Trailing Lambda


---



```kotlin
// Function with multiple parameters, lambda is last
fun processData(
    prefix: String,
    suffix: String,
    transform: (String) -> String
): String {
    return prefix + transform("data") + suffix
}

// Usage with trailing lambda
val result = processData("[", "]") { it.uppercase() }
println(result)  // [DATA]

// Without trailing lambda (less readable)
val result2 = processData("[", "]", { it.uppercase() })
```
