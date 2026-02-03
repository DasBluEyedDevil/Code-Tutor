---
type: "THEORY"
title: "Understanding the Build Script"
---


### Plugins Block

Plugins add capabilities to your build:

```kotlin
plugins {
    kotlin("jvm") version "2.3.0"  // Kotlin JVM plugin
    application                       // Creates runnable application
    id("com.example.custom")          // Custom/third-party plugin
}
```

### Repositories Block

Repositories are where Gradle finds dependencies:

```kotlin
repositories {
    mavenCentral()                    // Primary repository
    google()                          // Android/Google libraries
    gradlePluginPortal()              // Gradle plugins
    maven("https://custom.repo.com") // Custom repository
}
```

### Dependencies Block

Dependencies are libraries your project uses:

```kotlin
dependencies {
    implementation("group:artifact:version")  // Runtime dependency
    api("group:artifact:version")             // Exposed to consumers
    testImplementation("group:artifact:version") // Test-only
    compileOnly("group:artifact:version")     // Compile-only
    runtimeOnly("group:artifact:version")     // Runtime-only
}
```

---

