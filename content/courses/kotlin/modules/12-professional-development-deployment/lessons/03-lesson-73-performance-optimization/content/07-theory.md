---
type: "THEORY"
title: "Jetpack Compose Optimization"
---


### Recomposition Basics

**What is Recomposition?**

When state changes, Compose re-runs composables to update UI.

**Problem**: Unnecessary recompositions = poor performance

**Example**:

### Optimization 1: Stable Parameters

❌ **Bad** (Recomposes unnecessarily):

✅ **Good** (Only necessary recompositions):

### Optimization 2: derivedStateOf

❌ **Bad** (Recalculates on every recomposition):

✅ **Good** (Only recalculates when products change):

### Optimization 3: LazyColumn Keys

❌ **Bad** (Entire list recomposes):

✅ **Good** (Only changed items recompose):

### Optimization 4: Immutable Collections


✅ **Good** (Compose knows it's immutable):

### Measuring Recompositions


---



```kotlin
@Composable
fun LogCompositions(tag: String) {
    val ref = remember { Ref(0) }
    SideEffect {
        ref.value++
        Log.d("Recomposition", "$tag recomposed ${ref.value} times")
    }
}

class Ref(var value: Int)

@Composable
fun MyScreen() {
    LogCompositions("MyScreen")

    // Your content
}
```
