---
type: "THEORY"
title: "Catching the Future"
---

Asynchronous error handling is different because it involves the **Event Loop**.

### 1. Promises and `.catch()`
A Promise represents an operation that will finish later. If that operation fails, the Promise enters the "Rejected" state. To handle this, you attach a `.catch()` method. 
*   **The Chain:** If you have multiple `.then()` calls, a single `.catch()` at the bottom will catch an error from **any** of the steps above it.

### 2. Async/Await and `try/catch`
The `await` keyword effectively "unwraps" a Promise. 
*   If the Promise resolves, `await` returns the value.
*   If the Promise rejects, `await` **throws** an error.
This is why we can use standard `try/catch` blocks with `async/await`. It makes asynchronous error handling feel exactly like synchronous error handling.

### 3. Unhandled Rejections
If a Promise fails and you don't have a `.catch()` or a `try/catch` waiting for it, JavaScript triggers an **Unhandled Rejection**. 
*   In a browser, this shows up as a red error in the console.
*   In Node.js, this can sometimes crash the entire process.

### 4. `Promise.allSettled`
Sometimes you don't want a single failure to stop everything. While `Promise.all` fails immediately if **any** task fails, `Promise.allSettled` waits for **all** tasks to finish, and then gives you a report on which ones succeeded and which ones failed.
