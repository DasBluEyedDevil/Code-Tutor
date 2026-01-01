---
type: "THEORY"
title: "Project Structure Explained"
---


### Compose Multiplatform Project Layout


### Key Directories

#### 1. composeApp/src/commonMain/ (Shared Code)

This is where 80-95% of your code lives - shared across ALL platforms:


#### 2. composeApp/src/androidMain/ (Android-Specific)

Contains Android-specific implementations and the MainActivity:
- `AndroidManifest.xml`: App permissions and components
- `MainActivity.kt`: Android entry point that calls shared App()
- Platform-specific implementations using `actual` keyword

#### 3. composeApp/src/iosMain/ (iOS-Specific)

Contains iOS-specific implementations:
- `MainViewController.kt`: iOS entry point
- Platform-specific implementations using `actual` keyword

#### 4. iosApp/ (Xcode Project)

The iOS app wrapper that embeds the Kotlin framework:
- `iosApp.xcodeproj`: Xcode project file
- `ContentView.swift`: SwiftUI view that hosts the Compose UI
- `Info.plist`: iOS app configuration

### The Expect/Actual Pattern

For platform-specific code, use `expect` in commonMain and `actual` in platform source sets:

---



```kotlin
HelloCMP/
├── composeApp/
│   └── src/
│       ├── commonMain/          # Shared code (runs everywhere)
│       │   └── kotlin/
│       │       └── App.kt        # Your main composable
│       ├── androidMain/          # Android-specific code
│       │   └── kotlin/
│       │       └── MainActivity.kt
│       └── iosMain/              # iOS-specific code
│           └── kotlin/
│               └── MainViewController.kt
├── iosApp/                       # Xcode project for iOS
│   └── iosApp.xcodeproj
├── build.gradle.kts              # Root build config
└── gradle/libs.versions.toml     # Version catalog
```
