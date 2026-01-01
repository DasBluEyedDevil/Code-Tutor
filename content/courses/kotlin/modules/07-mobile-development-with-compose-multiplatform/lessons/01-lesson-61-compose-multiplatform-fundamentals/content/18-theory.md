---
type: "THEORY"
title: "Solution 3"
---


**Answers**:

1. **Shared App.kt**: `composeApp/src/commonMain/kotlin/App.kt`
   - This is where your shared UI code lives

2. **Android entry point**: `composeApp/src/androidMain/kotlin/MainActivity.kt`
   - Calls `setContent { App() }` to display the shared UI

3. **iOS entry point**: `composeApp/src/iosMain/kotlin/MainViewController.kt`
   - Creates a `ComposeUIViewController` that hosts the shared UI

4. **composeApp build.gradle.kts**: `composeApp/build.gradle.kts`
   - Contains multiplatform configuration, dependencies, and target platforms

5. **Xcode project**: `iosApp/`
   - Contains `iosApp.xcodeproj` which embeds the Kotlin framework
   - Also has `ContentView.swift` that hosts the Compose UI

---

