---
type: "WARNING"
title: "Flow Considerations"
---

### Dispatcher Matters

```kotlin
// ✅ Correct - use IO or Default
.mapToList(Dispatchers.IO)

// ❌ Wrong - blocks Main thread
.mapToList(Dispatchers.Main)
```

### Hot vs Cold

SQLDelight flows are **cold**:
- No work happens until collected
- Each collector gets its own subscription
- Consider using `stateIn` or `shareIn` for multiple collectors

```kotlin
// Cold - each collector causes new subscription
val notesFlow: Flow<List<Note>> = repository.observeAllNotes()

// Hot - single subscription shared
val notesState: StateFlow<List<Note>> = repository.observeAllNotes()
    .stateIn(
        scope = viewModelScope,
        started = SharingStarted.WhileSubscribed(5000),
        initialValue = emptyList()
    )
```

### Memory Leaks

Always collect in appropriate scope:
```kotlin
// ✅ Automatic cancellation
viewModelScope.launch {
    repository.observeAllNotes().collect { ... }
}

// ❌ Memory leak - never cancelled
GlobalScope.launch {
    repository.observeAllNotes().collect { ... }
}
```