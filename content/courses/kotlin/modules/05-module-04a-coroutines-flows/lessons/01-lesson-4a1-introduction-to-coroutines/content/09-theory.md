---
type: "THEORY"
title: "Exercise: Sequential vs Concurrent"
---

**Goal**: Understand the difference between sequential and concurrent execution.

**Part 1**: Run these functions sequentially and measure time:
```kotlin
import kotlinx.coroutines.*
import kotlin.system.measureTimeMillis

suspend fun fetchUser(): String {
    delay(1000)
    return "User"
}

suspend fun fetchPosts(): String {
    delay(1000)
    return "Posts"
}

fun main() = runBlocking {
    val time = measureTimeMillis {
        val user = fetchUser()    // 1 second
        val posts = fetchPosts()  // 1 second
        println("$user and $posts")
    }
    println("Sequential: ${time}ms") // ~2000ms
}
```

**Part 2**: Now run them concurrently:
```kotlin
fun main() = runBlocking {
    val time = measureTimeMillis {
        val userDeferred = async { fetchUser() }
        val postsDeferred = async { fetchPosts() }
        
        val user = userDeferred.await()
        val posts = postsDeferred.await()
        println("$user and $posts")
    }
    println("Concurrent: ${time}ms") // ~1000ms
}
```

**Key Insight**: `async` starts coroutines concurrently, `await()` waits for results. Both tasks run in parallel, cutting total time in half.