---
type: "THEORY"
title: "Passing Functions as Parameters"
---


One of the most powerful FP techniques.

### Example 1: Retry Logic


### Example 2: Timing Function Execution


### Example 3: List Transformation


---



```kotlin
fun List<Int>.customMap(transform: (Int) -> Int): List<Int> {
    val result = mutableListOf<Int>()
    for (item in this) {
        result.add(transform(item))
    }
    return result
}

val numbers = listOf(1, 2, 3, 4, 5)

val doubled = numbers.customMap { it * 2 }
println(doubled)  // [2, 4, 6, 8, 10]

val squared = numbers.customMap { it * it }
println(squared)  // [1, 4, 9, 16, 25]
```
