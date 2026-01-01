---
type: "WARNING"
title: "Arrow Function Pitfalls"
---

### 1. The Braces Trap
If you add curly braces `{ }`, you **MUST** add the word `return`. 
```javascript
const add = (a, b) => { a + b }; 
console.log(add(1, 1)); // Result: undefined!
```
*   **Fix:** Either remove the `{ }` or add `return a + b;`.

### 2. Returning an Object
This is a tricky syntax error. If you want to use the short form to return an object literal, JavaScript gets confused between the object's `{ }` and the function's `{ }`.
*   **Wrong:** `const getUser = id => { id: id };` (JavaScript thinks those are code block braces)
*   **Right:** `const getUser = id => ({ id: id });` (Wrap it in parentheses to show it's a value)

### 3. Readability
Just because you can write an arrow function on one line doesn't mean you should. If your logic involves multiple steps or complex decisions, a standard function or a multi-line arrow function is much easier for your team to read and debug.

### 4. Not for everything
Arrow functions cannot be used as "Constructors" (to create new objects with the `new` keyword), and they don't have the `arguments` object that standard functions have. Use them primarily for data transformation and simple callbacks.
