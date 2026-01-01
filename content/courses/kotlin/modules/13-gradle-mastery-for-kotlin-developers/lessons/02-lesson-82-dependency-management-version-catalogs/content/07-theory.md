---
type: "THEORY"
title: "Dependency Conflicts"
---


### Understanding Conflicts

Conflicts occur when different dependencies require different versions of the same library.

### Resolution Strategies

```kotlin
configurations.all {
    resolutionStrategy {
        // Force a specific version
        force("org.jetbrains.kotlin:kotlin-stdlib:2.0.21")
        
        // Fail on version conflict
        failOnVersionConflict()
        
        // Prefer project modules over external
        preferProjectModules()
    }
}
```

### Viewing Dependencies

```bash
# See all dependencies
./gradlew dependencies

# See specific configuration
./gradlew dependencies --configuration runtimeClasspath

# Find dependency that brought another
./gradlew dependencyInsight --dependency kotlin-stdlib
```

---

