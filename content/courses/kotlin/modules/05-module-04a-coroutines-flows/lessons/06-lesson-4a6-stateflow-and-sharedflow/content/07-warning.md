---
type: "WARNING"
title: "Common Mistakes"
---

### Mistake 1: Not exposing read-only Flow
```kotlin
// ❌ Exposes mutable flow
class ViewModel {
    val state = MutableStateFlow(State())
}

// ✅ Expose read-only, keep mutable private
class ViewModel {
    private val _state = MutableStateFlow(State())
    val state: StateFlow<State> = _state.asStateFlow()
}
```

### Mistake 2: Using StateFlow for events
```kotlin
// ❌ Wrong - events can be missed or replayed
val navigationEvent = MutableStateFlow<NavEvent?>(null)

// ✅ Right - use SharedFlow for one-shot events
val navigationEvent = MutableSharedFlow<NavEvent>()
```

### Mistake 3: Forgetting distinctUntilChanged is built-in
```kotlin
// ❌ Unnecessary - StateFlow already does this
stateFlow.distinctUntilChanged().collect { }

// ✅ Just collect directly
stateFlow.collect { }
```

### Mistake 4: Creating StateFlow in Composable
```kotlin
// ❌ Wrong - creates new flow on every recomposition
@Composable
fun Screen() {
    val state = MutableStateFlow(State()) // Don't do this!
}

// ✅ Right - state flows belong in ViewModel
@Composable
fun Screen(viewModel: ViewModel) {
    val state by viewModel.state.collectAsState()
}
```