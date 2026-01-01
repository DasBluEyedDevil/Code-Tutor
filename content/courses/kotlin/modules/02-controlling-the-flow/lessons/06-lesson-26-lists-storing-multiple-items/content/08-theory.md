---
type: "THEORY"
title: "Functional Operations on Lists"
---


### Map (Transform Each Element)


**Map pattern:**

### Filter (Keep Only Matching Items)


**Filter pattern:**

### Combining Map and Filter


### Other Useful Operations


---



```kotlin
fun main() {
    val numbers = listOf(1, 2, 3, 4, 5)

    // Sum
    println("Sum: ${numbers.sum()}")  // 15

    // Average
    println("Average: ${numbers.average()}")  // 3.0

    // Max and Min
    println("Max: ${numbers.maxOrNull()}")  // 5
    println("Min: ${numbers.minOrNull()}")  // 1

    // Any (at least one matches)
    println("Any > 3? ${numbers.any { it > 3 }}")  // true

    // All (all match)
    println("All > 0? ${numbers.all { it > 0 }}")  // true

    // None (none match)
    println("None < 0? ${numbers.none { it < 0 }}")  // true
}
```
