---
type: "ANALOGY"
title: "The Concept: Transforming vs Iterating"
---


### The Traditional Way (Imperative)


### The Functional Way (Declarative)


**Benefits**:
- Clearer intent (filter, then sum)
- No mutable state (`var total`)
- Chainable operations
- Less error-prone
- Easier to test and reason about

---



```kotlin
val items = listOf(50.0, 120.0, 75.0, 200.0, 95.0)
val total = items
    .filter { it > 100 }
    .sum()
println(total)  // 320.0
```
