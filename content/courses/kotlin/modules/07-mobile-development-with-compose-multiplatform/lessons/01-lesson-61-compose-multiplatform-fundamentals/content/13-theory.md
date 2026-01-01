---
type: "THEORY"
title: "Exercise 1: Customize the App"
---


Modify your `App.kt` in `commonMain` to:
1. Change the counter to display your name as a greeting
2. Add a second `Text` composable with your favorite color
3. Run on BOTH Android and iOS to verify it works on both

### Requirements


Run the app and verify:
- Your personalized greeting appears
- The app looks the same on Android and iOS

---



```kotlin
// composeApp/src/commonMain/kotlin/App.kt
@Composable
fun App() {
    MaterialTheme {
        Column(
            modifier = Modifier.fillMaxSize(),
            horizontalAlignment = Alignment.CenterHorizontally,
            verticalArrangement = Arrangement.Center
        ) {
            Text(text = "Hello, YourName!")
            Text(text = "My favorite color is Blue")
        }
    }
}
```
