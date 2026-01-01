---
type: "THEORY"
title: "Summary"
---


Congratulations! You've mastered for loops in Kotlin. Let's recap:

**Key Concepts:**
- **For loops** repeat code for each item in a collection or range
- **Ranges** define sequences: `1..10`, `1 until 10`, `10 downTo 1`
- **Step** allows custom increments: `0..100 step 5`
- **Collections** can be iterated directly or with indices
- **withIndex()** provides both index and value
- **Nested loops** enable multi-dimensional iteration

**For Loop Patterns:**

**Best Practices:**
- Iterate directly when you don't need indices
- Use `indices` or `until` to avoid off-by-one errors
- Use descriptive variable names
- Don't modify collections while iterating
- Choose the simplest loop form for your needs

---



```kotlin
// Range iteration
for (i in 1..10) { }

// Collection iteration
for (item in collection) { }

// With index
for ((index, item) in collection.withIndex()) { }

// Using indices
for (i in collection.indices) { }

// Reverse
for (i in 10 downTo 1) { }

// With step
for (i in 0..100 step 10) { }
```
