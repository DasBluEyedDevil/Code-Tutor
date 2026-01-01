---
type: "WARNING"
title: "Common Mistakes"
---


### Mistake 1: Overusing !!


### Mistake 2: Forgetting to Handle Null


### Mistake 3: Unnecessary Null Checks


---



```kotlin
val name: String = "Alice"  // Non-nullable

// ❌ Unnecessary
if (name != null) {
    println(name.length)
}

// ✅ Just use it directly
println(name.length)
```
