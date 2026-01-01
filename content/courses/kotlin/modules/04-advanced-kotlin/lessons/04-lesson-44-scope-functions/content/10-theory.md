---
type: "THEORY"
title: "also: Side Effects, Return Object"
---


`also` uses `it` context and returns the object itself.

### Basic Usage


### Debugging and Logging


### Validation with Side Effects


### Real-World Example: File Operations


---



```kotlin
import java.io.File

fun processFile(path: String): List<String> {
    return File(path)
        .also { println("Reading file: ${it.absolutePath}") }
        .also { require(it.exists()) { "File not found" } }
        .readLines()
        .also { println("Read ${it.size} lines") }
        .filter { it.isNotEmpty() }
        .also { println("After filtering: ${it.size} non-empty lines") }
}
```
