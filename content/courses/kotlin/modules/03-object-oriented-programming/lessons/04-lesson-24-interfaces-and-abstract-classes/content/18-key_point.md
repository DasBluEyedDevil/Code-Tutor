---
type: "KEY_POINT"
title: "Platform Contracts: The Expect/Actual Pattern"
---


Since this course teaches **Kotlin Multiplatform (KMP)** from day one, let's explore a powerful pattern that extends the interface concept to work across platforms: the `expect/actual` mechanism.

### The Problem

When writing code that runs on both Android and iOS, some things work differently on each platform:
- Getting the current time
- Reading files
- Logging messages
- Accessing device features

### The Solution: Expect/Actual

The `expect/actual` pattern is like an **interface for platforms**:
- `expect` declares **what** you need (the contract)
- `actual` provides **how** each platform implements it

### Basic Example

```kotlin
// In commonMain (shared code)
// Declares: "I need a way to get the platform name"
expect fun getPlatformName(): String

// In androidMain
actual fun getPlatformName(): String = "Android"

// In iosMain
actual fun getPlatformName(): String = "iOS"
```

### Using in Shared Code

```kotlin
// This works on ALL platforms!
fun greet(): String {
    return "Hello from ${getPlatformName()}!"
}

// Android: "Hello from Android!"
// iOS: "Hello from iOS!"
```

### Compare to Interfaces

| Concept | Interface | Expect/Actual |
|---------|-----------|---------------|
| Purpose | Define class contracts | Define platform contracts |
| Declares | What a class must implement | What each platform must provide |
| Scope | Within same codebase | Across platform source sets |
| Keyword | `interface` / `override` | `expect` / `actual` |

### When to Use

**Use expect/actual when:**
- You need platform-specific implementations
- Accessing platform APIs (storage, sensors, etc.)
- Different libraries per platform

**Example: Platform Logger**

```kotlin
// commonMain - declare what we need
expect fun log(message: String)

// androidMain - use Android's Log
actual fun log(message: String) {
    android.util.Log.d("App", message)
}

// iosMain - use println (or NSLog)
actual fun log(message: String) {
    println(message)
}
```

You'll explore this pattern in depth in Part 7 when building advanced cross-platform features!

---

