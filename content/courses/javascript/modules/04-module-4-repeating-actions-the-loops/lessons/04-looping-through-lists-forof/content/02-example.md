---
type: "EXAMPLE"
title: "Code Example"
---

See the code example above demonstrating Code Example.

```javascript
// Old way: using a regular for loop with an index
let fruits = ['apple', 'banana', 'cherry'];

for (let i = 0; i < fruits.length; i++) {
  console.log(fruits[i]);  // Need to use fruits[i] to get the item
}

// New way: for...of loop (much cleaner!)
for (let fruit of fruits) {
  console.log(fruit);  // Direct access to each item
}

// Another example: summing numbers
let numbers = [10, 20, 30, 40, 50];
let total = 0;

for (let num of numbers) {
  total += num;
}
console.log('Total: ' + total);  // 150

// Works with strings too! (string is a list of characters)
let word = 'hello';

for (let letter of word) {
  console.log(letter);  // h, e, l, l, o
}

// Practical: find if item exists
let shoppingCart = ['milk', 'eggs', 'bread', 'butter'];
let lookingFor = 'eggs';
let hasItem = false;

for (let item of shoppingCart) {
  if (item === lookingFor) {
    hasItem = true;
    break;
  }
}

console.log(hasItem ? 'Found it!' : 'Not in cart');
```
