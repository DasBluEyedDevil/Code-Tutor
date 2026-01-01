---
type: "THEORY"
title: "What is Compose Multiplatform?"
---


### The Framework

**Compose Multiplatform** is JetBrains' declarative UI framework that lets you build native user interfaces for multiple platforms from a single Kotlin codebase.

**Key Features**:
- **Single Codebase**: Write UI once, run on Android, iOS, Desktop, and Web
- **Native Performance**: Compiles to native code on each platform
- **Declarative UI**: Describe what the UI should look like, not how to build it
- **Kotlin-First**: Built entirely for Kotlin with full language support
- **Hot Reload**: Preview changes instantly during development

### Platform Support Timeline

| Platform       | Status     | Release    | Notes                          |
|----------------|------------|------------|--------------------------------|
| **Android**    | Stable     | 2021       | Via Jetpack Compose            |
| **Desktop**    | Stable     | 2021       | Windows, macOS, Linux          |
| **iOS**        | Stable     | May 2025   | Swift/SwiftUI interop          |
| **Web**        | Alpha      | 2024       | Canvas-based rendering         |

### Traditional Approach vs Compose Multiplatform

**Traditional** (Two Codebases):
- Android: Kotlin + Jetpack Compose
- iOS: Swift + SwiftUI
- Duplicate business logic and UI code
- Double the maintenance effort

**Compose Multiplatform** (One Codebase):
- Shared UI and business logic in Kotlin
- Platform-specific code only when needed
- 70-90% code sharing typically achievable

### Companies Using Compose Multiplatform

- **McDonald's**: Mobile ordering app
- **Cash App**: Financial services
- **9GAG**: Social media platform
- **Philips**: Healthcare applications
- **Netflix**: Internal tools

---



```kotlin
┌─────────────────────────────────────────────┐
│         Compose Multiplatform               │
├─────────────────────────────────────────────┤
│  commonMain/                                │  Shared UI + Logic
│    └── App.kt                              │
├──────────────────┬──────────────────────────┤
│  androidMain/    │  iosMain/                │
│    Platform-     │    Platform-             │
│    specific      │    specific              │
└──────────────────┴──────────────────────────┘
```
