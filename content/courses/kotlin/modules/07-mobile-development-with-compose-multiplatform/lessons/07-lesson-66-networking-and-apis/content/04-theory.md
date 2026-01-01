---
type: "THEORY"
title: "Kotlin Serialization"
---


### Data Models


---



```kotlin
import kotlinx.serialization.SerialName
import kotlinx.serialization.Serializable

@Serializable
data class User(
    val id: Int,
    val name: String,
    val email: String,
    @SerialName("avatar_url")  // Map JSON field to Kotlin property
    val avatarUrl: String? = null
)

@Serializable
data class Post(
    val id: Int,
    val title: String,
    val body: String,
    @SerialName("user_id")
    val userId: Int
)

@Serializable
data class ApiResponse<T>(
    val success: Boolean,
    val data: T? = null,
    val message: String? = null
)
```
