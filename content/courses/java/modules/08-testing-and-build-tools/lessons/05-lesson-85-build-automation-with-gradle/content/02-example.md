---
type: "EXAMPLE"
title: "build.gradle.kts Structure"
---

A Gradle build file defines plugins, repositories, dependencies, and application config. Notice how much shorter it is than Maven's pom.xml! Plugins define build behavior, repositories specify where to download dependencies, and the application block configures how to run your app.

```kotlin
plugins {
    kotlin("jvm") version "2.1.0"
    application
}

repositories {
    mavenCentral()
}

dependencies {
    implementation("com.google.guava:guava:32.1.2-jre")
    testImplementation(kotlin("test"))
}

application {
    mainClass.set("MainKt")
}
```
