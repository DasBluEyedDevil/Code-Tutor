---
type: "EXAMPLE"
title: "Code Example"
---

See the code example above demonstrating Code Example.

```javascript
// JavaScript: No type checking (risky)
function addNumbers(a, b) {
  return a + b;
}

console.log(addNumbers(5, 3));        // 8 ✓
console.log(addNumbers('5', '3'));    // '53' (string concatenation - oops!)
console.log(addNumbers(5, 'hello')); // '5hello' (probably not what you wanted)

// TypeScript: Type checking (safer)
function addNumbersTyped(a: number, b: number): number {
  return a + b;
}

console.log(addNumbersTyped(5, 3));        // 8 ✓
// console.log(addNumbersTyped('5', '3'));    // ERROR: Type 'string' is not assignable to type 'number'
// console.log(addNumbersTyped(5, 'hello')); // ERROR: Argument of type 'string' is not assignable to parameter of type 'number'

// TypeScript catches mistakes BEFORE you run the code!
```
