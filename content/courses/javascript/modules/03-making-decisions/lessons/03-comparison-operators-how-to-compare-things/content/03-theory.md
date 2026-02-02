---
type: "THEORY"
title: "The Comparison Toolkit"
---

Comparison operators take two values and return a `true` or `false` result.

### The Standard Operators

| Symbol | Meaning | Example | Result |
| :--- | :--- | :--- | :--- |
| `===` | Strict Equal | `5 === 5` | `true` |
| `!==` | Strict Not Equal | `5 !== 10` | `true` |
| `>` | Greater Than | `10 > 5` | `true` |
| `<` | Less Than | `5 < 10` | `true` |
| `>=` | Greater Than or Equal | `10 >= 10` | `true` |
| `<=` | Less Than or Equal | `5 <= 10` | `true` |

### Comparing Strings
You can also compare text!
*   `'apple' === 'apple'` is `true`.
*   `'Apple' === 'apple'` is **`false`** (JavaScript is case-sensitive).
*   `'a' < 'b'` is `true` (JavaScript uses alphabetical order for comparisons).

### Strictness Matters
In this course, we always use the "triple" symbols (`===` and `!==`). These are **Strict** operators. They ensure that both the **value** and the **type** are the same.
*   `5 === "5"` is `false` because one is a number and the other is a string.

(We will dive deeper into "Loose" vs "Strict" in the next lesson!)
