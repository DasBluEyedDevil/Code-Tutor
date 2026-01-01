---
type: "THEORY"
title: "Breaking Down the Syntax"
---

Array syntax:

// Creating arrays
let arrayName = [item1, item2, item3];
let empty = [];

// Accessing items (zero-indexed!)
array[0]  // First item
array[1]  // Second item
array[2]  // Third item

// Index visualization:
let numbers = [10, 20, 30, 40, 50];
//  Index:     0   1   2   3   4
//  Value:    10  20  30  40  50

numbers[0] → 10
numbers[4] → 50
numbers[5] → undefined (doesn't exist)

Key properties:

1. .length - number of items
   - ['a', 'b', 'c'].length → 3
   - [].length → 0

2. Zero-indexed
   - First item: array[0]
   - Last item: array[array.length - 1]

3. Mutable (can be changed)
   - array[0] = 'new value'

4. Can hold any type
   - [1, 'text', true, {}, []]  // all valid

Common patterns:

// Last item
let last = array[array.length - 1];

// Check if empty
if (array.length === 0) {
  console.log('Empty');
}

// Loop through all items
for (let i = 0; i < array.length; i++) {
  // use array[i]
}