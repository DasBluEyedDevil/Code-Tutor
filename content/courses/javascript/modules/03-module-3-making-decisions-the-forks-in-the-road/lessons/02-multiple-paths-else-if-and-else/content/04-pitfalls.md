---
type: "PITFALL"
title: "Logic Chain Errors"
---

### 1. The `else` with a condition
A very common beginner mistake is trying to give an `else` its own condition.
*   **Wrong:** `else (score < 50) { ... }`
*   **Right:** `else if (score < 50) { ... }` OR just `else { ... }`
Remember: `else` is the "everything else" bucket. It doesn't need to be told when to run.

### 2. Overlapping Conditions
If your conditions overlap (e.g., `score > 50` and `score > 80`), make sure the order is correct. If you check `> 50` first, someone with a score of 90 will trigger the `> 50` block and the `> 80` block will be ignored.

### 3. Forgetting the `else`
If you leave out the `else`, and none of your `if` or `else if` conditions are true, **nothing** will happen. This might be what you want, but often it leads to a "silent fail" where the user gets no feedback. Always consider if a "default" message is needed.