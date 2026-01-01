---
type: "THEORY"
title: "Creating Lists"
---


### Immutable Lists (Read-Only)

Created with `listOf()`:


**Immutable means:**
- ❌ Can't add items
- ❌ Can't remove items
- ❌ Can't change items
- ✅ Can read and iterate
- ✅ Thread-safe and predictable

**When to use:** When your collection won't change (days of the week, menu options, etc.)

### Mutable Lists (Can Change)

Created with `mutableListOf()`:


**Output:**

**When to use:** When your collection needs to change (shopping cart, todo list, etc.)

### Empty Lists


### Lists with Type Inference


---



```kotlin
// Kotlin infers type from values
val numbers = listOf(1, 2, 3, 4, 5)  // List<Int>
val names = listOf("Alice", "Bob")    // List<String>
val mixed = listOf<Any>(1, "two", 3.0) // List<Any>

// Explicit type declaration
val scores: List<Int> = listOf(95, 87, 92)
```
