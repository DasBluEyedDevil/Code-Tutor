---
type: "THEORY"
title: "The let Function"
---


`let` executes a block of code only if the value is not null.

### Basic Usage


### When Value is Null


### Practical Example


### let with Return Value


---



```kotlin
val name: String? = "Alice"

val uppercaseName = name?.let {
    it.uppercase()
} ?: "UNKNOWN"

println(uppercaseName)  // ALICE
```
