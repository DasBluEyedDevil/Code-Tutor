---
type: "THEORY"
title: "Default Interface Methods"
---


Kotlin interfaces can have default implementations (unlike Java pre-8):


---



```kotlin
interface Logger {
    fun log(message: String) {
        println("[LOG] $message")  // Default implementation
    }

    fun error(message: String) {
        println("[ERROR] $message")  // Default implementation
    }

    fun debug(message: String)  // Must be implemented
}

class ConsoleLogger : Logger {
    override fun debug(message: String) {
        println("[DEBUG] $message")
    }
    // log() and error() use default implementations
}

class FileLogger : Logger {
    override fun log(message: String) {
        println("[FILE LOG] Writing to file: $message")
    }

    override fun error(message: String) {
        println("[FILE ERROR] Writing error to file: $message")
    }

    override fun debug(message: String) {
        println("[FILE DEBUG] Writing debug to file: $message")
    }
}

fun main() {
    val console = ConsoleLogger()
    console.log("Application started")
    console.error("Connection failed")
    console.debug("Variable value: 42")

    println()

    val file = FileLogger()
    file.log("Application started")
    file.error("Connection failed")
    file.debug("Variable value: 42")
}
```
