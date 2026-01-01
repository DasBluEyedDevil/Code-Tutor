---
type: "EXAMPLE"
title: "includes() vs indexOf() - Simple Existence Checks"
---

includes() returns true/false for existence. indexOf() returns the position (or -1). The key difference is how they handle NaN - includes() can find NaN, indexOf() cannot!

```javascript
// === includes() - Returns true or false ===
let fruits = ['apple', 'banana', 'cherry', 'date'];

console.log(fruits.includes('banana'));  // true
console.log(fruits.includes('mango'));   // false

// With numbers
let numbers = [1, 2, 3, 4, 5];
console.log(numbers.includes(3));  // true
console.log(numbers.includes(10)); // false

// === indexOf() - Returns position or -1 ===
console.log(fruits.indexOf('cherry'));  // 2
console.log(fruits.indexOf('mango'));   // -1 (not found)

// === Second parameter: fromIndex ===
let letters = ['a', 'b', 'c', 'a', 'b', 'c'];

// Start searching from index 2
console.log(letters.indexOf('a', 2));     // 3 (second 'a')
console.log(letters.includes('a', 2));    // true (finds second 'a')

// Start from index 4 - 'a' not found after that
console.log(letters.indexOf('a', 4));     // -1
console.log(letters.includes('a', 4));    // false

// === Critical Difference: NaN Handling ===
let values = [1, 2, NaN, 4, 5];

// includes() CAN find NaN
console.log(values.includes(NaN));  // true

// indexOf() CANNOT find NaN (uses strict equality)
console.log(values.indexOf(NaN));   // -1 (!!)

// Why? NaN === NaN is false in JavaScript!
console.log(NaN === NaN);  // false

// includes() uses SameValueZero algorithm which handles NaN
// indexOf() uses strict equality (===) which fails for NaN

// === When to use which? ===
// Use includes() when you just need true/false
if (fruits.includes('banana')) {
  console.log('We have bananas!');
}

// Use indexOf() when you need the position
let pos = fruits.indexOf('cherry');
if (pos !== -1) {
  console.log('Cherry is at position ' + pos);
}

// === With objects - Reference Equality Trap! ===
let users = [{ name: 'Alice' }, { name: 'Bob' }];
let alice = { name: 'Alice' };

// This is FALSE! Different object references
console.log(users.includes(alice));  // false
console.log(users.indexOf(alice));   // -1

// The object in the array is not the same object as 'alice'
// They look the same but are different objects in memory
// Use find() for searching objects by property:
let found = users.find(u => u.name === 'Alice');
console.log(found);  // { name: 'Alice' }
```
