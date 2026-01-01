---
type: "WARNING"
title: "Platform Considerations"
---

### Where UI Tests Run

| Platform | Test Framework | Notes |
|----------|---------------|-------|
| Android | JUnit4 + Compose Test | Runs on device/emulator |
| Desktop (JVM) | JUnit4 + Compose Test | Runs headless |
| iOS | XCTest (limited) | Compose test support limited |
| Web | None built-in | Use Playwright/Selenium |

### Current KMP UI Testing Reality (2025)

Compose UI tests work well for:
- ✅ JVM/Desktop tests (fast, headless)
- ✅ Android instrumented tests
- ⚠️ iOS support is experimental

### Recommendation: Test Components in JVM

```kotlin
// Run fast UI tests on JVM
./gradlew :composeApp:jvmTest

// Run Android instrumented tests for full integration
./gradlew :composeApp:connectedAndroidTest
```

Most Compose components are platform-agnostic. Test them on JVM for speed, then run occasional Android tests for integration verification.