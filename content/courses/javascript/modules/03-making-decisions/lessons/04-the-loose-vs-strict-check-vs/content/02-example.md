---
type: "EXAMPLE"
title: "Loose vs. Strict Comparison"
---

```javascript
// 1. Loose Equality (==)
// JavaScript converts the string "10" into the number 10 automatically.
console.log(10 == "10");   // true
console.log(1 == true);    // true
console.log(0 == false);   // true
console.log(null == undefined); // true

// 2. Strict Equality (===)
// No conversion allowed. Values AND Types must match.
console.log(10 === "10");  // false (Number vs String)
console.log(1 === true);   // false (Number vs Boolean)
console.log(0 === false);  // false (Number vs Boolean)
console.log(null === undefined); // false

// 3. Strict Inequality (!==)
// This is the opposite of ===
let score = "100";
if (score !== 100) {
    console.log("The types do not match!");
}
```