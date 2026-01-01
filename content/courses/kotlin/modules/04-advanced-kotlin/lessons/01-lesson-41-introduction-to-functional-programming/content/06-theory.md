---
type: "THEORY"
title: "Lambda Expressions Basics"
---


Lambdas are concise anonymous functions.

### Basic Lambda Syntax


### Lambda Structure


Examples:


### Type Inference

Kotlin often infers lambda parameter types:


---



```kotlin
// Explicit type
val numbers = listOf(1, 2, 3, 4, 5)
val doubled = numbers.map({ x: Int -> x * 2 })

// Type inferred (cleaner!)
val tripled = numbers.map({ x -> x * 3 })

// Even shorter with 'it' (single parameter)
val quadrupled = numbers.map({ it * 4 })

// Trailing lambda (move outside parentheses)
val quintupled = numbers.map { it * 5 }

println(quintupled)  // [5, 10, 15, 20, 25]
```
