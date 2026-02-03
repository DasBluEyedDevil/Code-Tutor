---
type: "KEY_POINT"
title: "Comparison and Logical Operators"
---

## Key Takeaways

- **`==` checks equality, `=` assigns** -- this is the most common beginner mistake. `if (age == 18)` checks; `age = 18` overwrites. The compiler catches `if (age = 18)` as an error in C#.

- **`&&` (AND) requires both sides true; `||` (OR) requires at least one** -- `&&` is stricter. Both operators short-circuit: if the left side determines the result, the right side is never evaluated.

- **`!` (NOT) flips a boolean** -- `!isRaining` means "it is not raining." Use it to invert conditions. Avoid double negatives like `!isNotReady` -- rename the variable instead.
