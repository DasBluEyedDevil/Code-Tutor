---
type: "THEORY"
title: "with: Non-Extension Version"
---


`with` is not an extension function; you pass the object as parameter. Uses `this` context.

### Basic Usage


### Multiple Operations on Object


### When to Use with vs run


### Real-World Example: Configuration


---



```kotlin
data class DatabaseConfig(
    var host: String = "",
    var port: Int = 0,
    var username: String = "",
    var password: String = "",
    var database: String = ""
) {
    fun validate() = host.isNotEmpty() && username.isNotEmpty()
}

val config = DatabaseConfig()

val isValid = with(config) {
    host = "localhost"
    port = 5432
    username = "admin"
    password = "secret"
    database = "myapp"
    validate()
}

println("Config valid: $isValid")  // true
```
