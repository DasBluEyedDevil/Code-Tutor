---
type: "EXAMPLE"
title: "Using Version Catalogs"
---


Reference the catalog in your build scripts:



```kotlin
// build.gradle.kts using version catalog
plugins {
    alias(libs.plugins.kotlin.jvm)
    alias(libs.plugins.kotlinx.serialization)
}

dependencies {
    // Single library
    implementation(libs.kotlinx.coroutines.core)
    implementation(libs.kotlinx.serialization.json)
    
    // Bundle of libraries
    implementation(libs.bundles.ktor.server)
    
    // Testing
    testImplementation(kotlin("test"))
}
```
