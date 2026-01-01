---
type: "WARNING"
title: "Common Mistakes"
---


### Mistake 1: Forgetting Return Type


### Mistake 2: Not Returning a Value


### Mistake 3: Wrong Argument Order


---



```kotlin
fun createProfile(name: String, age: Int, city: String) { /* ... */ }

// ❌ Error - wrong order
createProfile(25, "Alice", "NYC")  // Type mismatch!

// ✅ Correct
createProfile("Alice", 25, "NYC")

// ✅ Better - use named arguments
createProfile(name = "Alice", age = 25, city = "NYC")
```
