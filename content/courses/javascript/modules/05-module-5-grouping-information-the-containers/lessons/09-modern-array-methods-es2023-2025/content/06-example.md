---
type: "EXAMPLE"
title: "Array.with() - Immutable Element Replacement"
---

Array.with() replaces an element at a specific index and returns a NEW array, leaving the original unchanged. It's the immutable alternative to arr[index] = value assignment.

```javascript
// Array.with() replaces an element at an index and returns a NEW array
// Like bracket assignment, but without mutating the original!

let colors = ['red', 'green', 'blue', 'yellow'];

// OLD WAY: Mutates the original array!
colors[1] = 'purple';
console.log(colors);  // ['red', 'purple', 'blue', 'yellow'] - Changed!

// Reset
colors = ['red', 'green', 'blue', 'yellow'];

// NEW WAY with .with() - Original preserved!
let newColors = colors.with(1, 'purple');
console.log(newColors);  // ['red', 'purple', 'blue', 'yellow']
console.log(colors);     // ['red', 'green', 'blue', 'yellow'] - Unchanged!

// Works with negative indices too (like at())
let fruits = ['apple', 'banana', 'cherry'];
let updated = fruits.with(-1, 'cranberry');  // Replace last element
console.log(updated);  // ['apple', 'banana', 'cranberry']
console.log(fruits);   // ['apple', 'banana', 'cherry'] - Original safe!

// Chain with other immutable methods
let numbers = [5, 2, 8, 1, 9];
let result = numbers
  .with(0, 100)           // Replace first with 100
  .toSorted((a, b) => a - b)  // Sort ascending
  .toReversed();          // Reverse
console.log(result);   // [100, 9, 8, 2, 1]
console.log(numbers);  // [5, 2, 8, 1, 9] - Original untouched!

// Practical: Update item in React state
// React requires immutable updates - .with() is perfect!
let todos = [
  { id: 1, text: 'Buy milk', done: false },
  { id: 2, text: 'Clean room', done: false },
  { id: 3, text: 'Exercise', done: false }
];

// Mark second todo as complete (immutably)
let indexToUpdate = 1;
let updatedTodos = todos.with(indexToUpdate, {
  ...todos[indexToUpdate],
  done: true
});
console.log(updatedTodos[1].done);  // true
console.log(todos[1].done);         // false - Original unchanged!
```
