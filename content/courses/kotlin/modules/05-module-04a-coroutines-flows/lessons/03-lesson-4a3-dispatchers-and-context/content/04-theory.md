---
type: "THEORY"
title: "withContext - Switching Dispatchers"
---

Use `withContext` to switch dispatchers within a coroutine:

```kotlin
suspend fun loadAndProcess(): ProcessedData {
    // Start on whatever dispatcher we were called from
    
    val rawData = withContext(Dispatchers.IO) {
        // Switch to IO for network call
        api.fetchData()
    }
    
    val processed = withContext(Dispatchers.Default) {
        // Switch to Default for CPU work
        processData(rawData)
    }
    
    return processed // Back to original dispatcher
}

// Usage from UI
viewModelScope.launch { // On Main
    val result = loadAndProcess() // Switches internally
    _uiState.value = result        // Back on Main
}
```

### Why withContext Instead of launch?

```kotlin
// ❌ Creates unnecessary coroutine, doesn't wait
launch(Dispatchers.IO) {
    val data = fetchData()
    // How do I get data back?
}

// ✅ Switches context, returns result
val data = withContext(Dispatchers.IO) {
    fetchData()
}
```

`withContext` suspends until the block completes and returns the result.