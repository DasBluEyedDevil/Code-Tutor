---
type: "THEORY"
title: "Generic Classes"
---


### Basic Generic Class

A generic class has type parameters in angle brackets:


### Multiple Type Parameters

Classes can have multiple type parameters:


### Generic Collections

Kotlin's standard collections are generic:


---



```kotlin
fun main() {
    // List<T>
    val numbers: List<Int> = listOf(1, 2, 3)
    val words: List<String> = listOf("a", "b", "c")

    // Map<K, V>
    val ages: Map<String, Int> = mapOf(
        "Alice" to 25,
        "Bob" to 30
    )

    // Set<T>
    val uniqueNumbers: Set<Int> = setOf(1, 2, 2, 3)  // [1, 2, 3]
}
```
