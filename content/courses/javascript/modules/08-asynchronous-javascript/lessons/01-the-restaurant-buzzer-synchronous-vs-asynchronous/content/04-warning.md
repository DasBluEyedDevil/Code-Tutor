---
type: "WARNING"
title: "Concurrency Cautions"
---

### 1. Blocking the Thread
If you write a piece of code that does a billion calculations synchronously, you will **block** the event loop. The "Buzzer" (asynchronous task) might go off, but the Event Loop can't move it to the stack because the stack is busy counting to a billion. 
*   **Result:** Your website freezes, and buttons stop working.

### 2. The "0ms" Misconception
```javascript
setTimeout(() => console.log("A"), 0);
console.log("B");
```
Even with 0 milliseconds, the output will be **B then A**. This is because `setTimeout` is *always* asynchronous and must go through the "Waiting Room" (Queue), which can only be entered after the main code is finished.

### 3. Race Conditions
When you have multiple asynchronous tasks running at once, you can't always predict which one will finish first. If Task A depends on data from Task B, but Task A finishes faster, your program might crash. 
*   **Fix:** We use **Promises** and **Async/Await** to manage the order of these tasks (coming up in the next lessons!).

### 4. Callback Hell
In the early days, programmers would put functions inside functions inside functions to handle async tasks. This created a "pyramid" of code that was impossible to read. Modern JavaScript has much better ways to handle this.
