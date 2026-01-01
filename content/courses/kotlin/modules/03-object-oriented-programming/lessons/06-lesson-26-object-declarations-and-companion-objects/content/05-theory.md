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
