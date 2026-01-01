---
type: "THEORY"
title: "Spacer"
---


Create fixed spacing between composables:


---



```kotlin
@Composable
fun SpacerExamples() {
    Column {
        Text("First")
        Spacer(modifier = Modifier.height(16.dp))
        Text("Second")
    }

    Row {
        Text("Left")
        Spacer(modifier = Modifier.width(24.dp))
        Text("Right")
    }

    // Fill available space
    Row(modifier = Modifier.fillMaxWidth()) {
        Text("Left")
        Spacer(modifier = Modifier.weight(1f))  // Takes all remaining space
        Text("Right")
    }
}
```
