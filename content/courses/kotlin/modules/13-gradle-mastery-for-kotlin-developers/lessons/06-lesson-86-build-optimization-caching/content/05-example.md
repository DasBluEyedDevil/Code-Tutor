---
type: "EXAMPLE"
title: "Remote Build Cache"
---


Share cache across your team or CI:



```kotlin
// settings.gradle.kts - Remote build cache
buildCache {
    local {
        isEnabled = true
        directory = File(rootDir, "build-cache")
    }

    remote<HttpBuildCache> {
        url = uri("https://cache.example.com/")
        isPush = System.getenv("CI") != null  // Only CI pushes
        credentials {
            username = System.getenv("CACHE_USER")
            password = System.getenv("CACHE_PASSWORD")
        }
    }
}
```
