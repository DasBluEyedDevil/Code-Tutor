---
type: "WARNING"
title: "Async/Await Pitfalls"
---

### 1. Forgetting the `await`
This is the most common bug in modern JavaScript.
```javascript
const user = fetchUser(); // FORGOT 'await'!
console.log(user.name); // ERROR: "user" is a Promise, not the data!
```
*   **Result:** Your variable contains a "Pending Promise" instead of the actual data. Always remember to `await` the results!

### 2. The Sequential Trap
If you have three independent tasks, don't `await` them one by one.
*   **Wrong:** 
    ```javascript
    await task1(); // 1 sec
    await task2(); // 1 sec
    await task3(); // 1 sec
    // Total time: 3 seconds
    ```
*   **Right:**
    ```javascript
    await Promise.all([task1(), task2(), task3()]);
    // Total time: 1 second (runs in parallel!)
    ```

### 3. Forgetting the `async`
If you try to use `await` inside a normal function, JavaScript will throw a syntax error. 
*   **Fix:** Ensure the parent function has the `async` keyword.

### 4. Swallowing Errors
If you use `try/catch` but don't do anything in the `catch` block, you might "swallow" a critical error. Your app will fail silently, and you'll have no idea why. Always at least `console.error` your caught mistakes.
