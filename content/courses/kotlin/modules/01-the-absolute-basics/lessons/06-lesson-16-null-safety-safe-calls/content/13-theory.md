---
type: "THEORY"
title: "Exercise 1: Safe User Profile Display"
---


**Goal**: Create a user profile system that handles missing data safely.

**Starter Code**:
```kotlin
data class User(
    val name: String?,
    val email: String?,
    val phone: String?,
    val address: String?
)

fun displayProfile(user: User?) {
    // 1. Check if user is null
    // 2. Use Elvis operator for nullable fields
}

fun main() {
    val user1 = User("Alice", "alice@example.com", "555-1234", "123 Main St")
    val user2 = User("Bob", "bob@example.com", null, null)
    
    displayProfile(user1)
    displayProfile(user2)
    displayProfile(null)
}
```

**Expected Output**:
```text
=== User Profile ===
Name: Alice Johnson
...
No user data available
```


