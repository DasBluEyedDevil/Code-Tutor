---
type: "THEORY"
title: "Solution: Logging System"
---



---



```kotlin
object Logger {
    private var enabled = true
    private var infoCount = 0
    private var warningCount = 0
    private var errorCount = 0

    fun enable() {
        enabled = true
        println("[LOGGER] Logging enabled")
    }

    fun disable() {
        enabled = false
        println("[LOGGER] Logging disabled")
    }

    fun info(message: String) {
        if (!enabled) return
        infoCount++
        println("[INFO] $message")
    }

    fun warning(message: String) {
        if (!enabled) return
        warningCount++
        println("[WARNING] $message")
    }

    fun error(message: String) {
        if (!enabled) return
        errorCount++
        println("[ERROR] $message")
    }

    fun printStatistics() {
        println("\n=== Logging Statistics ===")
        println("Info messages: $infoCount")
        println("Warning messages: $warningCount")
        println("Error messages: $errorCount")
        println("Total messages: ${infoCount + warningCount + errorCount}")
        println("==========================\n")
    }

    fun reset() {
        infoCount = 0
        warningCount = 0
        errorCount = 0
        println("[LOGGER] Statistics reset")
    }
}

fun main() {
    Logger.info("Application started")
    Logger.info("Loading configuration")
    Logger.warning("Configuration file not found, using defaults")
    Logger.info("Connecting to database")
    Logger.error("Failed to connect to database")
    Logger.info("Retrying connection")
    Logger.info("Connected successfully")

    Logger.printStatistics()

    Logger.disable()
    Logger.info("This won't be logged")

    Logger.enable()
    Logger.info("This will be logged")

    Logger.printStatistics()
}
```
