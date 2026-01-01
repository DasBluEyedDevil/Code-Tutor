---
type: "THEORY"
title: "Accessing List Elements"
---


### Indexing (Zero-Based)

Lists use **zero-based indexing**—the first element is at position 0:


**Visual representation:**

### Safe Access Methods


### First, Last, and More


---



```kotlin
fun main() {
    val numbers = listOf(10, 20, 30, 40, 50)

    println("First: ${numbers.first()}")     // 10
    println("Last: ${numbers.last()}")       // 50
    println("Size: ${numbers.size}")         // 5
    println("Is empty: ${numbers.isEmpty()}") // false

    // Safe versions
    val empty = emptyList<Int>()
    println(empty.firstOrNull())  // null (no error)
    println(empty.lastOrNull())   // null
}
```
