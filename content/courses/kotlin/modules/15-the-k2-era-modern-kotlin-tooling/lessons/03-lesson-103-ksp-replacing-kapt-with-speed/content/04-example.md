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
    implementation("androidx.room:room-runtime:2.6.1")
    implementation("androidx.room:room-ktx:2.6.1")
    kapt("androidx.room:room-compiler:2.6.1")
}

// After: build.gradle.kts with KSP
plugins {
    id("com.google.devtools.ksp") version "2.0.21-1.0.28"
}

dependencies {
    implementation("androidx.room:room-runtime:2.6.1")
    implementation("androidx.room:room-ktx:2.6.1")
    ksp("androidx.room:room-compiler:2.6.1")  // Changed from kapt
}

// KSP-specific configuration for Room
ksp {
    arg("room.schemaLocation", "$projectDir/schemas")
    arg("room.incremental", "true")
    arg("room.generateKotlin", "true")  // Generate Kotlin instead of Java
}
```
