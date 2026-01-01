---
type: "THEORY"
title: "Multiple Convention Plugins"
---


### Layer Your Conventions

Create specialized plugins for different module types:

```
buildSrc/src/main/kotlin/
├── kotlin-base-conventions.gradle.kts     # Basic Kotlin setup
├── kotlin-library-conventions.gradle.kts  # For libraries
├── kotlin-app-conventions.gradle.kts      # For applications
├── kotlin-android-conventions.gradle.kts  # For Android
└── kotlin-test-conventions.gradle.kts     # Test configuration
```

### Composing Plugins

```kotlin
// kotlin-library-conventions.gradle.kts
plugins {
    id("kotlin-base-conventions")  // Apply base first
    id("kotlin-test-conventions")  // Add test setup
}

// Library-specific additions
kotlin {
    explicitApi()
}
```

---

