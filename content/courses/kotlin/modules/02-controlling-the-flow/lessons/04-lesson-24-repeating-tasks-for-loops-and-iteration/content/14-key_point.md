---
type: "KEY_POINT"
title: "Key Takeaways"
---

**For loops in Kotlin iterate over anything with an iterator**, not just numeric ranges. Collections, ranges, progressions, and custom types can all be iterated with the same elegant `for (item in collection)` syntax.

**Use ranges and progressions for numeric iteration**: `1..10` for inclusive ranges, `1 until 10` for exclusive, `10 downTo 1` for descending, and `step` for custom increments. These are more expressive than C-style loops.

**Prefer higher-order functions like `forEach`, `map`, and `filter` for collection iteration** when you're transforming or processing data. Save explicit for loops for cases where you need index access or complex control flow.
