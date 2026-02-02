---
type: "WARNING"
title: "Lifecycle Considerations"
---

### Memory Leaks

```kotlin
// ❌ Scope lives forever if onCleared not called
class MyViewModel {
    private val scope = CoroutineScope(Dispatchers.Main + SupervisorJob())
}

// ✅ Always provide cleanup mechanism
class MyViewModel {
    private val scope = CoroutineScope(Dispatchers.Main + SupervisorJob())
    
    fun onCleared() {
        scope.cancel() // MUST be called!
    }
}
```

### State Survival on Configuration Change

Android rotates screen → Activity recreated → ViewModel survives
iOS has no equivalent configuration changes

```kotlin
// On Android, use wrapper ViewModel for state preservation
class AndroidWrapper(
    savedStateHandle: SavedStateHandle
) : ViewModel() {
    // SavedStateHandle survives process death
    private val userId: String = savedStateHandle["userId"] ?: ""
}
```

### Threading

```kotlin
// Always update state on Main dispatcher
_state.update { it.copy(...) } // OK if scope uses Dispatchers.Main

// Heavy work should switch dispatchers
scope.launch {
    val result = withContext(Dispatchers.Default) {
        heavyComputation()
    }
    _state.update { it.copy(result = result) }
}
```