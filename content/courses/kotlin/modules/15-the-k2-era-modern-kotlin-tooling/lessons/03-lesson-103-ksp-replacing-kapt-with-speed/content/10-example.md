---
type: "EXAMPLE"
title: "Mixing kapt and KSP"
---


Some projects need both during migration:



```kotlin
// build.gradle.kts - Using both kapt and KSP
plugins {
    kotlin("jvm") version "2.0.21"
    id("org.jetbrains.kotlin.kapt")  // Keep for legacy processors
    id("com.google.devtools.ksp") version "2.0.21-1.0.28"
}

dependencies {
    // KSP-migrated libraries
    implementation("androidx.room:room-runtime:2.6.1")
    ksp("androidx.room:room-compiler:2.6.1")
    
    implementation("com.squareup.moshi:moshi:1.15.1")
    ksp("com.squareup.moshi:moshi-kotlin-codegen:1.15.1")
    
    // Legacy library still on kapt
    implementation("some.legacy:library:1.0.0")
    kapt("some.legacy:processor:1.0.0")
}

// Note: Having both adds overhead
// Plan to remove kapt entirely when possible

// Order matters! KSP runs before kapt by default
// If you need kapt to run first:
tasks.withType<org.jetbrains.kotlin.gradle.tasks.KotlinCompile> {
    dependsOn("kaptKotlin")
}
```
