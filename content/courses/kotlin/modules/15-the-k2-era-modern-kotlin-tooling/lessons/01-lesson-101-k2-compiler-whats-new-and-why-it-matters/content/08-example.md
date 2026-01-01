---
type: "EXAMPLE"
title: "Enabling K2 in Your Project"
---


How to configure K2 in your build:



```kotlin
// build.gradle.kts

plugins {
    kotlin("jvm") version "2.0.21"
}

kotlin {
    compilerOptions {
        // Use Kotlin 2.0 language version (enables K2)
        languageVersion.set(org.jetbrains.kotlin.gradle.dsl.KotlinVersion.KOTLIN_2_0)
        apiVersion.set(org.jetbrains.kotlin.gradle.dsl.KotlinVersion.KOTLIN_2_0)
        
        // Optional: Enable all warnings as errors
        allWarningsAsErrors.set(true)
        
        // Optional: Enable progressive mode for latest fixes
        progressiveMode.set(true)
    }
}

// For multiplatform projects:
kotlin {
    jvm {
        compilerOptions {
            languageVersion.set(org.jetbrains.kotlin.gradle.dsl.KotlinVersion.KOTLIN_2_0)
        }
    }
    
    // Applies to all targets
    sourceSets.all {
        languageSettings {
            languageVersion = "2.0"
            apiVersion = "2.0"
        }
    }
}
```
