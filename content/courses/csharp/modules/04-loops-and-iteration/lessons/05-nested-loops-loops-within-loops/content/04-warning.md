---
type: "WARNING"
title: "Common Pitfalls"
---

## Watch Out For These Mistakes!

**Variable name collision**: `for (int i...) { for (int i...) }` is a compile error! Use different names: i/j, row/col, outer/inner.

**Modifying outer variable in inner loop**: `for (int i=0; i<3; i++) { for (int j=0; j<5; j++) { i=0; } }` resets i forever - infinite loop!

**Break only exits innermost loop**: In nested loops, `break` exits only the current loop. To exit all loops, use a flag: `bool done = false;` and check it in outer loop conditions.

**Performance explosion**: Nested loops multiply! 100x100=10,000 iterations. 100x100x100=1,000,000! Avoid deeply nested loops (3+ levels). Consider LINQ or parallel processing for large datasets.

**Off-by-one in patterns**: For a triangle pattern, `for (j=1; j<=i; j++)` gives increasing rows. Getting the bounds wrong is easy - trace through manually first!