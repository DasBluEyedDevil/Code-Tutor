---
type: "EXAMPLE"
title: "Raise with Coroutines"
---


Integrating Raise with suspend functions:



```kotlin
import arrow.core.raise.*
import arrow.core.*
import kotlinx.coroutines.*

sealed interface ApiError {
    data class NetworkError(val cause: Throwable) : ApiError
    data class NotFound(val id: Long) : ApiError
    data object RateLimited : ApiError
}

// Suspend function with Raise
context(raise: Raise<ApiError>)
suspend fun fetchUser(id: Long): User {
    raise.ensure(id > 0) { ApiError.NotFound(id) }

    return try {
        apiClient.get("/users/$id")
    } catch (e: IOException) {
        raise.raise(ApiError.NetworkError(e))
    }
}

context(raise: Raise<ApiError>)
suspend fun fetchUserPosts(user: User): List<Post> {
    return try {
        apiClient.get("/users/${user.id}/posts")
    } catch (e: IOException) {
        raise.raise(ApiError.NetworkError(e))
    }
}

// Compose suspend functions with Raise
context(raise: Raise<ApiError>)
suspend fun getUserWithPosts(userId: Long): UserWithPosts {
    val user = fetchUser(userId)
    val posts = fetchUserPosts(user)
    return UserWithPosts(user, posts)
}

// Run and handle
suspend fun main() {
    val result: Either<ApiError, UserWithPosts> = either {
        getUserWithPosts(123)
    }
    
    result.fold(
        ifLeft = { error -> println("Error: $error") },
        ifRight = { data -> println("Got ${data.posts.size} posts") }
    )
}
```
