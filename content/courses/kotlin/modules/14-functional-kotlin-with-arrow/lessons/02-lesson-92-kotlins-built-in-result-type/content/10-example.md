---
type: "EXAMPLE"
title: "Real-World Example: API Calls"
---


Using Result for HTTP operations:



```kotlin
data class User(val id: Long, val name: String, val email: String)

class UserService(private val httpClient: HttpClient) {

    fun getUser(id: Long): Result<User> = runCatching {
        val response = httpClient.get("https://api.example.com/users/$id")
        require(response.status == 200) {
            "HTTP ${response.status}: ${response.body}"
        }
        Json.decodeFromString<User>(response.body)
    }

    fun updateUser(user: User): Result<User> = runCatching {
        val response = httpClient.put("https://api.example.com/users/${user.id}") {
            setBody(Json.encodeToString(user))
        }
        require(response.status in 200..299) {
            "Update failed: ${response.body}"
        }
        user
    }
}

// Usage
fun displayUser(id: Long) {
    userService.getUser(id).fold(
        onSuccess = { user ->
            showUserProfile(user)
        },
        onFailure = { error ->
            when (error) {
                is java.net.UnknownHostException -> showOfflineMessage()
                is java.net.SocketTimeoutException -> showTimeoutMessage()
                else -> showGenericError(error.message)
            }
        }
    )
}
```
