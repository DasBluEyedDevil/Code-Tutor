---
type: "EXAMPLE"
title: "Migrating Room to KSP"
---


Room is one of the most common kapt users. Here's how to migrate:



```kotlin
// Before: build.gradle.kts with kapt
plugins {
    id("org.jetbrains.kotlin.kapt")
}

dependencies {
    implementation("androidx.room:room-runtime:2.8.4")
    kapt("androidx.room:room-compiler:2.8.4")
}

// After: build.gradle.kts with KSP
plugins {
    id("com.google.devtools.ksp") version "2.3.4"
}

dependencies {
    implementation("androidx.room:room-runtime:2.8.4")
    ksp("androidx.room:room-compiler:2.8.4")  // Changed from kapt
}

// KSP-specific configuration for Room
ksp {
    arg("room.schemaLocation", "$projectDir/schemas")
    arg("room.incremental", "true")
    arg("room.generateKotlin", "true")  // Generate Kotlin instead of Java
}
```
