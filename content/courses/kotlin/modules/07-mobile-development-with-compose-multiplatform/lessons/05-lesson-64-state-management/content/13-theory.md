---
type: "THEORY"
title: "Solution 2"
---



---



```kotlin
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.material3.*
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewmodel.compose.viewModel
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.asStateFlow

data class CounterUiState(
    val count: Int = 0,
    val history: List<Int> = emptyList()
)

class CounterViewModel : ViewModel() {
    private val _uiState = MutableStateFlow(CounterUiState())
    val uiState: StateFlow<CounterUiState> = _uiState.asStateFlow()

    fun increment() {
        val newCount = _uiState.value.count + 1
        updateState(newCount)
    }

    fun decrement() {
        val newCount = _uiState.value.count - 1
        updateState(newCount)
    }

    fun reset() {
        _uiState.value = CounterUiState(
            count = 0,
            history = _uiState.value.history
        )
    }

    private fun updateState(newCount: Int) {
        _uiState.value = _uiState.value.copy(
            count = newCount,
            history = (_uiState.value.history + newCount).takeLast(5)
        )
    }
}

@Composable
fun CounterScreen(
    viewModel: CounterViewModel = viewModel()
) {
    val uiState by viewModel.uiState.collectAsState()

    Column(
        modifier = Modifier
            .fillMaxSize()
            .padding(24.dp),
        horizontalAlignment = Alignment.CenterHorizontally,
        verticalArrangement = Arrangement.Center
    ) {
        Text(
            "Count: ${uiState.count}",
            style = MaterialTheme.typography.displayLarge
        )

        Spacer(modifier = Modifier.height(32.dp))

        Row(horizontalArrangement = Arrangement.spacedBy(16.dp)) {
            Button(onClick = { viewModel.decrement() }) {
                Text("-", style = MaterialTheme.typography.headlineMedium)
            }

            Button(onClick = { viewModel.reset() }) {
                Text("Reset")
            }

            Button(onClick = { viewModel.increment() }) {
                Text("+", style = MaterialTheme.typography.headlineMedium)
            }
        }

        Spacer(modifier = Modifier.height(48.dp))

        if (uiState.history.isNotEmpty()) {
            Text(
                "History (last 5):",
                style = MaterialTheme.typography.titleMedium
            )

            Spacer(modifier = Modifier.height(8.dp))

            LazyColumn {
                items(uiState.history) { value ->
                    Text(
                        "• $value",
                        style = MaterialTheme.typography.bodyLarge
                    )
                }
            }
        }
    }
}

@Preview(showBackground = true)
@Composable
fun CounterScreenPreview() {
    MaterialTheme {
        CounterScreen()
    }
}
```
