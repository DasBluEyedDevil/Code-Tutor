---
type: "EXAMPLE"
title: "Complete Example: Search with Debounce"
---

A common pattern for search-as-you-type with debouncing:

```kotlin
import kotlinx.coroutines.*
import kotlinx.coroutines.flow.*

class SearchViewModel {
    private val _searchQuery = MutableStateFlow("")
    
    val searchResults: Flow<List<String>> = _searchQuery
        .debounce(300) // Wait 300ms after last input
        .filter { it.length >= 2 } // Min 2 characters
        .distinctUntilChanged() // Ignore if same as before
        .flatMapLatest { query ->
            flow {
                emit(emptyList<String>()) // Clear while loading
                emit(searchApi(query))     // Emit results
            }
        }
        .catch { e ->
            emit(listOf("Error: ${e.message}"))
        }
    
    fun onSearchQueryChanged(query: String) {
        _searchQuery.value = query
    }
    
    private suspend fun searchApi(query: String): List<String> {
        delay(500) // Simulate network
        return listOf("$query result 1", "$query result 2", "$query result 3")
    }
}

fun main() = runBlocking {
    val viewModel = SearchViewModel()
    
    launch {
        viewModel.searchResults.collect { results ->
            println("Results: $results")
        }
    }
    
    // Simulate user typing
    viewModel.onSearchQueryChanged("k")
    delay(100)
    viewModel.onSearchQueryChanged("ko")
    delay(100)
    viewModel.onSearchQueryChanged("kot")
    delay(100)
    viewModel.onSearchQueryChanged("kotl")
    delay(100)
    viewModel.onSearchQueryChanged("kotli")
    delay(100)
    viewModel.onSearchQueryChanged("kotlin")
    
    delay(1500) // Wait for results
}
```
