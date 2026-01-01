---
type: "THEORY"
title: "Coroutine Builders"
---


Coroutine builders create and launch coroutines.

### `runBlocking` - Bridge to the Blocking World

`runBlocking` starts a coroutine and blocks the current thread until it completes:


**When to use**: Main functions, tests. Avoid in production code (blocks thread).

### `launch` - Fire and Forget

`launch` starts a coroutine that runs in the background:


**Returns**: `Job` - handle to manage the coroutine


### `async` - Return a Result

`async` is like `launch` but returns a result:


**Returns**: `Deferred<T>` - a future result

### Concurrent Execution with `async`


---



```kotlin
suspend fun fetchUser(id: Int): String {
    delay(1000)
    return "User $id"
}

suspend fun fetchPosts(userId: Int): List<String> {
    delay(1000)
    return listOf("Post 1", "Post 2")
}

fun main() = runBlocking {
    val startTime = System.currentTimeMillis()

    // Sequential (slow)
    val user = fetchUser(1)
    val posts = fetchPosts(1)
    println("Sequential time: ${System.currentTimeMillis() - startTime}ms")
    // ~2000ms

    // Concurrent (fast)
    val startTime2 = System.currentTimeMillis()
    val userDeferred = async { fetchUser(1) }
    val postsDeferred = async { fetchPosts(1) }

    val user2 = userDeferred.await()
    val posts2 = postsDeferred.await()
    println("Concurrent time: ${System.currentTimeMillis() - startTime2}ms")
    // ~1000ms
}
```
