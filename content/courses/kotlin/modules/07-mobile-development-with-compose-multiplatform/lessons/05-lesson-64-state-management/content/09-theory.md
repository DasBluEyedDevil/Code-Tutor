---
type: "THEORY"
title: "Derived State"
---


State computed from other state:


---



```kotlin
@Composable
fun DerivedStateExample() {
    var firstName by remember { mutableStateOf("") }
    var lastName by remember { mutableStateOf("") }

    // ❌ Bad: Recomposes on every keystroke
    val fullName = "$firstName $lastName"

    // ✅ Good: Only recomposes when firstName or lastName change
    val fullName by remember(firstName, lastName) {
        derivedStateOf { "$firstName $lastName" }
    }

    Column {
        TextField(value = firstName, onValueChange = { firstName = it })
        TextField(value = lastName, onValueChange = { lastName = it })
        Text("Full name: $fullName")
    }
}
```
