---
type: "THEORY"
title: "Generic Constraints with Where"
---


Complex constraints often need the `where` clause:


### Multiple Constraints Example


---



```kotlin
fun <T> findMax(items: List<T>) where T : Comparable<T>, T : Number {
    val max = items.maxOrNull()
    max?.let {
        println("Max value: $it, Double value: ${it.toDouble()}")
    }
}

fun main() {
    findMax(listOf(1, 5, 3, 9, 2))
    // Max value: 9, Double value: 9.0

    findMax(listOf(1.5, 2.8, 0.9))
    // Max value: 2.8, Double value: 2.8
}
```
