---
type: "EXAMPLE"
title: "Working with Different Data Types"
---

```javascript
// 1. STRINGS (Text)
const firstName = "Alice";
const lastName = 'Smith';
// Template Literal (Modern JS using backticks `` ` ``)
const fullName = `${firstName} ${lastName}`;

console.log(`Hello, my name is ${fullName}`);

// 2. NUMBERS
const price = 19.99;
const quantity = 3;
const total = price * quantity;

console.log(`The total cost is: $${total}`);

// Special Number Values
console.log(10 / 0);      // Infinity
console.log("Hello" * 5); // NaN (Not a Number)

// 3. BOOLEANS (Logic)
const isLoggedIn = true;
const hasPremiumAccount = false;

console.log(`User logged in: ${isLoggedIn}`);

// 4. NULL vs UNDEFINED
let userJob;              // Declared but no value yet: undefined
let userAddress = null;   // Explicitly set to empty: null

console.log(userJob);     // undefined
console.log(userAddress); // null
```

### Expected Output

```
Hello, my name is Alice Smith
The total cost is: $59.97
Infinity
NaN
User logged in: true
undefined
null
```
