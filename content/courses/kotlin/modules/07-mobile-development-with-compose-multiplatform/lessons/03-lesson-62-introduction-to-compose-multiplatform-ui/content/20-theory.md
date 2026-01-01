---
type: "THEORY"
title: "Quiz Answers"
---


**Question 1: B) Marks a function that can emit UI elements**

The `@Composable` annotation tells the Compose compiler:
- This function describes UI
- It can call other `@Composable` functions
- It can only be called from composable context


---

**Question 2: B) Creates state that persists across recompositions**

Without `remember`, state is lost on every recomposition:


---

**Question 3: C) Use the `.clickable()` modifier**


Alternative: Wrap in a `Button`, but that adds button styling.

---

**Question 4: B) `dp` for sizes/padding, `sp` for text (respects accessibility)**

- **`dp`** (density-independent pixels): Fixed size, same on all devices
  - Use for: padding, margins, component sizes
- **`sp`** (scalable pixels): Scales with user's font size preference
  - Use for: text size only
  - Respects accessibility settings


---

**Question 5: B) The composable automatically recomposes (rebuilds)**

Compose tracks state reads and automatically recomposes when state changes:


**Smart Recomposition**: Only the composables that read the changed state are recomposed, not the entire UI.

---



```kotlin
@Composable
fun Counter() {
    var count by remember { mutableStateOf(0) }

    // When count changes:
    // 1. Compose detects the change
    // 2. Automatically calls Counter() again
    // 3. UI updates with new count value

    Text("Count: $count")  // Auto-updates when count changes!
    Button(onClick = { count++ }) { Text("+") }
}
```
