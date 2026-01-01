---
type: "ANALOGY"
title: "The Concept: Key-Value Pairs"
---


### Real-World Map Analogy

Think of a map like a **phone book** or **dictionary**:


**Properties:**
- **Keys are unique**: Can't have two "Alice" entries
- **Keys map to values**: Each key has exactly one value
- **Fast lookup**: Find value by key instantly
- **Unordered**: Entries aren't in a specific order (usually)

### List vs Map Comparison

**List (Index → Value):**

**Map (Key → Value):**

**When to use maps:**
- ✅ Looking up by meaningful keys (name, ID, word)
- ✅ Need fast key-based access
- ✅ Associating related data (country → capital)

**When to use lists:**
- ✅ Ordered sequence of items
- ✅ Accessing by position
- ✅ Simple collection of values

---



```kotlin
val colorCodes = mapOf(
    "Red" to "#FF0000",
    "Green" to "#00FF00",
    "Blue" to "#0000FF"
)
println(colorCodes["Red"])  // "#FF0000"
```
