---
type: "THEORY"
title: "Creating Maps"
---


### Immutable Maps (Read-Only)

Created with `mapOf()`:


**Output:**

**The `to` keyword** creates a Pair: `"USA" to "Washington D.C."` → `Pair("USA", "Washington D.C.")`

### Mutable Maps (Can Change)

Created with `mutableMapOf()`:


**Output:**

### Empty Maps


### Maps with Different Types


---



```kotlin
// String keys, Int values
val ages = mapOf("Alice" to 25, "Bob" to 30)

// Int keys, String values
val weekDays = mapOf(
    1 to "Monday",
    2 to "Tuesday",
    3 to "Wednesday"
)

// String keys, Any values (mixed)
val mixed = mapOf(
    "name" to "Alice",
    "age" to 25,
    "active" to true
)
```
