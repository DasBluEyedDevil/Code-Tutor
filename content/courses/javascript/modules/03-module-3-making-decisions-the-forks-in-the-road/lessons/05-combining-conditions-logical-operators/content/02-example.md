---
type: "EXAMPLE"
title: "Combining Conditions"
---

```javascript
let userAge = 25;
let hasSubscription = true;
let isBanned = false;

// 1. The AND Operator (&&)
// All parts must be true
if (userAge >= 18 && hasSubscription) {
    console.log("Welcome to the Premium content!");
}

// 2. The OR Operator (||)
// At least one part must be true
let isWeekend = true;
let isHoliday = false;

if (isWeekend || isHoliday) {
    console.log("The office is closed.");
}

// 3. The NOT Operator (!)
// Flips the boolean
if (!isBanned) {
    console.log("User is allowed to post comments.");
}

// 4. Complex Combination
// We use ( ) to group logic just like in math
if ((userAge > 13 || hasParentalConsent) && !isBanned) {
    console.log("Access granted to the game server.");
}
```