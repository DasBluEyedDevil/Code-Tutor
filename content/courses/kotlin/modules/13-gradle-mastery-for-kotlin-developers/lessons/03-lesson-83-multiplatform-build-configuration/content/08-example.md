---
type: "EXAMPLE"
title: "Hierarchical Source Sets (New Default)"
---


Kotlin 2.0 uses hierarchical source sets by default:



```kotlin
kotlin {
    // This is now the default in Kotlin 2.0
    // applyDefaultHierarchyTemplate() is called automatically
    
    androidTarget()
    iosArm64()
    iosSimulatorArm64()
    jvm()
    
    sourceSets {
        // Automatic hierarchies created:
        // commonMain
        //   |-- appleMain (all Apple targets)
        //   |     |-- iosMain (all iOS targets)
        //   |           |-- iosArm64Main
        //   |           |-- iosSimulatorArm64Main
        //   |-- jvmMain
        //   |-- androidMain
        
        val commonMain by getting {
            dependencies {
                implementation(libs.kotlinx.coroutines.core)
            }
        }
        
        // appleMain is automatically created for Apple targets
        val appleMain by getting {
            dependencies {
                implementation(libs.ktor.client.darwin)
            }
        }
    }
}
```
