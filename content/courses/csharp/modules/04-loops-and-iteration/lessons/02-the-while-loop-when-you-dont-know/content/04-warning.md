---
type: "WARNING"
title: "Common Pitfalls"
---

## Watch Out For These Mistakes!

**Infinite loops**: The #1 mistake! Forgetting to change the condition variable inside the loop: `while (x < 10) { /* oops, x never changes! */ }` hangs forever.

**Semicolon after while**: `while (condition);` is a silent bug! The semicolon creates an empty loop body, and your actual code runs only once after the (infinite) loop ends.

**Condition never true**: If the condition starts false, the loop body NEVER runs: `int x = 100; while (x < 50) { /* never executes */ }`

**Changing the wrong variable**: If your condition checks `count`, make sure you're modifying `count` inside the loop, not some unrelated variable!

**Not using braces**: Without braces, only the FIRST statement is part of the loop. Always use braces to avoid subtle bugs!