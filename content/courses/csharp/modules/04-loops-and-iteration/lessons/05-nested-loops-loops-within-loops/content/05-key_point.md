---
type: "KEY_POINT"
title: "Nested Loop Awareness"
---

## Key Takeaways

- **Iterations multiply** -- an outer loop of 100 and inner loop of 100 equals 10,000 total iterations. Be mindful of performance with large datasets.

- **Use distinct variable names** -- `i` for outer, `j` for inner, or meaningful names like `row` and `col`. Reusing the same variable name causes a compiler error.

- **Consider alternatives for large datasets** -- LINQ, `Parallel.For`, or dictionary lookups can replace nested loops with better performance. If you find yourself nesting three or more levels, refactor.
