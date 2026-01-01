---
type: "THEORY"
title: "CocoaPods Integration"
---


### What is CocoaPods?

CocoaPods is the most popular dependency manager for iOS/macOS projects. KMP can integrate with CocoaPods to:
1. **Consume** iOS libraries in your Kotlin code
2. **Publish** your KMP library as a CocoaPod

### Setting Up CocoaPods Plugin

```kotlin
// build.gradle.kts
plugins {
    kotlin("multiplatform")
    kotlin("native.cocoapods")
}

kotlin {
    iosX64()
    iosArm64()
    iosSimulatorArm64()

    cocoapods {
        // Required: Library name and version
        name = "SharedModule"
        version = "1.0.0"
        summary = "Shared KMP code for iOS"
        homepage = "https://github.com/yourcompany/yourproject"

        // Framework settings
        framework {
            baseName = "shared"
            isStatic = true  // or false for dynamic
        }

        // iOS deployment target
        ios.deploymentTarget = "14.0"

        // Consume iOS libraries
        pod("AFNetworking") {
            version = "~> 4.0"
        }
        pod("Alamofire") {
            version = "5.8.0"
        }
    }
}
```

### Using CocoaPods Dependencies in Kotlin

```kotlin
// After adding Alamofire pod, you can use it:
import cocoapods.Alamofire.*

class IosNetworkClient {
    fun makeRequest(url: String) {
        // Use Alamofire APIs directly!
        AF.request(url).response { response in
            // Handle response
        }
    }
}
```

### Generating the Pod

Run Gradle to generate Podspec:

```bash
./gradlew podspec
```

This creates `SharedModule.podspec` that iOS developers can use:

```ruby
# In iOS project Podfile
pod 'SharedModule', :path => '../shared'
```

---

