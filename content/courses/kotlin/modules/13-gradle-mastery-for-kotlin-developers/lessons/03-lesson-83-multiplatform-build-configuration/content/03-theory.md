---
type: "THEORY"
title: "Multiplatform Plugin"
---


### Setting Up KMP

The multiplatform plugin is the foundation:

```kotlin
plugins {
    kotlin("multiplatform") version "2.3.0"
}

kotlin {
    // Configure targets here
}
```

### Available Targets

| Target | Platform | Use Case |
|--------|----------|----------|
| `jvm()` | JVM | Backend, Desktop |
| `androidTarget()` | Android | Mobile |
| `iosArm64()` | iOS Device | iPhone/iPad |
| `iosX64()` | iOS Simulator (Intel) | Testing |
| `iosSimulatorArm64()` | iOS Simulator (Apple Silicon) | Testing |
| `js()` | JavaScript | Web |
| `wasmJs()` | WebAssembly | Web |

---

