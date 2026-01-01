---
type: "THEORY"
title: "Solution 1"
---


Here's a complete solution with personalized greeting:


**Result**: The same UI appears on both Android and iOS!

---



```kotlin
// composeApp/src/commonMain/kotlin/App.kt
import androidx.compose.foundation.layout.*
import androidx.compose.material3.*
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp

@Composable
fun App() {
    MaterialTheme {
        Column(
            modifier = Modifier.fillMaxSize(),
            horizontalAlignment = Alignment.CenterHorizontally,
            verticalArrangement = Arrangement.Center
        ) {
            Text(
                text = "Hello, Alice!",
                style = MaterialTheme.typography.headlineMedium
            )
            Spacer(modifier = Modifier.height(8.dp))
            Text(
                text = "My favorite color is Blue",
                color = MaterialTheme.colorScheme.primary
            )
        }
    }
}
```
