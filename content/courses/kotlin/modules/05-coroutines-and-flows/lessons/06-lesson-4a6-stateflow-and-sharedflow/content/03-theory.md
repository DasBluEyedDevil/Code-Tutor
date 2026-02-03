---
type: "THEORY"
title: "MutableStateFlow in Detail"
---

### Creating and Updating
```kotlin
val state = MutableStateFlow<UiState>(UiState.Loading)

// Direct assignment
state.value = UiState.Success(data)

// Update with function (atomic)
state.update { currentState ->
    when (currentState) {
        is UiState.Success -> currentState.copy(count = currentState.count + 1)
        else -> currentState
    }
}

// Compare and set (atomic)
state.compareAndSet(
    expect = UiState.Loading,
    update = UiState.Success(data)
)
```

### Equality Matters
```kotlin
data class User(val name: String, val age: Int)

val userFlow = MutableStateFlow(User("Alice", 25))

userFlow.value = User("Alice", 25) // NOT emitted (same value)
userFlow.value = User("Alice", 26) // Emitted (different value)
```

### Common Patterns
```kotlin
// UI State Pattern
sealed class UiState {
    data object Loading : UiState()
    data class Success(val data: List<Item>) : UiState()
    data class Error(val message: String) : UiState()
}

class ItemsViewModel {
    private val _uiState = MutableStateFlow<UiState>(UiState.Loading)
    val uiState: StateFlow<UiState> = _uiState.asStateFlow()
    
    fun loadItems() {
        viewModelScope.launch {
            _uiState.value = UiState.Loading
            try {
                val items = repository.getItems()
                _uiState.value = UiState.Success(items)
            } catch (e: Exception) {
                _uiState.value = UiState.Error(e.message ?: "Unknown error")
            }
        }
    }
}
```