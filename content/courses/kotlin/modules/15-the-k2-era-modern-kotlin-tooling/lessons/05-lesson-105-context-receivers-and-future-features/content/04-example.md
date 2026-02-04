---
type: "EXAMPLE"
title: "Basic Context Parameter Usage"
---


Context parameters in practice:



```kotlin
// Enable with compiler flag in build.gradle.kts:
// kotlin {
//     compilerOptions {
//         freeCompilerArgs.add("-Xcontext-parameters")
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

// Function with a single context parameter
context(logger: Logger)
fun loggedOperation(name: String, block: () -> Unit) {
    logger.info("Starting: $name")
    block()
    logger.info("Completed: $name")
}

// Function with multiple context parameters
context(logger: Logger, db: Database)
fun createUser(name: String, email: String) {
    logger.info("Creating user: $name")
    db.execute("INSERT INTO users (name, email) VALUES ('$name', '$email')")
    logger.info("User created successfully")
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
