---
type: "EXAMPLE"
title: "Using Convention Plugins"
---


Apply the convention plugin in modules:



```kotlin
// app/build.gradle.kts
plugins {
    id("kotlin-library-conventions")
    application
}

// All standard config inherited from convention plugin!
// Just add module-specific config:
application {
    mainClass.set("com.example.MainKt")
}

// lib/build.gradle.kts  
plugins {
    id("kotlin-library-conventions")
}

// That's it! All team standards applied automatically.
```
