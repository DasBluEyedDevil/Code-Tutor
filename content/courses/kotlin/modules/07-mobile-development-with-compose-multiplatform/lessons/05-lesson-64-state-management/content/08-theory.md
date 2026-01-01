---
type: "THEORY"
title: "State Holders"
---


### Different State Holder Types


### When to Use Each

| State Type            | Use Case                              |
|-----------------------|---------------------------------------|
| `remember { mutableStateOf }` | Simple values (counter, toggle) |
| State object          | Related values (form fields)          |
| State holder class    | Complex logic + multiple values       |
| ViewModel             | Screen state, survives config changes |

---



```kotlin
// 1. Plain state (for simple values)
var count by remember { mutableStateOf(0) }

// 2. State object (for related state)
data class FormState(
    val email: String = "",
    val password: String = "",
    val isValid: Boolean = false
)

var formState by remember { mutableStateOf(FormState()) }

// 3. State holder class (for complex logic)
@Stable
class SearchState(
    initialQuery: String = ""
) {
    var query by mutableStateOf(initialQuery)
        private set

    var suggestions by mutableStateOf<List<String>>(emptyList())
        private set

    fun updateQuery(newQuery: String) {
        query = newQuery
        // Update suggestions based on query
        suggestions = getSuggestions(newQuery)
    }

    private fun getSuggestions(query: String): List<String> {
        // Logic to fetch suggestions
        return emptyList()
    }
}

@Composable
fun rememberSearchState() = remember { SearchState() }

// 4. ViewModel (for screen-level state)
class MyViewModel : ViewModel() {
    val uiState: StateFlow<UiState> = /* ... */
}
```
