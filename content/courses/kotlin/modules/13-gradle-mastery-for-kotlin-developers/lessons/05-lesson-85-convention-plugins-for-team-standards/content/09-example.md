---
type: "EXAMPLE"
title: "Convention Plugin with Extensions"
---


Add custom configuration options:



```kotlin
// buildSrc/src/main/kotlin/team-kotlin-conventions.gradle.kts
plugins {
    kotlin("jvm")
}

// Custom extension for team options
interface TeamExtension {
    val enableStrictMode: Property<Boolean>
    val minimumCoverage: Property<Int>
}

val team = extensions.create<TeamExtension>("team")

// Apply defaults
team.enableStrictMode.convention(true)
team.minimumCoverage.convention(80)

afterEvaluate {
    if (team.enableStrictMode.get()) {
        kotlin {
            compilerOptions {
                allWarningsAsErrors.set(true)
            }
        }
    }
}

// Usage in build.gradle.kts:
// plugins { id("team-kotlin-conventions") }
// team {
//     enableStrictMode.set(false)  // Override for this module
//     minimumCoverage.set(90)
// }
```
