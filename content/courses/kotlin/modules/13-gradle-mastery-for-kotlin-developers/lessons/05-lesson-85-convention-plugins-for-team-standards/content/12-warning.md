---
type: "WARNING"
title: "Convention Plugin Pitfalls"
---


### Overriding Applied Plugins

Be careful with plugin versions:

```kotlin
// buildSrc/build.gradle.kts
dependencies {
    implementation("org.jetbrains.kotlin:kotlin-gradle-plugin:2.0.21")
}

// Don't also specify version in convention plugin!
// kotlin-conventions.gradle.kts
plugins {
    kotlin("jvm")  // No version! Uses buildSrc dependency version
}
```

### Configuration Order

```kotlin
// WRONG - afterEvaluate can cause issues
afterEvaluate {
    kotlin { jvmToolchain(17) }
}

// RIGHT - configure directly
kotlin {
    jvmToolchain(17)
}
```

---

