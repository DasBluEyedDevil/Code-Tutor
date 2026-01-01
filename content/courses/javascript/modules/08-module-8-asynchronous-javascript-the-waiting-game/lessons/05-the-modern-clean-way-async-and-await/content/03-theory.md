---
type: "THEORY"
title: "The Mechanics of Async/Await"
---

Introduced in ES2017, `async/await` is "Syntactic Sugar" for Promises. It doesn't change how JavaScript works under the hood, but it drastically improves how we write it.

### 1. The `async` keyword
When you put `async` before a function declaration:
*   It tells JavaScript that this function will handle asynchronous tasks.
*   The function **always** returns a Promise. If you return a string like `"Hi"`, JavaScript automatically wraps it in a resolved Promise: `Promise.resolve("Hi")`.

### 2. The `await` keyword
The `await` keyword can only be used inside an `async` function (with one modern exception: Top-Level Await in modules).
*   When JavaScript hits an `await` line, it "pauses" the execution of **that specific function**.
*   Crucially, it **does not** pause the whole program. The event loop can still process other things while this function is waiting.
*   Once the Promise resolves, the function "wakes up" and continues with the result.

### 3. Error Handling
In the old Promise style, we used `.catch()`. With `async/await`, we use the standard JavaScript `try...catch` blocks. This allows us to use the same error-handling logic for both synchronous and asynchronous errors!

### 4. Top-Level Await
In modern JavaScript environments (like Node.js 14+ and all modern browsers) using ES Modules, you can use `await` at the very top of your file without wrapping it in an `async` function. This is perfect for loading initial configuration data.
