---
type: "EXAMPLE"
title: "Excluding Dependencies"
---


Exclude transitive dependencies when needed:



```kotlin
dependencies {
    implementation("com.example:library:1.0") {
        // Exclude specific dependency
        exclude(group = "org.unwanted", module = "library")
        
        // Exclude all transitive dependencies
        isTransitive = false
    }
}

// Global exclusion
configurations.all {
    exclude(group = "org.unwanted", module = "library")
}
```
