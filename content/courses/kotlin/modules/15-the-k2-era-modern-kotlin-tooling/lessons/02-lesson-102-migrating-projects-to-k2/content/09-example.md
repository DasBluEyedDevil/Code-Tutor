---
type: "EXAMPLE"
title: "Troubleshooting Guide"
---


Common issues and their solutions:



```kotlin
// Issue: "Cannot access class" errors
// Solution: The class visibility changed, use public API instead

// Issue: "Type mismatch" after K2 upgrade
// Solution: Add explicit type annotations
val data: Map<String, Any> = response.body()  // Be explicit

// Issue: Smart cast stopped working
// Cause: K2 is stricter about mutability
var value: Any? = getValue()
if (value != null) {
    // K2 may not smart cast if 'value' could change
    // Solution: Use val or local copy
    val localValue = value
    if (localValue != null) {
        println(localValue.toString())  // Works
    }
}

// Issue: Overload resolution changed
// K2 may pick a different overload
fun process(value: Int) = println("Int: $value")
fun process(value: Number) = println("Number: $value")

// Solution: Be explicit about which overload
process(42)           // May differ
process(42 as Int)    // Explicitly Int
process(42 as Number) // Explicitly Number

// Issue: Build fails with kapt
// Solution: Migrate to KSP or update kapt version
plugins {
    // id("org.jetbrains.kotlin.kapt")  // Remove if possible
    id("com.google.devtools.ksp") version "2.3.4"
}
```
