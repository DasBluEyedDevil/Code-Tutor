---
type: "THEORY"
title: "Debugging Your App"
---


### Cross-Platform Logging

For shared code, use `println()` or a multiplatform logging library:


### Android Debugging

**Logcat** (Android Studio):
- View logs in **Logcat** tab at the bottom
- Filter by package name or log level
- Use `Log.d("TAG", "message")` in Android-specific code

**Layout Inspector**:
1. Run app on device/emulator
2. **Tools** → **Layout Inspector**
3. Explore component tree

### iOS Debugging

**Xcode Console**:
- View `print()` output in Xcode's debug console
- Use **Debug Navigator** for performance monitoring

**Xcode Previews**:
- iOS doesn't support Compose `@Preview` annotations
- Use the iOS Simulator for testing

### Tips for Multiplatform Debugging

- Test on BOTH platforms frequently, not just one
- Platform-specific bugs may only appear on one platform
- Use `expect`/`actual` for platform-specific logging

---



```kotlin
// Cross-platform logging in commonMain
println("Debug: Counter value is $count")

// Or create a simple expect/actual logger:

// commonMain:
expect fun log(message: String)

// androidMain:
actual fun log(message: String) = Log.d("App", message)

// iosMain:
actual fun log(message: String) = println(message)
```
