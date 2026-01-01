---
type: "EXAMPLE"
title: "Code Example"
---

See the code example above demonstrating Code Example.

```javascript
let student = {
  name: 'Alice',
  age: 20,
  grade: 'A',
  major: 'Computer Science'
};

// Get all property names (keys)
let keys = Object.keys(student);
console.log(keys);  // ['name', 'age', 'grade', 'major']

// Get all property values
let values = Object.values(student);
console.log(values);  // ['Alice', 20, 'A', 'Computer Science']

// Get all key-value pairs
let entries = Object.entries(student);
console.log(entries);  // [['name', 'Alice'], ['age', 20], ...]

// Loop through keys
for (let key of Object.keys(student)) {
  console.log(key + ': ' + student[key]);
}

// Loop through key-value pairs (more elegant)
for (let [key, value] of Object.entries(student)) {
  console.log(key + ': ' + value);
}

// for...in loop (older way)
for (let key in student) {
  console.log(key + ': ' + student[key]);
}

// Practical: calculate total prices
let cart = {
  laptop: 1000,
  mouse: 25,
  keyboard: 75
};

let total = 0;
for (let price of Object.values(cart)) {
  total += price;
}
console.log('Total: $' + total);  // $1100
```
