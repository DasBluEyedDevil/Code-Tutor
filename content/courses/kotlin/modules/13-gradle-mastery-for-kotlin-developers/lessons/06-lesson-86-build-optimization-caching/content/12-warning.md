---
type: "WARNING"
title: "Common Performance Issues"
---


### Too Many Subprojects

```kotlin
// SLOW - 100+ subprojects
settings.gradle.kts:
include(":lib1", ":lib2", ... ":lib100")

// BETTER - consolidate small modules
// Or use --parallel with proper isolation
```

### Dynamic Dependency Resolution

```kotlin
// SLOW - version resolved every build
implementation("com.example:lib:+")

// FAST - fixed version
implementation("com.example:lib:1.2.3")
```

### Unnecessary Work

```kotlin
// SLOW - all tests run always
tasks.test {
    // No filtering
}

// FAST - run affected tests
./gradlew test --tests "*Feature*"
```

---

