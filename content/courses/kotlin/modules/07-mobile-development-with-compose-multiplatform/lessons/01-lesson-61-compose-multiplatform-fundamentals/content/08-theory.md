---
type: "THEORY"
title: "Shared Composable Example"
---


### Your First Cross-Platform Composable

The `App.kt` in `commonMain` contains your shared UI. Here's a simple counter that runs on both Android and iOS:


### How It Works

1. **`@Composable`**: Marks the function as a UI component
2. **`remember`**: Preserves state across recompositions
3. **`mutableStateOf`**: Creates reactive state that triggers UI updates
4. **`Column`**: Vertical layout container
5. **`Text` and `Button`**: Basic UI components

This EXACT code runs on:
- Android phones and tablets
- iPhones and iPads
- Desktop (Windows, macOS, Linux)
- Web browsers (experimental)

---



```kotlin
// composeApp/src/commonMain/kotlin/App.kt
import androidx.compose.foundation.layout.*
import androidx.compose.material3.*
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp

@Composable
fun App() {
    MaterialTheme {
        var count by remember { mutableStateOf(0) }
        
        Column(
            modifier = Modifier.fillMaxSize(),
            horizontalAlignment = Alignment.CenterHorizontally,
            verticalArrangement = Arrangement.Center
        ) {
            Text(
                text = "Count: $count",
                style = MaterialTheme.typography.headlineMedium
            )
            
            Spacer(modifier = Modifier.height(16.dp))
            
            Row(horizontalArrangement = Arrangement.spacedBy(8.dp)) {
                Button(onClick = { count-- }) {
                    Text("-")
                }
                Button(onClick = { count++ }) {
                    Text("+")
                }
            }
        }
    }
}
```
