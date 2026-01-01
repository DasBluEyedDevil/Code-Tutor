---
type: "EXAMPLE"
title: "Kotlin 2.0 in Practice"
---

Modern Kotlin 2.0 patterns combine data classes, extension functions, scope functions, and collection operations. The K2 compiler infers types more accurately and compiles these patterns faster.

```kotlin
// Modern Kotlin 2.0 code examples

// 1. Data class with modern patterns
data class User(
    val id: Long,
    val name: String,
    val email: String,
    val roles: List<String> = emptyList()
)

// 2. Extension functions with smart casts
fun Any?.safeToString(): String = when {
    this == null -> "null"
    this is String -> this  // Smart cast
    this is Number -> this.toString()
    else -> this.toString()
}

// 3. Scope functions with improved inference
fun createUser(name: String): User {
    return User(
        id = System.currentTimeMillis(),
        name = name,
        email = "$name@example.com"
    ).also {
        println("Created user: ${it.name}")
    }
}

// 4. Collection operations (unchanged but faster compilation)
fun processUsers(users: List<User>): Map<Long, User> {
    return users
        .filter { it.roles.isNotEmpty() }
        .associateBy { it.id }
}

fun main() {
    val user = createUser("Alice")
    println("User email: ${user.email}")
    
    val mixedValues: List<Any?> = listOf("Hello", 42, null, 3.14)
    mixedValues.forEach { 
        println(it.safeToString()) 
    }
}
```
