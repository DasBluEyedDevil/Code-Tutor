---
type: "EXAMPLE"
title: "Convention Plugin"
---


Create a convention plugin for Kotlin libraries:



```kotlin
// buildSrc/src/main/kotlin/kotlin-library-conventions.gradle.kts
plugins {
    kotlin("jvm")
    id("io.gitlab.arturbosch.detekt")
}

kotlin {
    jvmToolchain(17)

    compilerOptions {
        allWarningsAsErrors.set(true)
        freeCompilerArgs.addAll(
            "-Xjdk-release=17",
            "-opt-in=kotlin.RequiresOptIn"
        )
    }
}

detekt {
    buildUponDefaultConfig = true
    config.setFrom(rootProject.files("config/detekt.yml"))
}

tasks.withType<Test> {
    useJUnitPlatform()
}
```
