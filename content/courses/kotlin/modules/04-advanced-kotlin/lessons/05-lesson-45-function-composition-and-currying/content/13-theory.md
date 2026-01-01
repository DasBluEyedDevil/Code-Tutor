---
type: "THEORY"
title: "Exercise 2: Currying Implementation"
---


**Goal**: Implement a curry function for 2-parameter functions.

**Task**:


---



```kotlin
fun <A, B, C> curry(f: (A, B) -> C): (A) -> (B) -> C {
    // TODO: Implement
}

fun main() {
    val add = { a: Int, b: Int -> a + b }
    val multiply = { a: Int, b: Int -> a * b }

    // TODO: Test currying
}
```
