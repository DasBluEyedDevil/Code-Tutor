---
type: "WARNING"
title: "Common Mistakes"
---


### Mistake 1: Modifying Read-Only Collections
If you create a list with `listOf`, you cannot change it. Beginners often try to `add` or `remove` from a read-only list.
- `val items = listOf(1, 2)`
- `items.add(3)` ❌ (Method does not exist)
Always use `mutableListOf` if you need to modify the collection.

### Mistake 2: Index Out of Bounds
Trying to access an index that doesn't exist will crash your program.
- `val items = listOf("A")`
- `println(items[1])` ❌ (Index 1 doesn't exist, only 0 does)

### Mistake 3: Forgetting Map Values are Nullable

When you access a map with a key that doesn't exist, the result is `null`. Always handle this case to avoid null pointer exceptions.



```kotlin
val phoneBook = mapOf("Alice" to "555-1234")

// ❌ Potential null
val number = phoneBook["Bob"]  // Returns String?, not String!

// ✅ Handle null
val number = phoneBook["Bob"] ?: "Unknown"
val number2 = phoneBook.getOrDefault("Bob", "Unknown")
```
