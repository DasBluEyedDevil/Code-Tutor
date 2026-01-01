---
type: "THEORY"
title: "Partial Application"
---


Fixing some arguments of a function, creating a new function.

### Manual Partial Application


### Generic Partial Application Helper


### Practical Example: Database Queries


---



```kotlin
// Generic query function
fun query(
    database: String,
    table: String,
    columns: List<String>,
    where: String
): String {
    return "SELECT ${columns.joinToString()} FROM $database.$table WHERE $where"
}

// Partially apply database
fun queriesFor(database: String) = { table: String, columns: List<String>, where: String ->
    query(database, table, columns, where)
}

// Partially apply database and table
fun tableQueries(database: String, table: String) = { columns: List<String>, where: String ->
    query(database, table, columns, where)
}

// Usage
val prodQueries = queriesFor("production")
val userQuery = prodQueries("users", listOf("id", "name", "email"), "active = true")
println(userQuery)
// SELECT id, name, email FROM production.users WHERE active = true

val userTableQueries = tableQueries("production", "users")
val activeUsers = userTableQueries(listOf("*"), "active = true")
println(activeUsers)
// SELECT * FROM production.users WHERE active = true
```
