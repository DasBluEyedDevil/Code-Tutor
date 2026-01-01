---
type: "THEORY"
title: "Running on iOS"
---


### Setting Up the iOS Simulator

To run your Compose Multiplatform app on iOS:

**Prerequisites**:
- macOS with Xcode installed
- iOS Simulator configured
- Kotlin Multiplatform Mobile plugin in Android Studio

**Running on iOS Simulator**:
1. Open your project in Android Studio
2. Select the iOS target (e.g., `iosApp`)
3. Choose an iOS Simulator from the device dropdown
4. Click Run (or press Ctrl+R on macOS)

**Alternatively, from command line**:
```bash
./gradlew :composeApp:iosSimulatorArm64Run
```

### Platform Differences to Note

| Feature | Android | iOS |
|---------|---------|-----|
| **Status Bar** | Standard Android bar | Notch/Dynamic Island area |
| **System Fonts** | Roboto | SF Pro |
| **Back Navigation** | Hardware/gesture button | Swipe from left edge |
| **Safe Area** | System bars | Safe area insets |

### Handling Safe Areas

On iOS, you need to account for the notch and home indicator:

```kotlin
// In commonMain - works on both platforms!
@Composable
fun App() {
    MaterialTheme {
        Scaffold(
            // Scaffold handles safe areas automatically
        ) { paddingValues ->
            Column(
                modifier = Modifier
                    .fillMaxSize()
                    .padding(paddingValues)
            ) {
                // Your content here
            }
        }
    }
}
```

### Entry Points

**Android Entry Point** (`androidMain`):
```kotlin
class MainActivity : ComponentActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContent {
            App()  // Shared composable
        }
    }
}
```

**iOS Entry Point** (`iosMain`):
```kotlin
fun MainViewController(): UIViewController {
    return ComposeUIViewController {
        App()  // Same shared composable!
    }
}
```

Both platforms use the same `App()` composable from `commonMain`!

---

