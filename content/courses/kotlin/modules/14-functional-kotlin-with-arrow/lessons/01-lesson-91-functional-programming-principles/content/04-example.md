---
type: "EXAMPLE"
title: "Pure vs Impure Functions"
---


Understanding the difference is fundamental:



```kotlin
// PURE FUNCTIONS - same input always gives same output
fun add(a: Int, b: Int): Int = a + b  // Pure: no side effects

fun double(x: Int): Int = x * 2  // Pure: deterministic

fun formatName(first: String, last: String): String =
    "$first $last".trim()  // Pure: only uses inputs

// IMPURE FUNCTIONS - have side effects or depend on external state
fun now(): Long = System.currentTimeMillis()  // Impure: reads system clock

var counter = 0
fun increment(): Int = ++counter  // Impure: mutates global state

fun log(message: String) {
    println(message)  // Impure: I/O side effect
}

fun readConfig(): String =
    File("config.json").readText()  // Impure: reads file system
```
