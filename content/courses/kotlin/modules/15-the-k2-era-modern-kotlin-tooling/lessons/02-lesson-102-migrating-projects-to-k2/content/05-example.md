---
type: "EXAMPLE"
title: "Stricter Null Checking"
---


K2 is stricter about null safety:



```kotlin
// Issue: K2 catches guaranteed null dereferences

// Before (K1 allowed this):
val map: Map<String, String> = mapOf()
val value: String = map["key"]!!  // K2 warns: always null

// After (fix the logic):
val value: String = map["key"] ?: error("Key not found")
// Or:
val value: String = map.getValue("key")  // Throws if missing

// Issue: Platform type handling is stricter
// Java method: String getName() - could return null

// Before (K1 was lenient):
val name: String = javaObject.name  // Might crash at runtime

// After (K2 warns):
val name: String = javaObject.name ?: ""  // Handle null explicitly
// Or:
val name: String = requireNotNull(javaObject.name) { "Name cannot be null" }

// Issue: Nullable receivers in extensions
// Before:
fun String.process() = this.uppercase()
val result = nullableString.process()  // K1 allowed

// After (K2 requires safe call):
val result = nullableString?.process()
// Or define extension on nullable:
fun String?.processSafe() = this?.uppercase() ?: ""
```
