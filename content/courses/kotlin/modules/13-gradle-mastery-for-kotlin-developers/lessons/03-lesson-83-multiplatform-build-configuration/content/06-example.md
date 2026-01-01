---
type: "EXAMPLE"
title: "Platform-Specific Dependencies"
---


Different platforms need different implementations:



```kotlin
sourceSets {
    val commonMain by getting {
        dependencies {
            // Works on all platforms
            implementation(libs.kotlinx.coroutines.core)
            implementation(libs.kotlinx.datetime)
        }
    }
    
    val androidMain by getting {
        dependencies {
            // Android-specific HTTP client
            implementation(libs.ktor.client.okhttp)
            // Android-specific image loading
            implementation(libs.coil.compose)
        }
    }
    
    val iosMain by getting {
        dependencies {
            // iOS-specific HTTP client (uses Darwin networking)
            implementation(libs.ktor.client.darwin)
        }
    }
    
    val desktopMain by getting {
        dependencies {
            // Desktop uses CIO or Java client
            implementation(libs.ktor.client.cio)
        }
    }
}
```
