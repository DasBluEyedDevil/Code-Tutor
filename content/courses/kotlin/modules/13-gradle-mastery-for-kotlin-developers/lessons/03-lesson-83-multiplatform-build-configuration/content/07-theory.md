---
type: "THEORY"
title: "iOS Framework Configuration"
---


### Building iOS Frameworks

iOS apps consume Kotlin code as frameworks:

```kotlin
kotlin {
    listOf(
        iosX64(),
        iosArm64(),
        iosSimulatorArm64()
    ).forEach { target ->
        target.binaries {
            framework {
                baseName = "SharedKit"  // Framework name
                isStatic = true         // Static vs dynamic
                
                // Export other modules
                export(project(":core"))
                export(libs.kotlinx.datetime)
            }
        }
    }
}
```

### CocoaPods Integration

```kotlin
plugins {
    kotlin("multiplatform")
    kotlin("native.cocoapods")
}

kotlin {
    cocoapods {
        summary = "Shared Kotlin code"
        homepage = "https://github.com/example/project"
        version = "1.0.0"
        
        ios.deploymentTarget = "14.0"
        
        framework {
            baseName = "SharedKit"
            isStatic = true
        }
        
        // Use CocoaPods dependencies
        pod("AFNetworking") {
            version = "~> 4.0"
        }
    }
}
```

---

