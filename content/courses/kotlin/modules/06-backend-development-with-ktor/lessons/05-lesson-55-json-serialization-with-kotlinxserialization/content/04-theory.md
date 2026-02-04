---
type: "THEORY"
title: "🔧 The @Serializable Annotation"
---


### Basic Usage


**What @Serializable does:**
1. Generates a **serializer** for the class at compile time
2. Knows how to convert each field to/from JSON
3. Works automatically with Ktor's `call.receive()` and `call.respond()`

### What Gets Serialized?


**Rule**: Only properties in the **primary constructor** are serialized.

---



```kotlin
@Serializable
data class User(
    val id: Int,           // ✅ Serialized
    val name: String,      // ✅ Serialized
    var age: Int           // ✅ Serialized (var or val doesn't matter)
) {
    val isAdult: Boolean   // ❌ NOT serialized (not in constructor)
        get() = age >= 18

    fun greet() {          // ❌ NOT serialized (functions never are)
        println("Hello!")
    }
}
```
