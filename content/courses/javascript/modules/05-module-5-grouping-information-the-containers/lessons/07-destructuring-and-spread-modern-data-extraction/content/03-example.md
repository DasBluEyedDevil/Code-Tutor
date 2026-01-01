---
type: "EXAMPLE"
title: "Array Destructuring"
---

Array destructuring extracts values by POSITION rather than by name. The first variable gets the first element, second variable gets the second element, and so on.

```javascript
// === BASIC ARRAY DESTRUCTURING ===
let colors = ['red', 'green', 'blue', 'yellow', 'purple'];

// Old way
let first = colors[0];
let second = colors[1];
let third = colors[2];

// New way - destructure by position
let [primary, secondary, tertiary] = colors;
console.log(primary);    // 'red'
console.log(secondary);  // 'green'
console.log(tertiary);   // 'blue'

// === SKIPPING ELEMENTS ===
// Use empty commas to skip positions
let numbers = [1, 2, 3, 4, 5];

let [firstNum, , thirdNum, , fifthNum] = numbers;
console.log(firstNum);   // 1
console.log(thirdNum);   // 3 (skipped 2)
console.log(fifthNum);   // 5 (skipped 4)

// === SWAPPING VALUES (Classic Interview Question!) ===
let a = 10;
let b = 20;
console.log('Before:', a, b);  // Before: 10 20

// Old way - needs a temporary variable
// let temp = a; a = b; b = temp;

// New way - elegant one-liner!
[a, b] = [b, a];
console.log('After:', a, b);   // After: 20 10

// Works with more variables too!
let x = 1, y = 2, z = 3;
[x, y, z] = [z, x, y];  // Rotate values
console.log(x, y, z);   // 3 1 2

// === DEFAULT VALUES ===
let sparse = [100];

let [val1, val2 = 'default', val3 = 'also default'] = sparse;
console.log(val1);  // 100
console.log(val2);  // 'default' (no second element)
console.log(val3);  // 'also default' (no third element)

// === REST ELEMENTS: Collect remaining items ===
let scores = [95, 87, 92, 78, 85, 90];

// Get first score, collect the rest
let [highest, ...remaining] = scores;
console.log(highest);    // 95
console.log(remaining);  // [87, 92, 78, 85, 90]

// Practical: separate first from rest
let [head, ...tail] = [1, 2, 3, 4, 5];
console.log(head);  // 1
console.log(tail);  // [2, 3, 4, 5]

// Note: rest element MUST be last!
// let [...first, last] = array;  // SYNTAX ERROR!

// === PRACTICAL: Multiple Return Values ===
function getMinMax(numbers) {
  let min = Math.min(...numbers);
  let max = Math.max(...numbers);
  return [min, max];  // Return array
}

let values = [5, 2, 9, 1, 7];
let [minimum, maximum] = getMinMax(values);
console.log('Min:', minimum);  // Min: 1
console.log('Max:', maximum);  // Max: 9

// === PRACTICAL: Split and Destructure ===
let dateString = '2024-12-25';
let [year, month, day] = dateString.split('-');
console.log(year, month, day);  // 2024 12 25
```
