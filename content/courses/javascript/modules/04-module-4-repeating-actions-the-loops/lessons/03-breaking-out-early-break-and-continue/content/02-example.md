---
type: "EXAMPLE"
title: "Code Example"
---

See the code example above demonstrating Code Example.

```javascript
// BREAK - exit the loop early
console.log('--- Using break ---');
for (let i = 1; i <= 10; i++) {
  if (i === 5) {
    console.log('Found 5! Stopping.');
    break;  // Exit the entire loop
  }
  console.log(i);
}
console.log('Loop finished');
// Output: 1, 2, 3, 4, Found 5! Stopping., Loop finished

// CONTINUE - skip to next iteration
console.log('--- Using continue ---');
for (let i = 1; i <= 10; i++) {
  if (i % 2 === 0) {  // If even number
    continue;  // Skip the rest, go to next iteration
  }
  console.log(i);  // Only odd numbers print
}
// Output: 1, 3, 5, 7, 9

// Practical: searching for a value
let numbers = [5, 8, 12, 15, 20, 25];
let target = 15;
let found = false;

for (let i = 0; i < numbers.length; i++) {
  if (numbers[i] === target) {
    console.log('Found ' + target + ' at position ' + i);
    found = true;
    break;  // No need to keep searching!
  }
}

if (!found) {
  console.log(target + ' not found');
}

// Skip invalid data
for (let i = 1; i <= 5; i++) {
  if (i === 3) {
    console.log('Skipping 3');
    continue;
  }
  console.log('Processing: ' + i);
}
// Processes 1, 2, skips 3, processes 4, 5
```
