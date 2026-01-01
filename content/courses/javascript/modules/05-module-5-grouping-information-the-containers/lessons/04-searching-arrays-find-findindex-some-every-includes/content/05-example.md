---
type: "EXAMPLE"
title: "at() for Negative Indexing (ES2022+)"
---

The at() method provides a cleaner way to access array elements, especially from the end. While bracket notation requires length calculations for negative access, at() handles negative indices directly.

```javascript
// === The Old Way: Accessing from the end ===
let colors = ['red', 'green', 'blue', 'yellow', 'purple'];

// Get last element - clunky!
let last = colors[colors.length - 1];
console.log(last);  // 'purple'

// Get second-to-last - even clunkier!
let secondLast = colors[colors.length - 2];
console.log(secondLast);  // 'yellow'

// === The New Way: at() with negative indices ===
console.log(colors.at(-1));   // 'purple' (last)
console.log(colors.at(-2));   // 'yellow' (second-to-last)
console.log(colors.at(-3));   // 'blue' (third-to-last)

// Positive indices work too (same as bracket notation)
console.log(colors.at(0));    // 'red' (first)
console.log(colors.at(2));    // 'blue' (third)

// === Comparison: When at() is clearer ===
let history = ['action1', 'action2', 'action3', 'action4'];

// Get last action (undo feature)
// Old way:
let lastActionOld = history[history.length - 1];
// New way:
let lastAction = history.at(-1);
console.log(lastAction);  // 'action4'

// === Out of bounds behavior ===
console.log(colors.at(100));   // undefined
console.log(colors.at(-100));  // undefined
// Same as bracket notation - returns undefined for invalid indices

// === Works with strings too! ===
let greeting = 'Hello, World!';
console.log(greeting.at(0));   // 'H'
console.log(greeting.at(-1));  // '!'
console.log(greeting.at(-6));  // 'W'

// === Practical: Queue and Stack Operations ===
let queue = ['task1', 'task2', 'task3'];

// Peek at first (next to process)
let nextTask = queue.at(0);
console.log('Next task:', nextTask);  // 'task1'

let stack = ['undo1', 'undo2', 'undo3'];

// Peek at last (most recent)
let lastUndo = stack.at(-1);
console.log('Last undo:', lastUndo);  // 'undo3'

// === When to use at() vs bracket notation ===
// Use at() for:
//   - Negative indices (cleaner syntax)
//   - When index is calculated/variable and might be negative

// Use bracket notation for:
//   - Simple positive indices (arr[0], arr[1])
//   - When you need to assign values (at() is read-only)

colors[0] = 'orange';      // Works - assignment
// colors.at(0) = 'orange'; // ERROR - at() is for reading only
```
