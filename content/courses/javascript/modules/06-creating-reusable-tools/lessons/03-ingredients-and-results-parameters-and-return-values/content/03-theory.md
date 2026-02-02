---
type: "THEORY"
title: "Data Flow in Functions"
---

Understanding how data enters and leaves a function is the key to writing clean code.

### 1. Parameters vs. Arguments
*   **Parameters:** The placeholders you define in the function signature (e.g., `price` and `quantity`).
*   **Arguments:** The actual values you pass in when you call it (e.g., `19.99` and `3`).

### 2. The `return` Keyword
When a function hits a `return` statement:
1.  It calculates the value following the word `return`.
2.  It immediately **stops** running the rest of the function.
3.  It replaces the function call with that value.

If you don't use `return`, your function returns `undefined` by default.

### 3. Default Parameters
You can provide "fallback" values for your parameters. This makes your functions more flexible and prevents `undefined` errors if a user forgets to provide an input.
`function greet(name = "User") { ... }`

### 4. Returning Multiple Values
Technically, a function can only return **one thing**. However, that "one thing" can be an **Array** or an **Object**, which can contain as much data as you want.
```javascript
function getStats() {
    return {
        strength: 10,
        agility: 15
    };
}
```
