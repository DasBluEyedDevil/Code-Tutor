---
type: "THEORY"
title: "Referential Transparency"
---


### What is Referential Transparency?

An expression is referentially transparent if it can be replaced with its value without changing the program's behavior.

```kotlin
// Referentially transparent
val x = add(2, 3)  // Can replace with 5 anywhere
val result = x + x  // Same as 5 + 5 = 10

// NOT referentially transparent
var counter = 0
fun next(): Int = ++counter  // Value changes each call!

val a = next()  // 1
val b = next()  // 2
// Cannot replace next() with its value!
```

### Why It Matters

1. **Equational reasoning**: Substitute equals for equals
2. **Refactoring safety**: Extract/inline without changing behavior
3. **Lazy evaluation**: Defer computation safely
4. **Parallel execution**: Order doesn't matter

---

