---
type: "EXAMPLE"
title: "Complete Example: Resilient API Client"
---

A production-ready pattern for handling API errors:

```kotlin
import kotlinx.coroutines.*

sealed class ApiResult<out T> {
    data class Success<T>(val data: T) : ApiResult<T>()
    data class Error(val message: String, val cause: Throwable? = null) : ApiResult<Nothing>()
    object Loading : ApiResult<Nothing>()
}

class ApiClient {
    private val scope = CoroutineScope(
        Dispatchers.IO + 
        SupervisorJob() +
        CoroutineExceptionHandler { _, e -> 
            println("Unhandled: ${e.message}")
        }
    )
    
    suspend fun <T> safeApiCall(
        call: suspend () -> T
    ): ApiResult<T> {
        return try {
            ApiResult.Success(call())
        } catch (e: CancellationException) {
            throw e // Don't catch cancellation!
        } catch (e: Exception) {
            ApiResult.Error(
                message = e.message ?: "Unknown error",
                cause = e
            )
        }
    }
    
    suspend fun fetchUser(id: String): ApiResult<User> = safeApiCall {
        // Simulate API call
        delay(1000)
        if (id == "error") throw Exception("User not found")
        User(id, "John Doe")
    }
    
    suspend fun fetchWithRetry(
        times: Int = 3,
        initialDelay: Long = 100,
        factor: Double = 2.0,
        call: suspend () -> ApiResult<User>
    ): ApiResult<User> {
        var currentDelay = initialDelay
        repeat(times - 1) {
            when (val result = call()) {
                is ApiResult.Success -> return result
                is ApiResult.Error -> {
                    delay(currentDelay)
                    currentDelay = (currentDelay * factor).toLong()
                }
                ApiResult.Loading -> { /* continue */ }
            }
        }
        return call() // Last attempt
    }
}

data class User(val id: String, val name: String)

fun main() = runBlocking {
    val client = ApiClient()
    
    when (val result = client.fetchUser("123")) {
        is ApiResult.Success -> println("User: ${result.data}")
        is ApiResult.Error -> println("Error: ${result.message}")
        ApiResult.Loading -> println("Loading...")
    }
}
```
