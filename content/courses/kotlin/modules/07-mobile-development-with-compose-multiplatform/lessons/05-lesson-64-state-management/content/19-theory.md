---
type: "THEORY"
title: "Quiz Answers"
---


**Question 1: B) Re-executing composable functions when state changes**


**Smart**: Only composables reading changed state recompose, not everything.

---

**Question 2: B) `rememberSaveable` survives configuration changes (rotation)**


**Use `rememberSaveable` for**: form input, user selections
**Use `remember` for**: temporary UI state (dialog open)

---

**Question 3: A) Moving state up to make composables stateless**


---

**Question 4: B) For screen-level state that survives config changes**


**ViewModel for**:
- Data from repository/network
- Screen-level state
- Business logic

**remember for**:
- UI state (dialog open, selected tab)
- Animation values
- Scroll state

---

**Question 5: B) State computed from other state**


**Benefits**:
- Avoids storing redundant state
- Automatically updates when dependencies change
- More efficient than manual computation

---



```kotlin
@Composable
fun UserProfile() {
    var firstName by remember { mutableStateOf("John") }
    var lastName by remember { mutableStateOf("Doe") }

    // Derived state: computed from firstName + lastName
    val fullName by remember(firstName, lastName) {
        derivedStateOf { "$firstName $lastName" }
    }

    Text("Full name: $fullName")  // "John Doe"
}
```
