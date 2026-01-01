---
type: "THEORY"
title: "Built-in Scopes in KMP"
---

### Android Scopes

| Scope | Tied To | Use When |
|-------|---------|----------|
| `viewModelScope` | ViewModel lifetime | Loading data for UI |
| `lifecycleScope` | Activity/Fragment lifetime | One-shot UI operations |
| `rememberCoroutineScope()` | Composable lifetime | Side effects in Compose |

### Example: ViewModel
```kotlin
class ProfileViewModel : ViewModel() {
    fun loadProfile(userId: String) {
        viewModelScope.launch {
            // Automatically cancelled when ViewModel is cleared
            val user = repository.fetchUser(userId)
            _uiState.value = ProfileState.Success(user)
        }
    }
}
```

### Example: Compose
```kotlin
@Composable
fun ProfileScreen() {
    val scope = rememberCoroutineScope()
    
    Button(onClick = {
        scope.launch {
            // Cancelled when ProfileScreen leaves composition
            performAsyncAction()
        }
    }) {
        Text("Do Something")
    }
}
```

### Creating Custom Scopes for KMP
```kotlin
class ProfilePresenter {
    private val scope = CoroutineScope(Dispatchers.Main + SupervisorJob())
    
    fun loadProfile(userId: String) {
        scope.launch {
            // Your async work
        }
    }
    
    fun onDestroy() {
        scope.cancel() // Clean up!
    }
}
```