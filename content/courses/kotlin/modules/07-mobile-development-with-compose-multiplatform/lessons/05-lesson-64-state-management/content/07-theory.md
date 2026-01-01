---
type: "THEORY"
title: "ViewModel Integration"
---


### Why ViewModel?

**ViewModel** survives configuration changes and manages UI-related data:


### Setup

Add dependencies in `build.gradle.kts`:


In `gradle/libs.versions.toml`:


### Creating a ViewModel


### Using ViewModel in Composable


---



```kotlin
import androidx.lifecycle.viewmodel.compose.viewModel
import androidx.compose.runtime.collectAsState

@Composable
fun TodoScreen(
    viewModel: TodoViewModel = viewModel()
) {
    val uiState by viewModel.uiState.collectAsState()

    Column(modifier = Modifier.fillMaxSize()) {
        if (uiState.isLoading) {
            CircularProgressIndicator()
        }

        uiState.errorMessage?.let { error ->
            Text("Error: $error", color = Color.Red)
        }

        LazyColumn {
            items(uiState.todos.size) { index ->
                TodoItem(
                    todo = uiState.todos[index],
                    onDelete = { viewModel.removeTodo(index) }
                )
            }
        }

        var newTodo by remember { mutableStateOf("") }
        Row {
            TextField(
                value = newTodo,
                onValueChange = { newTodo = it }
            )
            Button(onClick = {
                viewModel.addTodo(newTodo)
                newTodo = ""
            }) {
                Text("Add")
            }
        }
    }
}

@Composable
fun TodoItem(todo: String, onDelete: () -> Unit) {
    Row(
        modifier = Modifier
            .fillMaxWidth()
            .padding(16.dp),
        horizontalArrangement = Arrangement.SpaceBetween
    ) {
        Text(todo)
        IconButton(onClick = onDelete) {
            Icon(Icons.Default.Delete, contentDescription = "Delete")
        }
    }
}
```
