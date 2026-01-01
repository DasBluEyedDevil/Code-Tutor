---
type: "THEORY"
title: "Source Set Hierarchy"
---


### Understanding Source Sets

Source sets define where code lives and what it can access:

```
commonMain           <- Shared code (all platforms)
    |
    |-- androidMain  <- Android-specific
    |-- iosMain      <- iOS-specific (custom)
    |       |
    |       |-- iosX64Main
    |       |-- iosArm64Main
    |       |-- iosSimulatorArm64Main
    |
    |-- desktopMain  <- Desktop-specific
```

### Creating Custom Source Sets

```kotlin
sourceSets {
    // Create intermediate source set
    val mobileMain by creating {
        dependsOn(commonMain)
    }
    
    val androidMain by getting {
        dependsOn(mobileMain)  // Android inherits mobile code
    }
    
    val iosMain by creating {
        dependsOn(mobileMain)  // iOS inherits mobile code
    }
}
```

---

