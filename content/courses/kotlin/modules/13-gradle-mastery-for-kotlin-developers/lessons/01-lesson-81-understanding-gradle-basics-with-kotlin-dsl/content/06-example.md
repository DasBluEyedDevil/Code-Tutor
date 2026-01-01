---
type: "EXAMPLE"
title: "Basic build.gradle.kts"
---


The build script configures how your project is built:



```kotlin
// build.gradle.kts
plugins {
    kotlin("jvm") version "2.0.21"
    application
}

group = "com.example"
version = "1.0.0"

repositories {
    mavenCentral()
}

dependencies {
    implementation(kotlin("stdlib"))
    testImplementation(kotlin("test"))
}

application {
    mainClass.set("com.example.MainKt")
}
```
