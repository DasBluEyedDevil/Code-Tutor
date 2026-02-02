---
type: "EXAMPLE"
title: "find() and findIndex() - Locate First Match"
---

find() returns the first element that matches your condition. findIndex() returns its position (index). Both stop searching once a match is found, making them efficient for large arrays.

```javascript
// === find() - Returns the ELEMENT itself ===
let users = [
  { id: 1, name: 'Alice', role: 'admin' },
  { id: 2, name: 'Bob', role: 'user' },
  { id: 3, name: 'Charlie', role: 'admin' },
  { id: 4, name: 'Diana', role: 'user' }
];

// Find first admin
let firstAdmin = users.find(user => user.role === 'admin');
console.log(firstAdmin);  // { id: 1, name: 'Alice', role: 'admin' }

// Find user by ID
let user2 = users.find(user => user.id === 2);
console.log(user2.name);  // 'Bob'

// When nothing is found - returns undefined
let manager = users.find(user => user.role === 'manager');
console.log(manager);  // undefined

// IMPORTANT: Always check for undefined!
if (manager) {
  console.log(manager.name);
} else {
  console.log('No manager found');
}

// === findIndex() - Returns the POSITION (index) ===
let numbers = [10, 25, 30, 45, 50];

// Find position of first number > 25
let index = numbers.findIndex(n => n > 25);
console.log(index);  // 2 (the value 30 is at index 2)

// When nothing is found - returns -1
let notFound = numbers.findIndex(n => n > 100);
console.log(notFound);  // -1

// Practical: Remove an item by finding its index
let userIndex = users.findIndex(u => u.id === 2);
if (userIndex !== -1) {
  users.splice(userIndex, 1);  // Remove Bob
}
console.log(users.length);  // 3 (Bob removed)

// === find() vs filter() - Key Difference ===
let scores = [85, 92, 78, 95, 88];

// find() returns FIRST match (or undefined)
let firstHigh = scores.find(s => s > 90);
console.log(firstHigh);  // 92 (just one value)

// filter() returns ALL matches (as array)
let allHigh = scores.filter(s => s > 90);
console.log(allHigh);  // [92, 95] (array of all matches)

// Use find() when you need just one item
// Use filter() when you need all matching items
```
