---
type: "THEORY"
title: "Lambda Syntax Variations"
---


Kotlin offers multiple ways to write lambdas, from verbose to ultra-concise.

### The Full Syntax Journey

Let's trace the evolution from most explicit to most concise:


### Syntax Breakdown


### Multi-Line Lambdas


**Key Rule**: The last expression in a lambda is automatically returned (no `return` keyword needed).

---



```kotlin
val complexOperation = numbers.map { number ->
    println("Processing: $number")
    val doubled = number * 2
    val squared = doubled * doubled
    squared  // Last expression is the return value
}

println(complexOperation)  // [4, 16, 36, 64, 100]
```
