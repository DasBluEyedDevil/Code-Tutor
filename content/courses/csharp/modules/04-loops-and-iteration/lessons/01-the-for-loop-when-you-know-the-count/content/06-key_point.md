---
type: "KEY_POINT"
title: "For Loop Structure"
---

## Key Takeaways

- **Use `for` when you know the iteration count** -- the three-part header `(init; condition; increment)` makes the loop boundaries explicit. `for (int i = 0; i < 10; i++)` runs exactly 10 times.

- **Prefer `foreach` for collections** -- `foreach` is cleaner and less error-prone when you do not need the index. Use `for` only when you need index access or non-sequential iteration.

- **Off-by-one errors are the most common loop bug** -- `i < length` and `i <= length` differ by one iteration. Arrays are zero-indexed, so `i < array.Length` is correct, not `i <= array.Length`.
