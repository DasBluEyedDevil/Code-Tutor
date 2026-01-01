---
type: "EXAMPLE"
title: "Basic Context Receiver Usage"
---


Context receivers in practice:



```kotlin
// Enable with compiler flag:
// kotlinc -Xcontext-receivers
// Or in build.gradle.kts:
// kotlin {
//     compilerOptions {
//         freeCompilerArgs.add("-Xcontext-receivers")
//     }
// }

// Define context types
class Logger {
    fun info(message: String) = println("INFO: $message")
    fun error(message: String) = println("ERROR: $message")
}

class Database {
    fun execute(sql: String) = println("Executing: $sql")
    fun query(sql: String): List<Map<String, Any>> = emptyList()
}

// Function with context receivers
context(Logger)
fun loggedOperation(name: String, block: () -> Unit) {
    info("Starting: $name")
    block()
    info("Completed: $name")
}

context(Logger, Database)
fun createUser(name: String, email: String) {
    info("Creating user: $name")
    execute("INSERT INTO users (name, email) VALUES ('$name', '$email')")
    info("User created successfully")
}

// Usage: provide contexts with 'with'
fun main() {
    val logger = Logger()
    val database = Database()
    
    with(logger) {
        loggedOperation("task") {
            println("Doing something")
        }
    }
    
    with(logger) {
        with(database) {
            createUser("John", "john@example.com")
        }
    }
}
```
