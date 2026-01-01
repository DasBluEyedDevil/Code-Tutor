---
type: "THEORY"
title: "Modifiers"
---


### What are Modifiers?

**Modifiers** customize the appearance and behavior of composables:
- Size (width, height)
- Padding and margins
- Background colors
- Click handling
- Alignment

### Basic Modifiers


### Padding


### Background and Border


### Clickable


### Modifier Chaining

Order matters! Modifiers are applied sequentially:


---



```kotlin
@Composable
fun ModifierOrder() {
    // Padding INSIDE background
    Text(
        "Padding Inside",
        modifier = Modifier
            .background(Color.Blue)
            .padding(16.dp)  // Blue extends to edges, text has padding
    )

    // Padding OUTSIDE background
    Text(
        "Padding Outside",
        modifier = Modifier
            .padding(16.dp)  // Gap around blue background
            .background(Color.Blue)
    )
}
```
