---
type: "THEORY"
title: "State Management Basics"
---


### What is State?

**State** is any value that can change over time and affects the UI.

Examples:
- Text field input
- Counter value
- Checkbox checked/unchecked
- List of items

### remember and mutableStateOf


**How it works**:
1. `mutableStateOf(0)` creates state with initial value `0`
2. `remember { }` preserves state across recompositions
3. `by` delegates property access (requires `import androidx.compose.runtime.getValue` and `setValue`)
4. When `count` changes, Compose automatically recomposes (rebuilds) the UI

### Without Delegation


### Multiple State Variables


---



```kotlin
@Composable
fun LoginForm() {
    var email by remember { mutableStateOf("") }
    var password by remember { mutableStateOf("") }
    var rememberMe by remember { mutableStateOf(false) }

    Column {
        Text("Email: $email")
        Text("Password: $password")
        Text("Remember: $rememberMe")

        Button(onClick = {
            // Use state values
            println("Login: $email / $password")
        }) {
            Text("Login")
        }
    }
}
```
