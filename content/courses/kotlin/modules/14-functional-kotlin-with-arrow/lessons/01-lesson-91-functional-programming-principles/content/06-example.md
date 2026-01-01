---
type: "EXAMPLE"
title: "Immutability with Data Classes"
---


Kotlin's data classes support immutable patterns:



```kotlin
// Immutable data class
data class User(
    val id: Long,
    val name: String,
    val email: String,
    val isActive: Boolean = true
)

// "Update" by creating new instance
fun updateEmail(user: User, newEmail: String): User =
    user.copy(email = newEmail)  // Returns new instance, original unchanged

fun deactivate(user: User): User =
    user.copy(isActive = false)

// Chaining updates
fun updateAndDeactivate(user: User, newEmail: String): User =
    user.copy(email = newEmail, isActive = false)

// Usage
val original = User(1, "John", "john@old.com")
val updated = updateEmail(original, "john@new.com")

println(original.email)  // "john@old.com" - unchanged!
println(updated.email)   // "john@new.com" - new instance
```
