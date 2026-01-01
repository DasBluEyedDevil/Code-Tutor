---
type: "THEORY"
title: "Sets"
---


Sets are collections of **unique** elements (no duplicates).

### Read-Only Sets (setOf)


### Mutable Sets (mutableSetOf)


### Set Operations


### When to Use Sets

Use sets when:
- You need unique elements
- Order doesn't matter
- You need fast membership checking


---



```kotlin
// Example: Track unique visitors
val visitors = mutableSetOf<String>()

visitors.add("Alice")
visitors.add("Bob")
visitors.add("Alice")  // Duplicate, ignored
visitors.add("Carol")

println("Unique visitors: ${visitors.size}")  // 3
```
