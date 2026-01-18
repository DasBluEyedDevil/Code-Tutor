---
type: "PITFALL"
title: "If Statement Pitfalls"
---

### 1. The Assignment Mistake (VERY COMMON)
Using a single `=` instead of `===` inside your condition.
```javascript
let score = 0;
if (score = 10) { // WRONG! This sets score to 10 and returns 10 (which is true)
    console.log("You win!");
}
```
*   **Result:** This code will *always* run, and it will overwrite your variable. Always use `===` or `==` for comparisons.

### 2. The Missing Curly Braces
Technically, if your `if` statement only has one line, you don't need `{ }`.
```javascript
if (isAdmin)
    console.log("Welcome Admin");
    deleteEverything(); // THIS RUNS NO MATTER WHAT!
```
Without the braces, only the very next line belongs to the `if`. The `deleteEverything()` call will run for everyone.
*   **Best Practice:** Always use curly braces, even for one line.

### 3. Semicolon at the end of the 'if'
```javascript
if (age > 18); {
   console.log("Access Granted");
}
```
Notice the `;` after the condition. This tells JavaScript the `if` statement is finished immediately. The code block `{ }` will then run for everyone, regardless of age!