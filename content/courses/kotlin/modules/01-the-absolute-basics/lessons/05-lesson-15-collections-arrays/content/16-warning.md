---
type: "WARNING"
title: "Common Mistakes"
---


### Mistake 1: Modifying Read-Only Collections


### Mistake 2: Index Out of Bounds


### Mistake 3: Forgetting Map Values are Nullable


---



```kotlin
val phoneBook = mapOf("Alice" to "555-1234")

// ❌ Potential null
val number = phoneBook["Bob"]  // Returns String?, not String!

// ✅ Handle null
val number = phoneBook["Bob"] ?: "Unknown"
val number2 = phoneBook.getOrDefault("Bob", "Unknown")
```
