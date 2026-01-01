---
type: "WARNING"
title: "Common Pitfalls"
---

## Watch Out For These Mistakes!

**Missing semicolon**: `} while (condition)` is WRONG! You MUST have a semicolon: `} while (condition);` - this is the #1 do-while mistake.

**Confusing with regular while**: `while` checks BEFORE (might run 0 times). `do-while` checks AFTER (runs at least 1 time). Choose the right one!

**Wrong brace placement**: It's `do { } while`, NOT `do while { }`. The while comes AFTER the closing brace, not before.

**Using when not needed**: If your loop might legitimately need to run zero times, use a regular `while` instead. `do-while` forces at least one execution.

**Infinite loops still possible**: Even though it runs at least once, you can still create infinite loops if the condition never becomes false!