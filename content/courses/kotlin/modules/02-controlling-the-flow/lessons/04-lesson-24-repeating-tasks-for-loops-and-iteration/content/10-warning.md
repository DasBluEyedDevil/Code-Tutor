---
type: "WARNING"
title: "Common Pitfalls and Best Practices"
---


### Pitfall 1: Off-By-One Errors

❌ **Common mistake:**

✅ **Correct:**

### Pitfall 2: Modifying Collection While Iterating

❌ **Dangerous:**

✅ **Safe approach:**

Or use built-in functions:

### Pitfall 3: Unnecessary Index Variables

⚠️ **Okay but verbose:**

✅ **Better:**

**Rule:** Only use indices when you actually need them.

### Best Practice 1: Descriptive Variable Names

❌ **Unclear:**

✅ **Clear:**

### Best Practice 2: Use Ranges Appropriately


### Best Practice 3: Choose the Right Loop Type


---



```kotlin
// Need the value only? Iterate directly
for (fruit in fruits) { println(fruit) }

// Need index and value? Use withIndex()
for ((index, fruit) in fruits.withIndex()) {
    println("$index: $fruit")
}

// Need just the index? Use indices
for (i in fruits.indices) {
    println("Position $i")
}
```
