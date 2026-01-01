---
type: "THEORY"
title: "Breaking Down the Syntax"
---

Understanding for...of:

for (let item of array) {
     │   │    │    │
     │   │    │    └─ The array to loop through
     │   │    └────── The 'of' keyword
     │   └─────────── Variable to hold each item (you choose the name)
     └─────────────── let (or const)
  // Use 'item' here
}

Comparing loop types:

// Regular for loop - use when you need the INDEX
for (let i = 0; i < arr.length; i++) {
  console.log('Position ' + i + ': ' + arr[i]);
}

// for...of loop - use when you just need the ITEMS
for (let item of arr) {
  console.log(item);  // Don't care about position
}

// while loop - use when you don't know how many times
while (notDone) {
  // Keep going until condition changes
}

When to use for...of:
✓ Going through all items in an array
✓ Don't need the index/position
✓ Cleaner, more readable code

When NOT to use for...of:
✗ Need the index number
✗ Need to modify the array while looping
✗ Looping a specific number of times (use regular for)

Note: You can use 'const' instead of 'let' in for...of:
for (const fruit of fruits) {
  // fruit is reassigned each iteration, so const works!
}

Array.length:
- Every array has a .length property
- fruits.length → 3 (number of items)
- Use in regular for loops: i < arr.length