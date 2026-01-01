---
type: "THEORY"
title: "Sets"
---


Sets are collections of **unique** elements. If you try to add a duplicate item, it will be ignored.

### Read-Only Sets (setOf)
```kotlin
val numbers = setOf(1, 2, 3, 3, 3)
println(numbers) // [1, 2, 3] (Duplicates were removed)
```

### Mutable Sets (mutableSetOf)
```kotlin
val colors = mutableSetOf("Red", "Green")
colors.add("Blue")
colors.add("Red") // Ignored
```

### Set Operations
Sets are great for mathematical operations like finding intersection or union.

```kotlin
val setA = setOf(1, 2, 3)
val setB = setOf(3, 4, 5)

println(setA intersect setB) // [3]
println(setA union setB)     // [1, 2, 3, 4, 5]
```

### When to Use Sets

Use sets when:
- You need to ensure all elements are unique
- Order doesn't matter
- You want fast lookup (checking if an item exists)
- You need mathematical set operations (union, intersection, difference)



```kotlin
// Example: Track unique visitors
val visitors = mutableSetOf<String>()

visitors.add("Alice")
visitors.add("Bob")
visitors.add("Alice")  // Duplicate, ignored
visitors.add("Carol")

println("Unique visitors: ${visitors.size}")  // 3
```
