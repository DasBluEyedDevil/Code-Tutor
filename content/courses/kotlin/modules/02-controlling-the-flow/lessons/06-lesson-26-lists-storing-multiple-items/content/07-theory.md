---
type: "THEORY"
title: "Common List Operations"
---


### Checking Contents


### Finding Elements


### Sorting Lists


---



```kotlin
fun main() {
    val numbers = mutableListOf(5, 2, 8, 1, 9)

    // Sort in place (modifies original)
    numbers.sort()
    println("Sorted: $numbers")  // [1, 2, 5, 8, 9]

    // Reverse sort
    numbers.sortDescending()
    println("Descending: $numbers")  // [9, 8, 5, 2, 1]

    // Sorted (returns new list, original unchanged)
    val original = listOf(5, 2, 8, 1, 9)
    val sorted = original.sorted()
    println("Original: $original")  // [5, 2, 8, 1, 9]
    println("Sorted: $sorted")      // [1, 2, 5, 8, 9]
}
```
