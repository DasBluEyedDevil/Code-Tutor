---
type: "WARNING"
title: "Recomposition Performance Pitfalls"
---

**Unnecessary recompositions kill performance**. Every state change triggers recomposition of all composables reading that state—if you update state 60 times per second, expensive composables recompose 60 times.

**Don't update state in composition**:
```kotlin
@Composable
fun BadCounter() {
    var count by remember { mutableStateOf(0) }
    count++  // INFINITE LOOP - updates state during composition!
    Text("Count: $count")
}
```

This causes infinite recomposition: composition reads state, modifies state, triggers recomposition, repeat forever.

**Use derivedStateOf for computed values** that depend on multiple states:
```kotlin
// BAD - recomposes whenever firstName OR lastName changes
val fullName = "${firstName.value} ${lastName.value}"

// GOOD - recomposes only when the result changes
val fullName by remember {
    derivedStateOf { "${firstName.value} ${lastName.value}" }
}
```

**Avoid lambda allocations in frequently-recomposing code**:
```kotlin
// BAD - creates new lambda on every recomposition
Button(onClick = { viewModel.increment(count) })

// GOOD - lambda doesn't capture recomposing state
Button(onClick = viewModel::increment)
```

Profile with Layout Inspector to identify recomposition hotspots before optimizing.
