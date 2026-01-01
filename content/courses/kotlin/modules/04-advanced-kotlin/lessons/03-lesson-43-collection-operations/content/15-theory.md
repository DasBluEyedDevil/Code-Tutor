---
type: "THEORY"
title: "Solution 2: Text Processing"
---



**Explanation**:
- `count` with predicate for conditional counting
- `map` + `toSet` for unique values
- `groupBy` organizes by key
- `filter` + `maxByOrNull` finds specific maximum
- Chaining operations creates powerful pipelines

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

    // 1. Count errors
    val errorCount = logs.count { it.level == "ERROR" }
    println("Error count: $errorCount")  // 2

    // 2. Unique users
    val uniqueUsers = logs.map { it.user }.toSet()
    println("Unique users: $uniqueUsers")  // [alice, bob, charlie]

    // 3. Group by log level
    val byLevel = logs.groupBy { it.level }
    println("\nLogs by level:")
    byLevel.forEach { (level, entries) ->
        println("  $level: ${entries.size}")
    }
    // INFO: 3
    // ERROR: 2
    // WARN: 1

    // 4. Most recent error
    val recentError = logs
        .filter { it.level == "ERROR" }
        .maxByOrNull { it.timestamp }

    println("\nMost recent error:")
    println("  User: ${recentError?.user}")
    println("  Message: ${recentError?.message}")
    // User: alice
    // Message: Timeout

    // Bonus: Activity by user
    val activityByUser = logs
        .groupBy { it.user }
        .mapValues { (_, entries) -> entries.size }
        .toList()
        .sortedByDescending { it.second }

    println("\nActivity by user:")
    activityByUser.forEach { (user, count) ->
        println("  $user: $count actions")
    }
    // alice: 3 actions
    // bob: 2 actions
    // charlie: 1 actions
}
```
