---
type: "THEORY"
title: "Function References"
---


Referring to existing functions instead of creating new lambdas.

### Function Reference Syntax

Use `::` to reference a function:


### Top-Level Function References


### Built-In Function References


---



```kotlin
val strings = listOf("  hello  ", "  world  ", "  kotlin  ")

// Method reference
val trimmed = strings.map(String::trim)
println(trimmed)  // [hello, world, kotlin]

// Property reference
val lengths = strings.map(String::length)
println(lengths)  // [9, 9, 10]
```
