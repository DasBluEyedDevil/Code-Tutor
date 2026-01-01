---
type: "THEORY"
title: "The suspend Keyword"
---

The `suspend` keyword marks functions that can pause and resume:

```kotlin
suspend fun fetchUserFromNetwork(userId: String): User {
    delay(2000) // Simulate network delay
    return User(userId, "John Doe")
}

suspend fun saveUserToDatabase(user: User) {
    delay(500) // Simulate database write
    println("User ${user.name} saved")
}

fun main() = runBlocking {
    val user = fetchUserFromNetwork("123") // Suspends here
    saveUserToDatabase(user) // Then suspends here
    println("Done!")
}
```

**Key Rules:**
1. `suspend` functions can only be called from coroutines or other `suspend` functions
2. They look like regular sequential code but execute asynchronously
3. The Kotlin compiler transforms them into state machines (you don't need to understand the internals)
4. Suspension points are where the function can pause execution