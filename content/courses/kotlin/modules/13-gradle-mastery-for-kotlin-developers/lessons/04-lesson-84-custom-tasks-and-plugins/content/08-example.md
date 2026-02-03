---
type: "EXAMPLE"
title: "Applying Script Plugins"
---


Apply the plugin in your build script:



```kotlin
// build.gradle.kts
plugins {
    kotlin("jvm") version "2.3.0"
}

// Apply the script plugin
apply(from = "gradle/quality.gradle.kts")

// Now quality tasks are available:
// ./gradlew detektCheck
// ./gradlew ktlintCheck
// ./gradlew check (includes both)
```
