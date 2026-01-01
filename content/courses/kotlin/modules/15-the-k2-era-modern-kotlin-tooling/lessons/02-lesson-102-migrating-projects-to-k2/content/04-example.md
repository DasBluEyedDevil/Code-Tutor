---
type: "EXAMPLE"
title: "Enabling K2 Progressively"
---


Start with language version, then move to API version:



```kotlin
// gradle/libs.versions.toml
[versions]
kotlin = "2.0.21"
kotlinx-coroutines = "1.9.0"
kotlinx-serialization = "1.7.3"

// Step 1: Update Kotlin version in build.gradle.kts
plugins {
    kotlin("jvm") version libs.versions.kotlin
}

// Step 2: Enable K2 with language version first
kotlin {
    compilerOptions {
        // Start with just language version
        languageVersion.set(org.jetbrains.kotlin.gradle.dsl.KotlinVersion.KOTLIN_2_0)
        
        // Build and test your project
        // Fix any issues that arise
    }
}

// Step 3: After successful testing, also set API version
kotlin {
    compilerOptions {
        languageVersion.set(org.jetbrains.kotlin.gradle.dsl.KotlinVersion.KOTLIN_2_0)
        apiVersion.set(org.jetbrains.kotlin.gradle.dsl.KotlinVersion.KOTLIN_2_0)
    }
}
```
