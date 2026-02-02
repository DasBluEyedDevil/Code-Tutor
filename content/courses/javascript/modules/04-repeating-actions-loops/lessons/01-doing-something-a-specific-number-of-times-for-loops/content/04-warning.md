---
type: "WARNING"
title: "Loop Hazards"
---

### 1. The Infinite Loop (CRITICAL)
If your condition never becomes `false`, the loop will run forever. This will freeze your computer or crash your application.
```javascript
// DANGER: i is always greater than -1
for (let i = 0; i > -1; i++) {
   console.log("I never stop!");
}
```
*   **Fix:** Always ensure your "Increment" step moves your variable toward the "Condition" being false.

### 2. The Off-By-One Error
Do you want to run 10 times? 
*   `i = 0; i < 10` runs 10 times (0 through 9).
*   `i = 1; i <= 10` runs 10 times (1 through 10).
*   `i = 0; i <= 10` runs **11 times**!
Pay close attention to whether you use `<` or `<=`.

### 3. Forgetting the Increment
If you forget the `i++`, the value of `i` never changes, which usually leads back to an **Infinite Loop**.

### 4. Semicolons in the Wrong Place
```javascript
for (let i = 0; i < 5; i++); {
    console.log("Hi");
}
```
If you put a `;` after the `for` parentheses, the loop finishes instantly without doing anything. The code block `{ }` will then run exactly once, regardless of the loop rules.
