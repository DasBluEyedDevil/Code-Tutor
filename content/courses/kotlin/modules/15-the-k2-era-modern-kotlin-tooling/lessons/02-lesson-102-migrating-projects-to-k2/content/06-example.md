---
type: "EXAMPLE"
title: "Property Inference Changes"
---


Some type inference may differ:



```kotlin
// Issue: Complex expression inference

// K1 might infer differently than K2 in edge cases
val items = listOf(1, 2, 3)

// Before (K1):
val processed = items.map { 
    if (it > 0) it.toString() else null 
}  // K1 might infer List<String?>

// After (K2) - be explicit when needed:
val processed: List<String?> = items.map { 
    if (it > 0) it.toString() else null 
}

// Issue: Lambda return type inference
fun process(block: () -> Any): Any = block()

// Before (K1 - worked):
val result = process {
    if (condition) "string"
    else 42
}  // K1 inferred Any

// After (K2) - explicit type may be needed:
val result: Any = process {
    if (condition) "string" else 42
}

// Issue: Generic type inference
fun <T> identity(value: T): T = value

// K2 may require explicit types in complex chains:
val result = identity(listOf(1, 2, 3))
    .map { it * 2 }
    .filter { it > 2 }  // Usually works

// If issues occur, add explicit type:
val result: List<Int> = identity<List<Int>>(listOf(1, 2, 3))
    .map { it * 2 }
    .filter { it > 2 }
```
