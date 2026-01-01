---
type: "THEORY"
title: "What are Convention Plugins?"
---


### The Problem

In large projects, you often see duplicated build configuration:

```kotlin
// app/build.gradle.kts
plugins {
    kotlin("jvm")
}
kotlin { jvmToolchain(17) }
tasks.withType<Test> { useJUnitPlatform() }

// lib/build.gradle.kts
plugins {
    kotlin("jvm")
}
kotlin { jvmToolchain(17) }  // Duplicate!
tasks.withType<Test> { useJUnitPlatform() }  // Duplicate!
```

### The Solution

Convention plugins encapsulate shared configuration:

```kotlin
// app/build.gradle.kts
plugins {
    id("kotlin-library-conventions")
}
// All standard config inherited!
```

---

