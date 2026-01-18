---
type: "COMMON_PITFALLS"
title: "Common Pitfalls"
---

### 1. Using Preview Features Without Enabling Them
Many modern features (like implicit classes) are "Preview Features" in Java 21/22. You might need to compile with `--enable-preview`.

### 2. Overusing `_`
Only use `_` when you *genuinely* don't need the variable. If you might need it for debugging later, give it a name.

### 3. Confusion with `var`
`var` infers the type. `_` ignores the value.
*   `var x = 10;` (Type is int, value is 10)
*   `int _ = 10;` (Value 10 is ignored)
