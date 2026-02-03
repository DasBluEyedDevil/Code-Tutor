---
type: "KEY_POINT"
title: "Array Fundamentals"
---

## Key Takeaways

- **Arrays have a fixed size set at creation** -- `new int[5]` creates exactly 5 slots that cannot grow or shrink. If you need resizing, use `List<T>` instead.

- **Indexes start at 0** -- an array of size 5 has indexes 0 through 4. Accessing index 5 throws an `IndexOutOfRangeException`. Use `.Length` to check bounds.

- **Use arrays when size is known and performance matters** -- arrays offer the fastest iteration and direct memory access. For most other scenarios, `List<T>` is more practical.
