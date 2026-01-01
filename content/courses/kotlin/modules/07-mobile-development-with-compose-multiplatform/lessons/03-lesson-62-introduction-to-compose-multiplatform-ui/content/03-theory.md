---
type: "THEORY"
title: "What is Compose Multiplatform?"
---


### Declarative UI Framework for All Platforms

**Imperative (Old Way)**:
- Manually create and update UI elements
- Keep track of UI state separately
- Write code to sync state with UI
- Write separate UI code for Android (XML) and iOS (SwiftUI/UIKit)

**Declarative (Compose Way)**:
- Describe what UI should look like for a given state
- Framework automatically updates UI when state changes
- Less code, fewer bugs
- **Same UI code works on Android AND iOS!**

**Benefits**:
- Less code (often 40% reduction)
- Easier to read and maintain
- No manual view updates (automatic recomposition)
- Type-safe (compiler catches errors)
- Real-time, interactive previews
- Easy reusability through functions
- **Share UI across Android, iOS, Desktop, and Web**

### Compose Multiplatform vs Jetpack Compose

| Feature | Jetpack Compose | Compose Multiplatform |
|---------|-----------------|----------------------|
| Platform | Android only | Android, iOS, Desktop, Web |
| UI Code | Android-specific | Shared across platforms |
| Native Performance | Yes | Yes (compiles to native) |
| Material Design | Yes | Yes |

---



```kotlin
// Describe WHAT the UI should look like
@Composable
fun MyScreen() {
    var text by remember { mutableStateOf("Hello") }

    Column {
        Text(
            text = text,
            fontSize = 20.sp,
            color = Color.Blue
        )
        Button(onClick = { text = "Clicked!" }) {
            Text("Click Me")
        }
    }
}
```
