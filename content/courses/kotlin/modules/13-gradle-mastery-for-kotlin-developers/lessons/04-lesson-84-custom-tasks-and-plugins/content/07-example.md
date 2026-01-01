---
type: "EXAMPLE"
title: "Creating a Script Plugin"
---


Extract reusable build logic into script plugins:



```kotlin
// gradle/quality.gradle.kts
// A reusable script plugin for code quality

val detektTask = tasks.register("detektCheck") {
    group = "verification"
    description = "Runs Detekt static analysis"
    
    doLast {
        println("Running Detekt analysis...")
        // Detekt logic here
    }
}

val ktlintTask = tasks.register("ktlintCheck") {
    group = "verification"
    description = "Runs ktlint style check"
    
    doLast {
        println("Running ktlint check...")
        // ktlint logic here
    }
}

tasks.named("check") {
    dependsOn(detektTask, ktlintTask)
}
```
