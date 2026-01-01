---
type: "ANALOGY"
title: "The Concept: Repetition in Programming"
---


### Real-World Iteration

You perform iteration constantly in daily life:

**Making pancakes:**

**Checking email:**

**Grading papers:**

In each case, you're **repeating the same steps** for different items. That's exactly what loops do in programming!

### The Manual vs Loop Comparison

**Without loops (manual repetition):**

**With loops (automatic repetition):**

The loop version:
- Works for any number of names
- Less code to write and maintain
- Easy to modify (change the greeting in one place)
- No chance of typos from copying and pasting

---



```kotlin
val names = listOf("Alice", "Bob", "Charlie", "Diana", "Eve")
for (name in names) {
    println("Welcome, $name!")
}
```
