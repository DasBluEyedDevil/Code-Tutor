---
type: "THEORY"
title: "MVVM vs MVI"
---

### When to Choose Each

| MVVM | MVI |
|------|-----|
| Simpler to implement | More predictable |
| Good for small-medium apps | Better for complex state |
| Multiple methods on ViewModel | Single Intent channel |
| State can be fragmented | Single source of truth |
| Faster initial development | Easier debugging |

### MVI Advantages

**1. Time-Travel Debugging**
```kotlin
// Every state change is logged
states.forEach { println(it) }
// State(items=[], loading=true)
// State(items=[...], loading=false)
// State(items=[...], loading=false, selected=Item1)
```

**2. Predictable State Transitions**
```kotlin
// Pure reducer - same input = same output
fun reduce(state: State, intent: Intent): State = when (intent) {
    is Intent.Load -> state.copy(loading = true)
    is Intent.Loaded -> state.copy(items = intent.items, loading = false)
}
```

**3. Easy Testing**
```kotlin
@Test
fun `Load intent should set loading to true`() {
    val result = reduce(State(), Intent.Load)
    assertTrue(result.loading)
}
```