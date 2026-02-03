---
type: "THEORY"
title: "Backward Compatibility"
---


### Your Code Still Works

One of the best things about the Kotlin 2.x series:

**Most existing code works unchanged!**

JetBrains designed K2 to be backward compatible:
- Existing libraries continue to work
- Your current code compiles without changes
- Gradual migration is supported

### What Might Need Attention

A few things to be aware of:

1. **K1 is deprecated**: IntelliJ IDEA 2025.3+ uses K2 mode exclusively. The old K1 compiler mode is no longer supported in modern IDEs.
2. **KAPT to KSP migration**: If you use annotation processing, migrate from KAPT to KSP for better K2 compatibility.
3. **Stricter type checking**: Some code that was previously allowed might now show warnings or errors.

### Checking Your Kotlin Version

In your `build.gradle.kts`:
```kotlin
plugins {
    kotlin("jvm") version "2.3.0"  // Latest stable as of early 2026
}
```

In IntelliJ IDEA or Android Studio:
- Go to **Settings > Languages & Frameworks > Kotlin**
- Check the Kotlin version

---

