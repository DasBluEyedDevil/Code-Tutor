---
type: "EXAMPLE"
title: "settings.gradle.kts"
---


The settings file defines your project structure:



```kotlin
// settings.gradle.kts
rootProject.name = "my-kotlin-project"

// For multi-module projects:
include(":app")
include(":shared")
include(":backend")
```
