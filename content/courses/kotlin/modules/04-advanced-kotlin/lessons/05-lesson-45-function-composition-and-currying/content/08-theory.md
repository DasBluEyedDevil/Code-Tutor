---
type: "THEORY"
title: "Infix Functions"
---


Make function calls read like natural language.

### Basic Infix


### Building Readable DSLs


### Practical Example: Query DSL


---



```kotlin
data class Query(val table: String, val conditions: List<String> = emptyList())

infix fun String.from(table: String) = Query(table)

infix fun Query.where(condition: String) = this.copy(
    conditions = this.conditions + condition
)

infix fun Query.and(condition: String) = this.copy(
    conditions = this.conditions + condition
)

fun Query.build(): String {
    val whereCl= if (conditions.isNotEmpty()) {
        " WHERE ${conditions.joinToString(" AND ")}"
    } else ""
    return "SELECT $table FROM $table$whereClause"
}

// Usage: reads like SQL!
val query = "users" from "users_table" where "age > 18" and "active = true"
println(query.build())
// SELECT users FROM users_table WHERE age > 18 AND active = true
```
