---
type: "WARNING"
title: "Async Error Pitfalls"
---

### 1. The `await` Requirement
This is the #1 mistake. If you don't `await` a promise inside a `try` block, the error will "escape" the block before the catch has a chance to run.
```javascript
try {
    return fetchUserData(); // WRONG! Forgot 'await'
} catch (e) {
    // This will NOT catch network errors from fetchUserData
}
```
*   **Fix:** Always `await` the promise you want to protect.

### 2. Silent Failures in `.forEach`
The `.forEach` loop does not wait for promises.
```javascript
// DANGER: try/catch won't work here!
users.forEach(async (u) => {
    try {
        await save(u);
    } catch (e) {
        console.log("Caught!"); // Might not run when you expect
    }
});
```
*   **Fix:** Use a `for...of` loop if you need to catch errors sequentially.

### 3. Throwing in a Promise vs. `async`
Inside a manual `new Promise`, you should use the `reject()` function instead of `throw`. While `throw` works in some cases, `reject` is the standard way to communicate failure in the promise constructor.

### 4. Swallowing Async Errors
If you use `.catch(() => {})` just to make the red error go away in the console, you are making your app impossible to debug. At the very least, log the error to an external service or the console.
