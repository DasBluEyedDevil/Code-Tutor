---
type: "THEORY"
title: "Solution: API Result"
---



---



```kotlin
sealed class ApiResult<out T> {
    data class Success<T>(val data: T) : ApiResult<T>()
    data class Error(val code: Int, val message: String) : ApiResult<Nothing>()
    data object Loading : ApiResult<Nothing>()
}

data class User(val id: Int, val name: String, val email: String)

fun fetchUser(userId: Int): ApiResult<User> {
    return when {
        userId <= 0 -> ApiResult.Error(400, "Invalid user ID")
        userId == 999 -> ApiResult.Loading
        else -> ApiResult.Success(User(userId, "User $userId", "user$userId@example.com"))
    }
}

fun <T> handleResult(result: ApiResult<T>, onSuccess: (T) -> Unit) {
    when (result) {
        is ApiResult.Success -> {
            println("✅ Success!")
            onSuccess(result.data)
        }
        is ApiResult.Error -> {
            println("❌ Error ${result.code}: ${result.message}")
        }
        ApiResult.Loading -> {
            println("⏳ Loading...")
        }
    }
}

fun main() {
    println("=== Fetch User 1 ===")
    val result1 = fetchUser(1)
    handleResult(result1) { user ->
        println("User: ${user.name} (${user.email})")
    }

    println("\n=== Fetch Invalid User ===")
    val result2 = fetchUser(-1)
    handleResult(result2) { user ->
        println("User: ${user.name}")
    }

    println("\n=== Fetch Loading State ===")
    val result3 = fetchUser(999)
    handleResult(result3) { user ->
        println("User: ${user.name}")
    }

    // Using when expression directly
    println("\n=== Direct when expression ===")
    val message = when (val result = fetchUser(5)) {
        is ApiResult.Success -> "Loaded: ${result.data.name}"
        is ApiResult.Error -> "Failed: ${result.message}"
        ApiResult.Loading -> "Please wait..."
    }
    println(message)
}
```
