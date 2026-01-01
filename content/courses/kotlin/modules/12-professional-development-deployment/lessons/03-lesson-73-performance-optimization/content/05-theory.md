---
type: "THEORY"
title: "Memory Management"
---


### Detecting Memory Leaks

**Common Leak: Activity Reference in ViewModel**:

❌ **Bad**:

✅ **Good**:

**Common Leak: Coroutine Not Cancelled**:

❌ **Bad**:

✅ **Good**:

**Better: Use lifecycleScope**:

### Memory Leak Detection with LeakCanary


LeakCanary automatically detects leaks and shows:
- What object leaked
- Reference path keeping it alive
- Suggested fix

---



```kotlin
// build.gradle.kts
dependencies {
    debugImplementation("com.squareup.leakcanary:leakcanary-android:2.13")
}
```
