---
type: "THEORY"
title: "First-Class Functions"
---


In Kotlin, functions are **first-class citizens**—they're treated like any other value.

### Assigning Functions to Variables


### Anonymous Functions

Functions without names:


### Lambda Expressions (Preview)

Shorter syntax for anonymous functions:


### Why This Matters


---



```kotlin
// Store different math operations
val add = { a: Int, b: Int -> a + b }
val subtract = { a: Int, b: Int -> a - b }
val multiply = { a: Int, b: Int -> a * b }

// Use them interchangeably
fun calculate(a: Int, b: Int, operation: (Int, Int) -> Int): Int {
    return operation(a, b)
}

println(calculate(10, 5, add))       // 15
println(calculate(10, 5, subtract))  // 5
println(calculate(10, 5, multiply))  // 50
```
