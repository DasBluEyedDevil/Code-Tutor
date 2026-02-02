---
type: "WARNING"
title: "Error Object Pitfalls"
---

### 1. Throwing Strings
Technically, JavaScript allows you to `throw "Help!"` or `throw 404`. 
*   **Why it's bad:** These primitive values don't have a `.name` or a `.stack` property. When you catch them, you lose all the helpful debugging information.
*   **Rule:** Always throw an `Error` object or a subclass of `Error`.

### 2. Stack Trace Leaks
The `.stack` property contains a lot of information about your computer's file structure and your code's internal logic. 
*   **Security Risk:** Never show the raw stack trace to a user in a production app. It can give hackers clues about how to attack your system. Only log it to your internal developer console.

### 3. TypeError vs. ReferenceError
It's easy to mix these up.
*   If the variable **doesn't exist**, it's a `ReferenceError`.
*   If the variable **exists** but you used it wrong (e.g., trying to use `undefined` like an object), it's a `TypeError`.

### 4. Catching Everything
If you use `instanceof` to check for specific errors, make sure you still have a "default" catch at the end for unexpected problems you didn't plan for!
