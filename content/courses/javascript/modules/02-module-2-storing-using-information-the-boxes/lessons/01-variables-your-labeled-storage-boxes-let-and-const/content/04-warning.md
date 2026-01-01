---
type: "WARNING"
title: "Common Pitfalls & Errors"
---

### 1. Assignment to Constant Variable
If you try to change a `const`, your program will crash with a `TypeError`. 
*   **Fix:** If the value needs to change, use `let`.

### 2. Using Before Declaration (The "Dead Zone")
In JavaScript, you cannot use a `let` or `const` variable before the line where you create it.
```javascript
console.log(myNumber); // ERROR!
let myNumber = 5;
```
This is called the **Temporal Dead Zone**. The computer knows the variable exists, but it refuses to let you touch it until the declaration line is reached.

### 3. Redeclaring Variables
You cannot create two boxes with the same name in the same scope.
```javascript
let user = 'Bob';
let user = 'Alice'; // ERROR! "user" has already been declared
```
If you want to change Bob to Alice, just use `user = 'Alice';` without the `let`.

### 4. Forgetting Quotes for Strings
If you write `let name = Alice;` without quotes, JavaScript looks for another variable named `Alice`. If it doesn't find one, it crashes with a `ReferenceError: Alice is not defined`.
