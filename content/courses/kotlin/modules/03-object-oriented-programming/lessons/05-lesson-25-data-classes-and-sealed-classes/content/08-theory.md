---
type: "THEORY"
title: "Sealed Classes for State Management"
---



---



```kotlin
sealed class UiState {
    data object Loading : UiState()
    data class Success(val items: List<String>) : UiState()
    data class Error(val message: String) : UiState()
    data object Empty : UiState()
}

class ViewModel {
    private var state: UiState = UiState.Loading

    fun loadData() {
        state = UiState.Loading
        displayState()

        // Simulate loading
        Thread.sleep(1000)

        val items = listOf("Item 1", "Item 2", "Item 3")
        state = if (items.isNotEmpty()) {
            UiState.Success(items)
        } else {
            UiState.Empty
        }
        displayState()
    }

    fun displayState() {
        when (state) {
            is UiState.Loading -> println("⏳ Loading...")
            is UiState.Success -> {
                val items = (state as UiState.Success).items
                println("✅ Loaded ${items.size} items: $items")
            }
            is UiState.Error -> {
                val message = (state as UiState.Error).message
                println("❌ Error: $message")
            }
            UiState.Empty -> println("📭 No items found")
        }
    }
}

fun main() {
    val viewModel = ViewModel()
    viewModel.loadData()
}
```
