---
type: "ANALOGY"
title: "The Concept: Why Delegation Matters"
---


### The Problem: Code Duplication

Without delegation:


### The Solution: Class Delegation


**Benefits**:
- No boilerplate forwarding code
- Composition over inheritance
- Clear separation of concerns

---



```kotlin
class Logger(printer: Printer) : Printer by printer {
    fun log(message: String) {
        print("[LOG] $message")
    }
}

fun main() {
    val logger = Logger(ConsolePrinter())
    logger.print("Hello")     // Delegated to ConsolePrinter
    logger.log("Important")   // [LOG] Important
}
```
