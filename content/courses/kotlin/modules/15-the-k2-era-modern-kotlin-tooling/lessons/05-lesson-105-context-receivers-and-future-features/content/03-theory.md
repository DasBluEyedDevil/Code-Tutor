---
type: "THEORY"
title: "Understanding Context Receivers"
---


### The Problem

Kotlin has extension functions that operate on a single receiver:

```kotlin
fun String.wordCount(): Int = this.split(" ").size
"Hello World".wordCount()  // String is the receiver
```

But what if a function needs multiple "contexts" to operate?

```kotlin
class Logger { fun info(msg: String) { /* ... */ } }
class Database { fun query(sql: String): List<Row> { /* ... */ } }

// How do we write a function that needs both?
// Traditional approach: pass as parameters
fun loadUsers(logger: Logger, db: Database): List<User> {
    logger.info("Loading users")
    return db.query("SELECT * FROM users").map { /* ... */ }
}
```

### The Solution: Context Receivers

Context receivers allow functions to require multiple implicit receivers:

```kotlin
context(Logger, Database)
fun loadUsers(): List<User> {
    info("Loading users")  // Logger is in context
    return query("SELECT * FROM users").map { /* ... */ }  // Database too
}
```

---

