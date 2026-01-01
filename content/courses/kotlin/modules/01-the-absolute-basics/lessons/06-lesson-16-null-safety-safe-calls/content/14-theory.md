---
type: "THEORY"
title: "Solution 1: Safe User Profile Display"
---



**Solution Code**:

```kotlin
data class User(
    val name: String?,
    val email: String?,
    val phone: String?,
    val address: String?
)

fun displayProfile(user: User?) {
    if (user == null) {
        println("No user data available")
        return
    }

    println("=== User Profile ===")
    println("Name: ${user.name ?: "Not provided"}")
    println("Email: ${user.email ?: "Not provided"}")
    println("Phone: ${user.phone ?: "Not provided"}")
    println("Address: ${user.address ?: "Not provided"}")
}

fun main() {
    val user1 = User("Alice Johnson", "alice@example.com", "555-1234", "123 Main St")
    val user2 = User("Bob Smith", "bob@example.com", null, null)
    
    displayProfile(user1)
    println()
    displayProfile(user2)
    println()
    displayProfile(null)
}
```

**Sample Output**:
