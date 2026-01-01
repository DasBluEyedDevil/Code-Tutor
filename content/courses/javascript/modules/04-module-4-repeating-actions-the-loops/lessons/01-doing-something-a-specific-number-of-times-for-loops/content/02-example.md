---
type: "EXAMPLE"
title: "Code Example"
---

See the code example above demonstrating Code Example.

```javascript
// Basic for loop - count from 0 to 4
for (let i = 0; i < 5; i++) {
  console.log('Count: ' + i);
}
// Displays: Count: 0, Count: 1, Count: 2, Count: 3, Count: 4

// Count from 1 to 10
for (let i = 1; i <= 10; i++) {
  console.log(i);
}

// Count by twos (even numbers)
for (let i = 0; i <= 10; i += 2) {
  console.log(i);  // 0, 2, 4, 6, 8, 10
}

// Count backwards
for (let i = 10; i >= 1; i--) {
  console.log(i);
}
console.log('Blast off!');

// Practical example: calculate total
let total = 0;
for (let i = 1; i <= 5; i++) {
  total += i;  // Same as: total = total + i
}
console.log('Sum of 1 to 5: ' + total);  // 15
```
