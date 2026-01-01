---
type: "THEORY"
title: "Repository Pattern"
---



---



```kotlin
sealed class Result<out T> {
    data class Success<T>(val data: T) : Result<T>()
    data class Error(val message: String, val exception: Exception? = null) : Result<Nothing>()
    object Loading : Result<Nothing>()
}

class UserRepository(private val apiService: ApiService) {
    suspend fun getUsers(): Result<List<User>> {
        return try {
            val users = apiService.getUsers()
            Result.Success(users)
        } catch (e: Exception) {
            Result.Error("Failed to fetch users: ${e.message}", e)
        }
    }

    suspend fun getUser(userId: Int): Result<User> {
        return try {
            val user = apiService.getUser(userId)
            Result.Success(user)
        } catch (e: Exception) {
            Result.Error("Failed to fetch user: ${e.message}", e)
        }
    }

    suspend fun createUser(name: String, email: String): Result<User> {
        return try {
            val request = CreateUserRequest(name, email)
            val user = apiService.createUser(request)
            Result.Success(user)
        } catch (e: Exception) {
            Result.Error("Failed to create user: ${e.message}", e)
        }
    }
}
```
