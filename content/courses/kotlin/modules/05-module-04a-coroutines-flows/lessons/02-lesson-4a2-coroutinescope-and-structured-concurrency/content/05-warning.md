---
type: "WARNING"
title: "Never Use GlobalScope"
---

```kotlin
// ❌ NEVER do this
GlobalScope.launch {
    // This coroutine lives forever (until app is killed)
    // Memory leaks, lifecycle issues, hard to test
}
```

**Why GlobalScope is bad:**
1. **No automatic cancellation** - Coroutines run until completion or app death
2. **Hard to test** - Coroutines outlive tests, causing flaky results
3. **Can't track running work** - No way to know what's still running
4. **Violates structured concurrency** - No parent-child relationship
5. **Resource leaks** - Database connections, network calls continue indefinitely

**Always use a proper scope:**
```kotlin
// ✅ In a ViewModel
class MyViewModel : ViewModel() {
    fun loadData() {
        viewModelScope.launch {
            // Automatically cancelled when ViewModel is cleared
        }
    }
}

// ✅ In a Composable
@Composable
fun MyScreen() {
    val scope = rememberCoroutineScope()
    Button(onClick = {
        scope.launch {
            // Cancelled when composable leaves
        }
    }) {
        Text("Load")
    }
}

// ✅ Custom scope with cleanup
class MyPresenter {
    private val scope = CoroutineScope(Dispatchers.Default + SupervisorJob())
    
    fun cleanup() = scope.cancel()
}
```