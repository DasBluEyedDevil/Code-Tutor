---
type: "WARNING"
title: "Common Pitfalls and Best Practices"
---


### Pitfall 1: Index Out of Bounds

❌ **Crash:**

✅ **Safe:**

### Pitfall 2: Modifying Immutable Lists

❌ **Error:**

✅ **Correct:**

### Pitfall 3: Forgetting Lists Are Zero-Indexed

❌ **Confusion:**

✅ **Remember:**

### Best Practice 1: Use val with Mutable Lists


### Best Practice 2: Prefer Immutable When Possible


### Best Practice 3: Use Collection Functions


---



```kotlin
// ❌ Manual (verbose)
val numbers = listOf(1, 2, 3, 4, 5)
val evens = mutableListOf<Int>()
for (num in numbers) {
    if (num % 2 == 0) {
        evens.add(num)
    }
}

// ✅ Functional (concise)
val evens2 = numbers.filter { it % 2 == 0 }
```
