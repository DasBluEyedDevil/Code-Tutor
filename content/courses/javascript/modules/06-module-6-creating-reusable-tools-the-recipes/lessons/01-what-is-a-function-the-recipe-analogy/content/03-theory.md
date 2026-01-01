---
type: "THEORY"
title: "The Anatomy of a Function"
---

A function in JavaScript consists of several parts:

```javascript
function name(parameter) {
    // code to run
}
```

### 1. The `function` Keyword
This tells the computer: "I am about to define a reusable tool."

### 2. The Name
Following the same rules as variables, function names should be descriptive and use `camelCase`. A good habit is to start with a verb (e.g., `calculateTotal`, `fetchData`, `isValid`).

### 3. Parameters `( ... )`
These are the "input slots" for your function. They act like local variables that only exist inside the function. When you call the function, you pass in **Arguments** that fill these slots.

### 4. The Body `{ ... }`
The code between the curly braces is what actually happens when the function is called. This code is "stored" but not executed until the call happens.

### 5. Hoisting (Technical Note)
In JavaScript, functions declared with the `function` keyword are "hoisted" to the top of their file. This means you can technically call a function on line 1 even if you don't define it until line 10. However, for readability, it is best to define your tools before you use them!
