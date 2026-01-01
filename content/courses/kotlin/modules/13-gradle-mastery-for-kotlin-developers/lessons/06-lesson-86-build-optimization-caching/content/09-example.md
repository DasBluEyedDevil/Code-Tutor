---
type: "EXAMPLE"
title: "Kotlin Compilation Optimization"
---


Speed up Kotlin compilation specifically:



```kotlin
// build.gradle.kts
kotlin {
    compilerOptions {
        // Use incremental compilation
        incremental = true
        
        // Parallel compilation (for multi-module)
        freeCompilerArgs.add("-Xparallel-compilation")
    }
}

// gradle.properties
kotlin.incremental=true
kotlin.caching.enabled=true
kotlin.compiler.execution.strategy=in-process

# For large projects
kotlin.build.report.output=file
```
