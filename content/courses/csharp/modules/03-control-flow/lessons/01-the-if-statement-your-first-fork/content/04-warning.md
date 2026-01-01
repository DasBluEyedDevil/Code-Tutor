---
type: "WARNING"
title: "Watch Out!"
---

## Common Pitfalls with if Statements

**Semicolon after if:** Never put a semicolon right after the condition! `if (age >= 21);` creates an empty if block, and the code in braces will ALWAYS run.

**Using = instead of ==:** Using a single equals sign is ASSIGNMENT, not comparison. `if (age = 21)` tries to assign 21 to age, which causes a compiler error in C#.

**Skipping braces:** While C# allows single-line if statements without braces, this is risky. Adding a second line later will NOT be part of the if block!

**Truthy values:** Unlike JavaScript, C# requires an actual boolean. `if (age)` is invalid - you must write `if (age > 0)` or similar.