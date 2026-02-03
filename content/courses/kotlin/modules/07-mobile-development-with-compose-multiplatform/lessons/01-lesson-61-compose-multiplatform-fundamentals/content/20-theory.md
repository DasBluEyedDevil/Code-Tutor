---
type: "THEORY"
title: "Quiz Answers"
---


**Question 1: B) JetBrains' framework for building native UI across Android, iOS, Desktop, and Web**

Compose Multiplatform extends Jetpack Compose beyond Android to support multiple platforms from a single Kotlin codebase.

---

**Question 2: C) commonMain/**

The `commonMain` source set contains code that runs on ALL platforms:
- Shared UI composables
- Business logic
- Data classes

Platform-specific code goes in `androidMain/` or `iosMain/`.

---

**Question 3: B) Declaring interfaces in shared code with platform-specific implementations**

The expect/actual pattern lets you:
- Declare an `expect` function/class in commonMain
- Provide `actual` implementations in each platform source set
- Access platform APIs from shared code


---

**Question 4: B) Marks a function that emits UI elements**

`@Composable` tells the compiler:
- This function describes UI
- Can call other `@Composable` functions
- Can only be called from composable context

---

**Question 5: B) iOS support is now stable**

Compose Multiplatform iOS support reached stable status in 2025, and the framework continues to evolve rapidly (latest version 1.10.0 as of January 2026). Companies like McDonald's, Cash App, and 9GAG are using it in production apps.

---



```kotlin
// expect/actual example:

// commonMain:
expect fun getPlatformName(): String

// androidMain:
actual fun getPlatformName(): String = "Android"

// iosMain:
actual fun getPlatformName(): String = "iOS"
```
