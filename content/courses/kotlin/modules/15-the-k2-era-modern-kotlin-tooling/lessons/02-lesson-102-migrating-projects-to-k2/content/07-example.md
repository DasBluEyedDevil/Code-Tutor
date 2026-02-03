---
type: "EXAMPLE"
title: "Library Compatibility Check"
---


Verify your dependencies support Kotlin 2.0:



```kotlin
// Check dependency versions
// Run: ./gradlew dependencies --configuration compileClasspath

// Common libraries with K2 support:

// gradle/libs.versions.toml
[versions]
kotlin = "2.3.0"
kotlinx-coroutines = "1.10.2"       # Full K2 support
kotlinx-serialization = "1.10.0"    # Full K2 support
ktor = "3.4.0"                      # Full K2 support
koin = "4.1.1"                      # Full K2 support (use ksp)
arrow = "2.2.1"                     # Full K2 support
compose-multiplatform = "1.10.0"    # Full K2 support

// Libraries to check/update:
// - Room: 2.6.0+ supports KSP
// - Moshi: 1.15.0+ supports KSP
// - Dagger/Hilt: Use KSP mode

// build.gradle.kts - Check for kapt usage
dependencies {
    // Replace kapt with ksp where possible:
    // kapt("com.google.dagger:hilt-compiler:2.51.1")  // Old
    ksp("com.google.dagger:hilt-compiler:2.51.1")     // New
    
    // Some libraries still require kapt:
    // kapt("some.legacy:processor:1.0")  // Check for updates
}
```
