---
type: "THEORY"
title: "Constants: `const` vs `val`"
---


### `const` for Compile-Time Constants


**Rules for `const`**:
- Must be top-level, in object, or in companion object
- Must be primitive type or String
- Must be initialized with a compile-time constant
- Cannot have custom getter

---



```kotlin
object Constants {
    const val MAX_USERS = 100  // ✅ Compile-time constant
    const val API_KEY = "abc123"  // ✅ Compile-time constant

    val createdAt = System.currentTimeMillis()  // ✅ Runtime value (not const)
}

class Config {
    companion object {
        const val TIMEOUT = 30  // ✅ Top-level or companion object
        val instance = Config()  // ✅ Runtime value
    }
}
```
