---
type: "THEORY"
title: "Lambda with Receiver"
---


The foundation of Kotlin DSLs is **lambda with receiver**.

### Regular Lambda


### Lambda with Receiver


**Key Difference**: `StringBuilder.() -> Unit` means `this` inside the lambda is `StringBuilder`.

### Visualizing the Difference


### Standard Library Examples

Kotlin's standard library uses lambdas with receiver:


---



```kotlin
// apply
val person = Person().apply {
    name = "Alice"  // this.name
    age = 25        // this.age
}

// with
val result = with(person) {
    println(name)   // this.name
    println(age)    // this.age
}

// buildString (actually uses lambda with receiver)
val text = buildString {
    append("Line 1")
    appendLine()
    append("Line 2")
}
```
