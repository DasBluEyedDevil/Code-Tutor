---
type: "EXAMPLE"
title: "The Order of Execution"
---

```javascript
// 1. Synchronous Code (One by one)
console.log("Step 1: Boiling water...");
console.log("Step 2: Adding noodles...");
console.log("Step 3: Meal is ready!");

console.log("-------------------");

// 2. Asynchronous Code (The Waiting Game)
console.log("A: Ordering a custom pizza...");

// setTimeout simulates a task that takes time (2 seconds)
setTimeout(() => {
    console.log("B: PIZZA IS READY! (Buzzer goes off)");
}, 2000);

console.log("C: Browsing the bookstore while I wait...");
console.log("D: Buying a new pen...");

// Note the output order! 
// Even though B is written first, C and D happen before it 
// because JavaScript doesn't wait around for the timer.
```