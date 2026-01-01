---
type: "THEORY"
title: "Summary"
---


Congratulations! You've mastered lists in Kotlin. Let's recap:

**Key Concepts:**
- **Lists** store multiple items in order
- **Immutable lists** (`listOf`) can't be changed
- **Mutable lists** (`mutableListOf`) can be modified
- **Zero-indexed**: First element is at index 0
- **Rich operations**: map, filter, sort, find, and more

**List Creation:**

**Common Operations:**

**Best Practices:**
- Use immutable lists by default
- Prefer collection functions over manual loops
- Use safe access methods (getOrNull)
- Remember zero-based indexing
- Use val with mutable lists

---



```kotlin
// Access
list[0], list.first(), list.last()

// Modify (mutable only)
list.add(item)
list.remove(item)
list.removeAt(index)

// Transform
list.map { }      // Transform each
list.filter { }   // Keep matching
list.sorted()     // Sort

// Aggregate
list.sum()
list.average()
list.maxOrNull()
```
