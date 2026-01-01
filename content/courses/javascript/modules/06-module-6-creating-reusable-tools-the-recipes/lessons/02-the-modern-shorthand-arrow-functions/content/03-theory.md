---
type: "THEORY"
title: "The Rules of the Arrow"
---

Arrow functions are more than just a shorter way to write code; they have a few unique rules that make them different from standard functions.

### 1. The Implicit Return
In a standard function, if you don't use the `return` keyword, the function returns `undefined`.
In an arrow function, if you omit the curly braces `{ }`, the result of the line is **automatically returned**. This is called an implicit return.

### 2. Parentheses Rules
*   **0 Parameters:** Must use empty parentheses: `() => ...`
*   **1 Parameter:** Parentheses are optional: `x => ...` or `(x) => ...`
*   **2+ Parameters:** Must use parentheses: `(x, y) => ...`

### 3. No Hoisting
Unlike standard function declarations, arrow functions are usually stored in variables (`const myFunc = ...`). This means they are **not hoisted**. You cannot call an arrow function before you define it in your code.

### 4. What about `this`?
This is a more advanced topic, but standard functions have their own `this` keyword (which changes depending on how the function is called). Arrow functions **do not have their own `this`**. They inherit it from the code surrounding them. This makes them much easier to use inside objects or when handling browser events (like button clicks).
