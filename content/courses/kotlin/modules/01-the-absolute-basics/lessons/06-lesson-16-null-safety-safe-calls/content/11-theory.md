---
type: "THEORY"
title: "The let Function"
---


`let` executes a block of code only if the value is not null.

`let` is a "scope function" that is often used together with the safe call operator `?.` to perform operations on a non-null object.

### Basic Usage
The code inside the `let` block only runs if the variable is NOT null.

```kotlin
val name: String? = "Alice"
name?.let {
    println("The name is $it") // 'it' refers to the non-null name
}
```

### When Value is Null
If the value is null, the `let` block is skipped entirely.

### Practical Example
Use `let` when you want to call one or more functions that require a non-null argument.

### let with Return Value

The `let` block returns the result of its last expression. This is useful when you want to transform a nullable value:



```kotlin
val name: String? = "Alice"

val uppercaseName = name?.let {
    it.uppercase()
} ?: "UNKNOWN"

println(uppercaseName)  // ALICE
```
