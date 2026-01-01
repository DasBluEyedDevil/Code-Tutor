---
type: "EXAMPLE"
title: "StateFlow for UI State"
---

StateFlow maintains a current value that's immediately available to new collectors. Use MutableStateFlow privately and expose read-only StateFlow publicly. The update() function provides atomic state modifications, and copy() enables immutable state updates.

```kotlin
import kotlinx.coroutines.*
import kotlinx.coroutines.flow.*

// UI State pattern - commonly used in Android MVVM
data class UiState(
    val isLoading: Boolean = false,
    val data: List<String> = emptyList(),
    val error: String? = null
)

class ViewModel {
    // Private mutable state
    private val _uiState = MutableStateFlow(UiState())
    
    // Public read-only state
    val uiState: StateFlow<UiState> = _uiState.asStateFlow()
    
    suspend fun loadData() {
        // Show loading
        _uiState.value = _uiState.value.copy(isLoading = true)
        
        try {
            delay(1000) // Simulate network call
            val data = listOf("Item 1", "Item 2", "Item 3")
            
            // Update with data
            _uiState.value = _uiState.value.copy(
                isLoading = false,
                data = data,
                error = null
            )
        } catch (e: Exception) {
            _uiState.value = _uiState.value.copy(
                isLoading = false,
                error = e.message
            )
        }
    }
    
    // Alternative: use update for atomic updates
    fun addItem(item: String) {
        _uiState.update { currentState ->
            currentState.copy(data = currentState.data + item)
        }
    }
}

fun main() = runBlocking {
    val viewModel = ViewModel()
    
    // Collector (simulating UI)
    val job = launch {
        viewModel.uiState.collect { state ->
            when {
                state.isLoading -> println("Loading...")
                state.error != null -> println("Error: ${state.error}")
                else -> println("Data: ${state.data}")
            }
        }
    }
    
    delay(100)
    viewModel.loadData()
    delay(100)
    viewModel.addItem("New Item")
    delay(100)
    
    job.cancel()
}
// Output:
// Data: []
// Loading...
// Data: [Item 1, Item 2, Item 3]
// Data: [Item 1, Item 2, Item 3, New Item]
```
