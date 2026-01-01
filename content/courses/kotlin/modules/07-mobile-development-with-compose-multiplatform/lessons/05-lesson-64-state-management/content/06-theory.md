---
type: "THEORY"
title: "State Hoisting"
---


### What is State Hoisting?

**State hoisting** means moving state to a composable's caller to make it stateless and reusable.

**Bad (Stateful)**:


**Good (Stateless)**:


### Benefits of State Hoisting

- ✅ **Reusable**: Composable can be used with different state
- ✅ **Testable**: Easy to test with different inputs
- ✅ **Single source of truth**: State in one place
- ✅ **Control**: Parent controls state

### Pattern


---



```kotlin
// Stateless composable (receives state + callbacks)
@Composable
fun MyComponent(
    value: String,
    onValueChange: (String) -> Unit,
    modifier: Modifier = Modifier
) {
    // UI implementation
}

// Stateful wrapper (manages state)
@Composable
fun MyComponentStateful() {
    var value by remember { mutableStateOf("") }

    MyComponent(
        value = value,
        onValueChange = { value = it }
    )
}
```
