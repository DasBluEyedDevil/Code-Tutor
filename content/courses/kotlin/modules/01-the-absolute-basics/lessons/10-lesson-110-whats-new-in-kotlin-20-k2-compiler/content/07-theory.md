---
type: "THEORY"
title: "Backward Compatibility"
---


### Your Code Still Works

One of the best things about Kotlin 2.0:

**Most existing code works unchanged!**

JetBrains designed K2 to be backward compatible:
- Existing libraries continue to work
- Your current code compiles without changes
- Gradual migration is supported

### What Might Need Attention

A few edge cases might behave differently:

1. **Some compiler plugins need updates**: If you use custom compiler plugins, check for K2-compatible versions
2. **Stricter type checking**: Some code that was previously allowed might now show warnings
3. **Deprecated features**: Some old features are fully removed

### Checking Your Kotlin Version

In your `build.gradle.kts`:
```kotlin
plugins {
    kotlin("jvm") version "2.0.0"  // Use Kotlin 2.0+
}
```

In IntelliJ IDEA:
- Go to **Settings > Languages & Frameworks > Kotlin**
- Check the Kotlin version

---

