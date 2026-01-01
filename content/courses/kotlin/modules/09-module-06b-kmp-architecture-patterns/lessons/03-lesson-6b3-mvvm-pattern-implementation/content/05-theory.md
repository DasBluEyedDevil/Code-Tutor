---
type: "THEORY"
title: "State Management Best Practices"
---

### 1. Single State Object

```kotlin
// ❌ Multiple state properties
class ViewModel {
    val isLoading = MutableStateFlow(false)
    val items = MutableStateFlow<List<Item>>(emptyList())
    val error = MutableStateFlow<String?>(null)
}

// ✅ Single state object
class ViewModel {
    private val _state = MutableStateFlow(UiState())
    val state: StateFlow<UiState> = _state.asStateFlow()
}

data class UiState(
    val isLoading: Boolean = false,
    val items: List<Item> = emptyList(),
    val error: String? = null
)
```

### 2. Immutable Updates

```kotlin
// ❌ Mutating state directly
state.value.items.add(newItem) // Won't trigger recomposition!

// ✅ Create new state
_state.update { currentState ->
    currentState.copy(items = currentState.items + newItem)
}
```

### 3. UI Models vs Domain Models

```kotlin
// Domain model (data layer)
data class Note(
    val id: String,
    val content: String,
    val createdAt: Long // Unix timestamp
)

// UI model (presentation layer)
data class NoteUiModel(
    val id: String,
    val content: String,
    val formattedDate: String // "Dec 31, 2025"
)
```

Keep domain models pure. Format for display in UI models.