---
type: "EXAMPLE"
title: "KSP Configuration Options"
---


Optimize KSP for your project:



```kotlin
// build.gradle.kts

ksp {
    // Pass arguments to processors
    arg("option.name", "value")
    
    // Room-specific arguments
    arg("room.schemaLocation", "$projectDir/schemas")
    arg("room.incremental", "true")
    arg("room.generateKotlin", "true")
    
    // Moshi arguments
    arg("moshi.generated", "javax.annotation.processing.Generated")
}

// For multiplatform projects:
kotlin {
    sourceSets {
        commonMain {
            kotlin.srcDir("build/generated/ksp/metadata/commonMain/kotlin")
        }
    }
}

// Add generated sources to source sets
android {
    applicationVariants.all {
        val variantName = name
        sourceSets {
            getByName("main") {
                java.srcDir("build/generated/ksp/$variantName/kotlin")
            }
        }
    }
}

// Performance optimization in gradle.properties
// ksp.incremental=true              # Enable incremental processing
// ksp.incremental.log=true          # Log incremental decisions
```
