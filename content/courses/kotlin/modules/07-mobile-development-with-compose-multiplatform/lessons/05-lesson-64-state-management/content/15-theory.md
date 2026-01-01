---
type: "THEORY"
title: "Solution 3"
---



---



```kotlin
import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Clear
import androidx.compose.material.icons.filled.Search
import androidx.compose.material3.*
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp

@Stable
class SearchState(
    initialQuery: String = "",
    private val allItems: List<String>
) {
    var query by mutableStateOf(initialQuery)
        private set

    var suggestions by mutableStateOf<List<String>>(emptyList())
        private set

    var selectedItems by mutableStateOf<List<String>>(emptyList())
        private set

    fun updateQuery(newQuery: String) {
        query = newQuery
        suggestions = if (newQuery.isEmpty()) {
            emptyList()
        } else {
            allItems.filter { it.contains(newQuery, ignoreCase = true) }
                .take(5)
        }
    }

    fun selectItem(item: String) {
        if (item !in selectedItems) {
            selectedItems = selectedItems + item
        }
        query = ""
        suggestions = emptyList()
    }

    fun removeItem(item: String) {
        selectedItems = selectedItems - item
    }

    fun clearAll() {
        selectedItems = emptyList()
        query = ""
        suggestions = emptyList()
    }
}

@Composable
fun rememberSearchState(
    allItems: List<String>
): SearchState {
    return remember { SearchState(allItems = allItems) }
}

@Composable
fun SearchScreen() {
    val sampleItems = remember {
        listOf(
            "Apple", "Banana", "Cherry", "Date", "Elderberry",
            "Fig", "Grape", "Honeydew", "Kiwi", "Lemon",
            "Mango", "Orange", "Papaya", "Quince", "Raspberry"
        )
    }

    val searchState = rememberSearchState(allItems = sampleItems)

    Column(
        modifier = Modifier
            .fillMaxSize()
            .padding(16.dp)
    ) {
        // Search field
        OutlinedTextField(
            value = searchState.query,
            onValueChange = { searchState.updateQuery(it) },
            label = { Text("Search") },
            leadingIcon = {
                Icon(Icons.Default.Search, contentDescription = null)
            },
            trailingIcon = {
                if (searchState.query.isNotEmpty()) {
                    IconButton(onClick = { searchState.updateQuery("") }) {
                        Icon(Icons.Default.Clear, contentDescription = "Clear")
                    }
                }
            },
            modifier = Modifier.fillMaxWidth()
        )

        // Suggestions
        if (searchState.suggestions.isNotEmpty()) {
            Card(
                modifier = Modifier
                    .fillMaxWidth()
                    .padding(top = 8.dp)
            ) {
                LazyColumn {
                    items(searchState.suggestions) { suggestion ->
                        Text(
                            text = suggestion,
                            modifier = Modifier
                                .fillMaxWidth()
                                .clickable { searchState.selectItem(suggestion) }
                                .padding(16.dp)
                        )
                    }
                }
            }
        }

        Spacer(modifier = Modifier.height(16.dp))

        // Selected items
        Row(
            modifier = Modifier.fillMaxWidth(),
            horizontalArrangement = Arrangement.SpaceBetween,
            verticalAlignment = Alignment.CenterVertically
        ) {
            Text(
                "Selected Items (${searchState.selectedItems.size})",
                style = MaterialTheme.typography.titleMedium
            )

            if (searchState.selectedItems.isNotEmpty()) {
                TextButton(onClick = { searchState.clearAll() }) {
                    Text("Clear All")
                }
            }
        }

        LazyColumn {
            items(searchState.selectedItems) { item ->
                Card(
                    modifier = Modifier
                        .fillMaxWidth()
                        .padding(vertical = 4.dp)
                ) {
                    Row(
                        modifier = Modifier
                            .fillMaxWidth()
                            .padding(16.dp),
                        horizontalArrangement = Arrangement.SpaceBetween,
                        verticalAlignment = Alignment.CenterVertically
                    ) {
                        Text(item)
                        IconButton(onClick = { searchState.removeItem(item) }) {
                            Icon(Icons.Default.Clear, contentDescription = "Remove")
                        }
                    }
                }
            }
        }
    }
}

@Preview(showBackground = true)
@Composable
fun SearchScreenPreview() {
    MaterialTheme {
        SearchScreen()
    }
}
```
