---
type: "EXAMPLE"
title: "buildSrc/build.gradle.kts"
---


Configure buildSrc to use Kotlin DSL:



```kotlin
// buildSrc/build.gradle.kts
plugins {
    `kotlin-dsl`
}

repositories {
    mavenCentral()
    gradlePluginPortal()
}

dependencies {
    // Add plugins you want to configure in convention plugins
    implementation("org.jetbrains.kotlin:kotlin-gradle-plugin:2.3.0")
    implementation("io.gitlab.arturbosch.detekt:detekt-gradle-plugin:1.23.8")
}
```
