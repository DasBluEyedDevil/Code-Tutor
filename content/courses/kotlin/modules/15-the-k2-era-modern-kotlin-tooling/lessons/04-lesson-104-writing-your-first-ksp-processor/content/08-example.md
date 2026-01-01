---
type: "EXAMPLE"
title: "The Provider and Registration"
---


Register your processor with KSP:



```kotlin
// processor/src/main/kotlin/com/example/AutoBuilderProcessorProvider.kt
package com.example

import com.google.devtools.ksp.processing.*

class AutoBuilderProcessorProvider : SymbolProcessorProvider {
    override fun create(environment: SymbolProcessorEnvironment): SymbolProcessor {
        return AutoBuilderProcessor(
            codeGenerator = environment.codeGenerator,
            logger = environment.logger
        )
    }
}

// Register the provider:
// Create file: processor/src/main/resources/META-INF/services/com.google.devtools.ksp.processing.SymbolProcessorProvider
// Contents: com.example.AutoBuilderProcessorProvider

// Or use AutoService for automatic registration:
// processor/build.gradle.kts
dependencies {
    implementation("com.google.auto.service:auto-service-annotations:1.1.1")
    ksp("dev.zacsweers.autoservice:auto-service-ksp:1.2.0")
}

// Then annotate your provider:
import com.google.auto.service.AutoService

@AutoService(SymbolProcessorProvider::class)
class AutoBuilderProcessorProvider : SymbolProcessorProvider {
    // ...
}
```
