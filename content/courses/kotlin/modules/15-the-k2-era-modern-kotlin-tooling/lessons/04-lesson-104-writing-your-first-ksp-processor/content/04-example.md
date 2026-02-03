---
type: "EXAMPLE"
title: "Project Structure"
---


A KSP processor typically uses a multi-module setup:



```kotlin
// Project structure:
// my-project/
// ├── annotations/          # Annotation definitions
// │   ├── build.gradle.kts
// │   └── src/main/kotlin/
// │       └── AutoBuilder.kt
// ├── processor/             # KSP processor
// │   ├── build.gradle.kts
// │   └── src/main/kotlin/
// │       └── AutoBuilderProcessor.kt
// └── app/                   # Application using the processor
//     └── build.gradle.kts

// annotations/build.gradle.kts
plugins {
    kotlin("jvm")
}

// This module has no dependencies - just the annotation

// processor/build.gradle.kts
plugins {
    kotlin("jvm")
}

dependencies {
    implementation(project(":annotations"))
    implementation("com.google.devtools.ksp:symbol-processing-api:2.3.4")
    implementation("com.squareup:kotlinpoet:1.18.1")
    implementation("com.squareup:kotlinpoet-ksp:1.18.1")
}

// app/build.gradle.kts
plugins {
    kotlin("jvm")
    id("com.google.devtools.ksp") version "2.3.4"
}

dependencies {
    implementation(project(":annotations"))
    ksp(project(":processor"))
}
```
