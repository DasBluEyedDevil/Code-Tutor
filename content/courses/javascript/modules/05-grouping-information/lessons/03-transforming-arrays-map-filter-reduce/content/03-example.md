---
type: "EXAMPLE"
title: "filter() - Keep What Matches"
---

The filter() method creates a NEW array containing only the elements that pass a test (return true from the callback). The original array is never modified. The new array may be shorter than the original, or even empty if nothing matches.

```javascript
// filter() keeps elements that pass a test and returns a NEW array
// Original array is NEVER modified

// Example 1: Filter numbers by condition
let numbers = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

let evenNumbers = numbers.filter(num => num % 2 === 0);
console.log(evenNumbers);  // [2, 4, 6, 8, 10]

let bigNumbers = numbers.filter(num => num > 5);
console.log(bigNumbers);   // [6, 7, 8, 9, 10]

let smallEven = numbers.filter(num => num % 2 === 0 && num < 6);
console.log(smallEven);    // [2, 4]

// Original unchanged
console.log(numbers);  // [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]

// Example 2: Remove falsy values (null, undefined, 0, '', false)
let messy = [0, 'hello', null, 42, undefined, '', 'world', false, true];
let truthy = messy.filter(item => item);  // Truthy check
console.log(truthy);  // ['hello', 42, 'world', true]

// More explicit version:
let notNull = messy.filter(item => item !== null && item !== undefined);
console.log(notNull);  // [0, 'hello', 42, '', 'world', false, true]

// Example 3: Filter objects by property
let users = [
  { name: 'Alice', age: 25, active: true },
  { name: 'Bob', age: 17, active: true },
  { name: 'Charlie', age: 30, active: false },
  { name: 'Diana', age: 22, active: true }
];

// Get active adult users
let activeAdults = users.filter(user => user.active && user.age >= 18);
console.log(activeAdults);
// [
//   { name: 'Alice', age: 25, active: true },
//   { name: 'Diana', age: 22, active: true }
// ]

// Get inactive users
let inactive = users.filter(user => !user.active);
console.log(inactive);
// [{ name: 'Charlie', age: 30, active: false }]

// Example 4: Filter by string content
let products = ['Apple iPhone', 'Samsung Galaxy', 'Apple Watch', 'Google Pixel'];

let appleProducts = products.filter(p => p.includes('Apple'));
console.log(appleProducts);  // ['Apple iPhone', 'Apple Watch']

let startsWithS = products.filter(p => p.startsWith('S'));
console.log(startsWithS);    // ['Samsung Galaxy']

// Example 5: Edge case - empty result
let scores = [45, 62, 78, 55, 80];
let perfect = scores.filter(score => score === 100);
console.log(perfect);        // [] - empty array, no matches
console.log(perfect.length); // 0

// Example 6: Remove specific items
let tasks = ['Buy milk', 'Clean room', 'Do homework', 'Exercise'];
let taskToRemove = 'Clean room';
let remaining = tasks.filter(task => task !== taskToRemove);
console.log(remaining);  // ['Buy milk', 'Do homework', 'Exercise']
```
