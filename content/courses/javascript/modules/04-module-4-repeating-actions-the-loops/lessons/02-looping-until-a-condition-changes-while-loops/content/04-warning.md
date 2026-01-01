---
type: "WARNING"
title: "The While Loop Trap"
---

### 1. The Missing Update
This is the #1 mistake with `while` loops. 
```javascript
let count = 0;
while (count < 5) {
    console.log("Stuck!");
    // Forgot count++;
}
```
Because `count` never changes, it will always be less than 5. The loop will never end. 
*   **Rule:** Always make sure your loop body contains code that eventually makes the condition `false`.

### 2. The Semi-Colon Death
Putting a semicolon after the `while` condition.
```javascript
while (loading === true); {
   // This code block is NOT part of the loop!
}
```
This tells JavaScript to loop the "empty space" after the `;` forever, effectively freezing your program.

### 3. Infinite "Falsy" Values
If your condition checks for a specific value, make sure it’s possible to reach that value. If you're checking `while (score !== 10)`, but your code adds 3 to the score every time (1, 4, 7, 10, 13...), it will land on 10 and stop. But if you start at 2 (2, 5, 8, 11...), you'll **jump over** 10 and loop forever!
