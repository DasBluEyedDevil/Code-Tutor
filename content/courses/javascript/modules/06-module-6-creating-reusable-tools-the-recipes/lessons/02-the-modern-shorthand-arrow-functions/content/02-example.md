---
type: "EXAMPLE"
title: "Arrow Function Syntax"
---

```javascript
// 1. Classic Function (for comparison)
function double(n) {
    return n * 2;
}

// 2. Arrow Function (Long Form)
const doubleArrow = (n) => {
    return n * 2;
};

// 3. Arrow Function (Short Form / Implicit Return)
// If there's only one line of code, we can remove { } and 'return'
const doubleShort = n => n * 2;

console.log(doubleShort(10)); // 20

// 4. Multiple Parameters
// If you have 0 or 2+ parameters, you MUST use ( )
const add = (a, b) => a + b;
console.log(add(5, 5)); // 10

const sayHi = () => console.log("Hi!");
sayHi();

// 5. Practical usage in a loop
const numbers = [1, 2, 3];
const squares = numbers.map(x => x * x);
console.log(squares); // [1, 4, 9]
```