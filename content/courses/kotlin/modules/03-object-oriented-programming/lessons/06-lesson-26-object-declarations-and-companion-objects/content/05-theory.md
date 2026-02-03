---
type: "THEORY"
title: "Object Declarations (Singletons)"
---


**Object declaration** creates a singleton - a class with exactly one instance.

### Basic Singleton


**Output**:

**Key Points**:
- Created on first access (lazy initialization)
- Thread-safe by default
- Cannot have constructors
- Can implement interfaces and extend classes

> **`data object` variant**: When you use `data object` instead of `object`, Kotlin generates a clean `toString()` (e.g., `"Loading"` instead of `"Loading@1a2b3c"`), a consistent `equals()`, and a stable `hashCode()`. Use `data object` for sealed class branches and anywhere a readable string representation matters.

### Real-World Example: Application Config


### Singleton with Interface


---



```kotlin
interface Logger {
    fun log(message: String)
    fun error(message: String)
}

object ConsoleLogger : Logger {
    override fun log(message: String) {
        println("[LOG] $message")
    }

    override fun error(message: String) {
        println("[ERROR] $message")
    }
}

fun processData(logger: Logger) {
    logger.log("Processing data...")
    logger.error("An error occurred!")
}

fun main() {
    processData(ConsoleLogger)
}
```
