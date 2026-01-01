---
type: "THEORY"
title: "runCatching for Safe Execution"
---

Kotlin's `runCatching` wraps results in a `Result` type:

```kotlin
val result: Result<User> = runCatching {
    fetchUser(id)
}

// Handle both cases
result
    .onSuccess { user -> println("Got: $user") }
    .onFailure { error -> println("Failed: ${error.message}") }

// Or get value with fallback
val user = result.getOrNull()
val userOrDefault = result.getOrDefault(defaultUser)
val userOrElse = result.getOrElse { error -> handleError(error) }
```

### Pattern: Safe Parallel Fetching
```kotlin
suspend fun loadDashboard() = supervisorScope {
    val userResult = async { runCatching { fetchUser() } }
    val postsResult = async { runCatching { fetchPosts() } }
    val statsResult = async { runCatching { fetchStats() } }
    
    DashboardState(
        user = userResult.await().getOrNull(),
        posts = postsResult.await().getOrDefault(emptyList()),
        stats = statsResult.await().getOrNull(),
        errors = listOfNotNull(
            userResult.await().exceptionOrNull()?.message,
            postsResult.await().exceptionOrNull()?.message,
            statsResult.await().exceptionOrNull()?.message
        )
    )
}
```