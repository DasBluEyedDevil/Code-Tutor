---
type: "THEORY"
title: "Flows in CommonMain"
---

Flows work identically in shared code:

```kotlin
// In commonMain/kotlin/data/Repository.kt
class UserRepository {
    private val _users = MutableStateFlow<List<User>>(emptyList())
    val users: StateFlow<List<User>> = _users.asStateFlow()
    
    fun observeUser(id: String): Flow<User?> = flow {
        while (true) {
            emit(fetchUser(id))
            delay(30_000) // Refresh every 30 seconds
        }
    }
    
    private suspend fun fetchUser(id: String): User? {
        // API call
    }
}
```

### Shared ViewModel Pattern
```kotlin
// In commonMain
open class BaseViewModel {
    protected val scope = CoroutineScope(mainDispatcher + SupervisorJob())
    
    fun onCleared() = scope.cancel()
}

class ProfileViewModel(private val repository: UserRepository) : BaseViewModel() {
    private val _uiState = MutableStateFlow<ProfileUiState>(ProfileUiState.Loading)
    val uiState: StateFlow<ProfileUiState> = _uiState.asStateFlow()
    
    fun loadProfile(userId: String) {
        scope.launch {
            repository.observeUser(userId)
                .catch { e -> _uiState.value = ProfileUiState.Error(e.message) }
                .collect { user ->
                    _uiState.value = user?.let { 
                        ProfileUiState.Success(it) 
                    } ?: ProfileUiState.Error("User not found")
                }
        }
    }
}
```