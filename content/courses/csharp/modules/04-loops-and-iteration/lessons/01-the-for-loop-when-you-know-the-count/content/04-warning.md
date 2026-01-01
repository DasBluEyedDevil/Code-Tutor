---
type: "WARNING"
title: "Common Pitfalls"
---

## Watch Out For These Mistakes!

**Off-by-one errors**: `for (int i = 0; i < 10; i++)` runs 10 times (0-9), but `for (int i = 0; i <= 10; i++)` runs 11 times (0-10). Be careful with `<` vs `<=`!

**Infinite loops**: `for (int i = 0; i >= 0; i++)` never ends because i is ALWAYS >= 0 when incrementing! Make sure your condition eventually becomes false.

**Modifying loop variable inside the body**: Changing `i` inside the loop body causes confusing behavior. Let the increment part handle it.

**Using commas instead of semicolons**: `for (int i = 0, i < 10, i++)` is WRONG! The three parts are separated by semicolons, not commas.

**Forgetting braces for multi-line bodies**: Without braces, only the first line is part of the loop. Always use braces to avoid bugs!