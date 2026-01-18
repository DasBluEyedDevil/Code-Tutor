---
type: "THEORY"
title: "The Lifecycle of a Variable"
---

### 1. Declaration vs. Initialization
Creating a variable happens in two steps (often done at the same time):
*   **Declaration:** `let score;` — You tell the computer "I need a box named score." Currently, the box is empty (`undefined`).
*   **Initialization:** `score = 10;` — You put a value in the box for the first time.

You can do both at once: `let score = 10;`

### 2. `let` vs. `const`: The Rule of Thumb
Always use `const` by default. Only use `let` if you know for a fact the value needs to be reassigned. This makes your code safer because it prevents accidental changes to data that should stay permanent.

### 3. What about `var`?
You might see `var` in older tutorials. It was the original way to create variables in JavaScript. However, it has "quirky" behaviors that often lead to bugs (like being accessible before it's even declared!).

**Modern JavaScript developers avoid `var` entirely.** Stick to `let` and `const`.

### 4. Naming Rules (The Fine Print)
*   **Camel Case:** JavaScript uses `camelCase` (first word lowercase, subsequent words capitalized: `userAccountBalance`).
*   **Case Sensitivity:** `myVariable` and `myvariable` are two completely different boxes.
*   **Allowed Characters:** Names can start with a letter, `$`, or `_`. They cannot start with a number or contain spaces.
*   **Reserved Words:** You can't name a variable `let`, `const`, `function`, or `if`.
