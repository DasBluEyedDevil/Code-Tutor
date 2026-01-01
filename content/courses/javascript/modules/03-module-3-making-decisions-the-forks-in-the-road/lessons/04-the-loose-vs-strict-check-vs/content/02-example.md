---
type: "EXAMPLE"
title: "Code Example"
---

See the code example above demonstrating Code Example.

```javascript
// The difference between == and ===

// == (loose equality) - converts types
console.log(5 == '5');     // true (converts '5' to number)
console.log(true == 1);    // true (converts true to 1)
console.log(false == 0);   // true (converts false to 0)
console.log(null == undefined);  // true (special case)

// === (strict equality) - no conversion
console.log(5 === '5');    // false (number vs string)
console.log(true === 1);   // false (boolean vs number)
console.log(false === 0);  // false (boolean vs number)
console.log(null === undefined);  // false (different types)

// Real-world example where == causes bugs
let userInput = '0';  // User typed '0' in a form

if (userInput == false) {
  console.log('This runs! But did the user mean false?');
}

if (userInput === false) {
  console.log('This does NOT run - safer!');
}

// Best practice: ALWAYS use ===
let count = 0;
if (count === 0) {
  console.log('Count is zero');
}
```
