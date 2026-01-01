---
type: "THEORY"
title: "Finding Elements"
---


### find: First Match or Null


### findLast: Last Match or Null


### first and last


### any, all, none: Boolean Checks


### Practical Example: Validation


---



```kotlin
data class User(val name: String, val age: Int, val email: String)

val users = listOf(
    User("Alice", 25, "alice@example.com"),
    User("Bob", 17, "bob@example.com"),
    User("Charlie", 30, "charlie@example.com")
)

// Check if any user is underage
val hasMinors = users.any { it.age < 18 }
println("Has minors: $hasMinors")  // true

// Check if all have valid emails
val allValidEmails = users.all { it.email.contains("@") }
println("All valid emails: $allValidEmails")  // true

// Check if no user has empty name
val noEmptyNames = users.none { it.name.isEmpty() }
println("No empty names: $noEmptyNames")  // true
```
