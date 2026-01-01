---
type: "THEORY"
title: "Solution 2"
---


**Bonus Solution - Platform Detection**:


**Result**: The app shows which platform it's running on, demonstrating the expect/actual pattern for platform-specific code.

---



```kotlin
// commonMain/kotlin/Platform.kt
expect fun getPlatformName(): String

// androidMain/kotlin/Platform.android.kt
actual fun getPlatformName(): String = "Android"

// iosMain/kotlin/Platform.ios.kt
actual fun getPlatformName(): String = "iOS"

// Usage in App.kt:
@Composable
fun App() {
    MaterialTheme {
        Column(...) {
            Text("Running on ${getPlatformName()}")
        }
    }
}
```
