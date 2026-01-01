---
type: "THEORY"
title: "Exercise 2: Text Processing"
---


**Goal**: Process log files using collection operations.

**Task**: Parse log entries and:
1. Count errors
2. Find unique users
3. Group by log level
4. Get most recent error


---



```kotlin
data class LogEntry(
    val timestamp: Long,
    val level: String,
    val user: String,
    val message: String
)

fun main() {
    val logs = listOf(
        LogEntry(1000, "INFO", "alice", "User logged in"),
        LogEntry(2000, "ERROR", "bob", "Connection failed"),
        LogEntry(3000, "INFO", "alice", "Data saved"),
        LogEntry(4000, "WARN", "charlie", "Slow query"),
        LogEntry(5000, "ERROR", "alice", "Timeout"),
        LogEntry(6000, "INFO", "bob", "Request completed")
    )

    // TODO: Process logs
}
```
