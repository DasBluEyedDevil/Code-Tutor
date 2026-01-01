---
type: "THEORY"
title: "Hierarchical Source Sets"
---


### Advanced Project Structure

KMP supports hierarchical source sets for sharing code between subsets of platforms.

```kotlin
kotlin {
    androidTarget()
    iosX64()
    iosArm64()
    iosSimulatorArm64()
    jvm()
    
    sourceSets {
        // Shared across ALL platforms
        val commonMain by getting
        
        // Shared between Android and JVM only
        val jvmCommonMain by creating {
            dependsOn(commonMain)
        }
        val androidMain by getting {
            dependsOn(jvmCommonMain)
        }
        val jvmMain by getting {
            dependsOn(jvmCommonMain)
        }
        
        // Shared across all iOS variants
        val iosMain by creating {
            dependsOn(commonMain)
        }
        val iosX64Main by getting { dependsOn(iosMain) }
        val iosArm64Main by getting { dependsOn(iosMain) }
        val iosSimulatorArm64Main by getting { dependsOn(iosMain) }
        
        // Shared across native platforms (iOS + desktop native)
        val nativeMain by creating {
            dependsOn(commonMain)
        }
    }
}
```

### Why Hierarchical Source Sets?

- Share JVM-specific code between Android and server
- Share native-specific code between iOS and desktop
- Reduce duplication while maintaining platform flexibility

---

