---
type: "THEORY"
title: "Quiz Answers"
---


**Question 1: B) `map` transforms each element; `flatMap` transforms and flattens nested structures**


`flatMap` = `map` + `flatten`

---

**Question 2: C) A new collection with only elements matching the predicate**


`filter` returns a new list; the original is unchanged (immutability).

---

**Question 3: B) `fold` requires an initial value; `reduce` uses the first element as initial value**


`fold` is safer and more flexible.

---

**Question 4: C) For large collections with multiple operations, especially when you don't need all results**


Sequences have overhead; only beneficial for specific scenarios.

---

**Question 5: B) Splits a collection into two groups based on a predicate**


Returns a `Pair` of lists: (matching, not-matching).

---



```kotlin
val numbers = listOf(1, 2, 3, 4, 5, 6)

val (evens, odds) = numbers.partition { it % 2 == 0 }
println(evens)  // [2, 4, 6]
println(odds)   // [1, 3, 5]
```
