---
type: "THEORY"
title: "The Danger of Coercion"
---

### What is Type Coercion?
Type Coercion is JavaScript's attempt to "fix" a comparison by changing the data type of one value so it matches the other.
*   When you use `==`, JavaScript thinks: "Wait, the user is comparing a Number to a String. Let me quickly convert this string into a number so the comparison works."

### Why you should avoid `==`
While it seems convenient, coercion has strange rules that are hard to remember. For example:
*   `"" == 0` is `true`.
*   `[] == 0` is `true`.
*   `" \t\r\n" == 0` is `true`.

Because of these bizarre edge cases, **professional JavaScript developers almost NEVER use `==`**.

### The Industry Standard: `===`
Strict equality (`===`) is much safer because it follows a simple rule: **Values must be identical AND Types must be identical.**
1.  Check Types: Are they both numbers? (If not, return `false`).
2.  Check Values: Are the numbers the same? (If not, return `false`).

This predictability makes your code easier to read and much harder to break.
