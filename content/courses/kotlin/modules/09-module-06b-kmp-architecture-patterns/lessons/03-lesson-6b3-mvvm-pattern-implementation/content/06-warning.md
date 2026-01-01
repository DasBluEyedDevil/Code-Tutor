---
type: "WARNING"
title: "Common MVVM Mistakes"
---

### Mistake 1: ViewModel Holding View References

```kotlin
// ❌ Never hold View references
class ViewModel(private val textView: TextView) { }

// ✅ Expose state, let View observe
class ViewModel {
    val text: StateFlow<String>
}
```

### Mistake 2: Business Logic in View

```kotlin
// ❌ Logic in Composable
@Composable
fun Screen() {
    val discount = if (user.isPremium) price * 0.2 else 0.0
}

// ✅ Logic in ViewModel
class ViewModel {
    val discount: StateFlow<Double> // Calculated in ViewModel
}
```

### Mistake 3: Not Handling Loading/Error States

```kotlin
// ❌ Only happy path
data class State(val items: List<Item>)

// ✅ Complete state
data class State(
    val items: List<Item> = emptyList(),
    val isLoading: Boolean = true,
    val error: String? = null
)
```

### Mistake 4: Updating State from Multiple Places

```kotlin
// ❌ State updates scattered
repository.getItems().collect { _items.value = it }
repository.getUser().collect { _user.value = it }

// ✅ Combine flows, single state update
combine(repository.getItems(), repository.getUser()) { items, user ->
    State(items = items, user = user)
}.collect { _state.value = it }
```