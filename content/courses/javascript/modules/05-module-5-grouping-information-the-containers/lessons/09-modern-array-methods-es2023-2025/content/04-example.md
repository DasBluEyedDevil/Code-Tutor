---
type: "EXAMPLE"
title: "Non-Mutating Splice with toSpliced()"
---

The toSpliced() method works like splice() for removing and adding elements, but returns a NEW array instead of modifying the original. Perfect for immutable updates when managing lists.

```javascript
// splice() reminder: removes/adds elements AND mutates the array
let colors = ['red', 'green', 'blue', 'yellow'];
let removed = colors.splice(1, 2, 'purple');  // Remove 2 items at index 1, add 'purple'
console.log(removed);  // ['green', 'blue'] (what was removed)
console.log(colors);   // ['red', 'purple', 'yellow'] - MUTATED!

// toSpliced() does the same but returns a NEW array
let colors2 = ['red', 'green', 'blue', 'yellow'];
let newColors = colors2.toSpliced(1, 2, 'purple');
console.log(newColors);  // ['red', 'purple', 'yellow']
console.log(colors2);    // ['red', 'green', 'blue', 'yellow'] - PRESERVED!

// More examples:
let nums = [1, 2, 3, 4, 5];

// Remove 2 items starting at index 1
console.log(nums.toSpliced(1, 2));  // [1, 4, 5]

// Remove 1 item at index 2, insert 'a', 'b'
console.log(nums.toSpliced(2, 1, 'a', 'b'));  // [1, 2, 'a', 'b', 4, 5]

// Insert without removing (delete count = 0)
console.log(nums.toSpliced(2, 0, 'inserted'));  // [1, 2, 'inserted', 3, 4, 5]

// Original is always unchanged
console.log(nums);  // [1, 2, 3, 4, 5]

// Practical: removing an item by index (common in React!)
let todoList = ['Buy milk', 'Clean room', 'Do homework', 'Exercise'];
let indexToRemove = 1;  // Remove 'Clean room'

// Create new list without that item
let updatedList = todoList.toSpliced(indexToRemove, 1);
console.log(updatedList);  // ['Buy milk', 'Do homework', 'Exercise']
console.log(todoList);     // Original unchanged!
```
