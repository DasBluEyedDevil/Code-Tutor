---
type: "EXAMPLE"
title: "The Basic 'if' Statement"
---

```javascript
let userBalance = 50.00;
let itemPrice = 19.99;

console.log("Starting purchase...");

// 1. A basic check
if (userBalance >= itemPrice) {
    // Everything inside the { } only runs if the condition is TRUE
    console.log("Transaction approved!");
    userBalance = userBalance - itemPrice;
    console.log(`Remaining balance: $${userBalance}`);
}

// 2. What if the condition is FALSE?
let age = 16;
if (age >= 18) {
    console.log("Access granted to the Adult section.");
    // This line will be completely skipped!
}

console.log("Application finished.");

// 3. One-line shortcut (use sparingly)
if (isLoggedIn) console.log("Welcome back!");
```