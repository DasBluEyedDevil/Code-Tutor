---
type: "EXAMPLE"
title: "Complete Example: Loading a Profile Screen"
---

Here's a real-world pattern for loading a screen with multiple data sources:

```kotlin
import kotlinx.coroutines.*

data class User(val id: String, val name: String)
data class Post(val id: String, val title: String)
data class ProfileScreenState(
    val user: User? = null,
    val posts: List<Post> = emptyList(),
    val isLoading: Boolean = true,
    val error: String? = null
)

class ProfileViewModel {
    // Use SupervisorJob so failures don't cancel the scope
    private val scope = CoroutineScope(Dispatchers.Main + SupervisorJob())
    
    private val _state = MutableStateFlow(ProfileScreenState())
    val state: StateFlow<ProfileScreenState> = _state.asStateFlow()
    
    fun loadProfile(userId: String) {
        scope.launch {
            _state.value = _state.value.copy(isLoading = true, error = null)
            
            try {
                // Load user and posts in parallel
                coroutineScope {
                    val userDeferred = async { fetchUser(userId) }
                    val postsDeferred = async { fetchPosts(userId) }
                    
                    _state.value = _state.value.copy(
                        user = userDeferred.await(),
                        posts = postsDeferred.await(),
                        isLoading = false
                    )
                }
            } catch (e: Exception) {
                _state.value = _state.value.copy(
                    isLoading = false,
                    error = e.message ?: "Unknown error"
                )
            }
        }
    }
    
    fun onCleared() {
        scope.cancel() // Clean up when ViewModel is destroyed
    }
    
    private suspend fun fetchUser(id: String): User {
        delay(1000) // Simulate network
        return User(id, "John Doe")
    }
    
    private suspend fun fetchPosts(userId: String): List<Post> {
        delay(800)
        return listOf(
            Post("1", "First Post"),
            Post("2", "Second Post")
        )
    }
}
```
