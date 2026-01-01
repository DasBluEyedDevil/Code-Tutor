---
type: "THEORY"
title: "Why Compose Multiplatform?"
---


### The Cross-Platform Challenge

**Traditional Approach** requires maintaining separate codebases:
- Android team: Kotlin with platform-specific UI
- iOS team: Swift + UIKit or SwiftUI
- Result: Duplicated logic, inconsistent UIs, higher costs

### Compose Multiplatform Solution


**Benefits**:
- **Single Codebase**: Write UI once, deploy everywhere
- **Native Performance**: No JavaScript bridge or web views
- **Type Safety**: Kotlin's type system catches errors at compile time
- **Declarative**: Describe what UI should look like, not how to build it
- **Kotlin First**: Leverage all Kotlin features across platforms
- **Reactive**: UI automatically updates when state changes
- **Interoperable**: Access platform-specific APIs when needed

### Code Sharing in Practice

Typical Compose Multiplatform projects achieve:
- **UI Layer**: 80-95% shared
- **Business Logic**: 90-100% shared
- **Platform APIs**: Expect/actual declarations for platform-specific needs

---



```kotlin
// This code runs on BOTH Android and iOS!
@Composable
fun Greeting(name: String) {
    Column {
        Text("Hello, $name!")
        Button(onClick = { /* handle click */ }) {
            Text("Click Me")
        }
    }
}
```
