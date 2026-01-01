---
type: "WARNING"
title: "Scope Slip-ups"
---

### 1. Polluting the Global Scope
If you declare everything globally, you run the risk of **Name Collisions**. Two different functions might try to use a variable called `index`, and they will overwrite each other's data.
*   **Rule:** Keep variables as "local" as possible.

### 2. Reference Errors
If you try to access a function variable from the outside, your program will crash.
```javascript
function login() {
    let username = "Alice";
}
console.log(username); // ReferenceError: username is not defined
```

### 3. Accidental Globals
If you forget to use `let` or `const`, JavaScript (in non-strict mode) will automatically create a **Global** variable for you.
```javascript
function oops() {
    count = 10; // FORGOT 'let'! This is now global.
}
```
This is a very dangerous source of bugs. Always use `const` or `let`.

### 4. Closures (A Sneak Peek)
Sometimes a function "remembers" the variables from its outer scope even after the outer scope has finished running. This is a powerful feature called a **Closure**, which we will explore in the advanced JavaScript modules!
