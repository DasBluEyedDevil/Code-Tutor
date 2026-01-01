---
type: "THEORY"
title: "State Management on iOS"
---


### Cross-Platform State

The same state management code works on both Android and iOS! Your `remember`, `rememberSaveable`, and ViewModel patterns all work identically.

### Platform Differences

| Feature | Android | iOS |
|---------|---------|-----|
| **Configuration Changes** | Screen rotation triggers | Less common (iOS handles rotation differently) |
| **App Lifecycle** | Activity lifecycle | UIViewController lifecycle |
| **State Restoration** | System-managed | System-managed |

### Running State Examples on iOS

1. Build and run on iOS Simulator
2. Try rotating the device (Cmd + Left/Right Arrow)
3. Observe that `rememberSaveable` state is preserved
4. Test form inputs across configuration changes

### ViewModel on iOS

With Compose Multiplatform, ViewModels work on iOS too! The state survives configuration changes on both platforms.

```kotlin
// In commonMain - works on both platforms!
class CounterViewModel : ViewModel() {
    private val _count = MutableStateFlow(0)
    val count: StateFlow<Int> = _count.asStateFlow()
    
    fun increment() {
        _count.value++
    }
}

@Composable
fun CounterScreen(viewModel: CounterViewModel = viewModel()) {
    val count by viewModel.count.collectAsState()
    
    // This UI and state work on Android AND iOS!
    Column {
        Text("Count: $count")
        Button(onClick = { viewModel.increment() }) {
            Text("+1")
        }
    }
}
```

---

