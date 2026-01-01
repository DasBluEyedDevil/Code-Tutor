---
type: "THEORY"
title: "Recomposition"
---


### What is Recomposition?

**Recomposition** is when Compose re-executes composable functions to update the UI after state changes.


**Flow**:
1. User clicks button
2. `count` increases
3. Compose detects state change
4. Recomposes `Text("Count: $count")`
5. UI updates with new value

### Smart Recomposition

Compose only recomposes what's necessary:


**Optimization**: Only the `Text` displaying `count` recomposes, not the entire `Column`.

---



```kotlin
@Composable
fun SmartRecomposition() {
    var count by remember { mutableStateOf(0) }

    Column {
        Text("Static text")  // ❌ Never recomposes

        Text("Count: $count")  // ✅ Recomposes when count changes

        Button(onClick = { count++ }) {
            Text("Increment")  // ❌ Never recomposes
        }
    }
}
```
