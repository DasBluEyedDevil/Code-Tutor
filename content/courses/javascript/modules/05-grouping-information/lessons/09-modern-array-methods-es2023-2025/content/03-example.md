---
type: "EXAMPLE"
title: "Non-Mutating Sort and Reverse"
---

The toSorted() and toReversed() methods work like sort() and reverse(), but return NEW arrays instead of modifying the original. This is safer and essential for frameworks like React that require immutable state updates.

```javascript
// THE PROBLEM: sort() and reverse() mutate the original array!
let numbers = [3, 1, 4, 1, 5, 9, 2, 6];
let sorted = numbers.sort((a, b) => a - b);
console.log(sorted);   // [1, 1, 2, 3, 4, 5, 6, 9]
console.log(numbers);  // [1, 1, 2, 3, 4, 5, 6, 9] - ORIGINAL CHANGED!

// THE SOLUTION: toSorted() returns a NEW array
let numbers2 = [3, 1, 4, 1, 5, 9, 2, 6];
let sorted2 = numbers2.toSorted((a, b) => a - b);
console.log(sorted2);   // [1, 1, 2, 3, 4, 5, 6, 9]
console.log(numbers2);  // [3, 1, 4, 1, 5, 9, 2, 6] - ORIGINAL PRESERVED!

// Same with reverse!
let letters = ['a', 'b', 'c', 'd'];

// OLD: reverse() mutates
let reversed1 = letters.reverse();
console.log(letters);  // ['d', 'c', 'b', 'a'] - original changed!

// Reset and try toReversed()
letters = ['a', 'b', 'c', 'd'];
let reversed2 = letters.toReversed();
console.log(reversed2);  // ['d', 'c', 'b', 'a']
console.log(letters);    // ['a', 'b', 'c', 'd'] - original preserved!

// Practical: showing sorted data without affecting original
let products = [
  { name: 'Laptop', price: 1000 },
  { name: 'Mouse', price: 25 },
  { name: 'Keyboard', price: 75 }
];

// Sort by price for display
let byPrice = products.toSorted((a, b) => a.price - b.price);
console.log('Sorted by price:', byPrice.map(p => p.name));  // Mouse, Keyboard, Laptop
console.log('Original order:', products.map(p => p.name));  // Laptop, Mouse, Keyboard
```
