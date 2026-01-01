---
type: "THEORY"
title: "Sequences: Lazy Evaluation"
---


Collections process eagerly (all at once). Sequences process lazily (on demand).

### The Problem with Eager Evaluation


### Sequences to the Rescue


### How Sequences Work


### When to Use Sequences

**Use sequences when**:
- ✅ Large collections (1000+ elements)
- ✅ Multiple chained operations
- ✅ Only need part of result (take, first)
- ✅ Infinite data streams

**Use regular collections when**:
- ✅ Small collections (< 100 elements)
- ✅ Single operation
- ✅ Need the entire result anyway

### Performance Comparison


---



```kotlin
fun measureTime(label: String, block: () -> Unit) {
    val start = System.currentTimeMillis()
    block()
    val elapsed = System.currentTimeMillis() - start
    println("$label: ${elapsed}ms")
}

val largeList = (1..10_000_000).toList()

measureTime("List") {
    val result = largeList
        .map { it * 2 }
        .filter { it > 1000 }
        .take(100)
        .sum()
}

measureTime("Sequence") {
    val result = largeList.asSequence()
        .map { it * 2 }
        .filter { it > 1000 }
        .take(100)
        .sum()
}

// Typical output:
// List: 450ms
// Sequence: 0ms (processes only ~51 elements!)
```
