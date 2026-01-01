---
type: "EXAMPLE"
title: "Code Example"
---

See the code example above demonstrating Code Example.

```javascript
// Creating an array
let fruits = ['apple', 'banana', 'cherry'];

// Accessing items by index (position)
console.log(fruits[0]);  // apple (first item)
console.log(fruits[1]);  // banana (second item)
console.log(fruits[2]);  // cherry (third item)

// Arrays can hold different types
let mixed = ['text', 42, true, null];
console.log(mixed[1]);  // 42

// Empty array
let empty = [];

// Array length (how many items)
console.log(fruits.length);  // 3

// Last item (using length)
let lastFruit = fruits[fruits.length - 1];
console.log(lastFruit);  // cherry

// Changing an item
fruits[1] = 'blueberry';
console.log(fruits);  // ['apple', 'blueberry', 'cherry']

// Loop through array
for (let i = 0; i < fruits.length; i++) {
  console.log('Item ' + i + ': ' + fruits[i]);
}
```
