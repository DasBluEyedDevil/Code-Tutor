---
type: "EXAMPLE"
title: "Spread Operator with Arrays"
---

Array spread works similarly to object spread - it 'spreads out' array elements. Perfect for combining arrays, creating copies, and converting iterables to arrays.

```javascript
// === COPYING ARRAYS ===
let original = [1, 2, 3];
let copy = [...original];

copy.push(4);
console.log(original);  // [1, 2, 3] (unchanged)
console.log(copy);      // [1, 2, 3, 4]

// === COMBINING ARRAYS ===
let first = [1, 2, 3];
let second = [4, 5, 6];

// Old way: concat
let combined1 = first.concat(second);

// New way: spread
let combined2 = [...first, ...second];
console.log(combined2);  // [1, 2, 3, 4, 5, 6]

// Can add elements in between
let combined3 = [...first, 'middle', ...second];
console.log(combined3);  // [1, 2, 3, 'middle', 4, 5, 6]

// === INSERTING ELEMENTS ===
let numbers = [1, 2, 5, 6];
// Insert 3 and 4 at position 2
let expanded = [...numbers.slice(0, 2), 3, 4, ...numbers.slice(2)];
console.log(expanded);  // [1, 2, 3, 4, 5, 6]

// === SPREAD IN FUNCTION CALLS ===
let scores = [85, 92, 78, 95, 88];

// Math.max expects individual arguments, not an array
// Math.max([85, 92, 78]) returns NaN!
// Spread the array into individual arguments:
let highest = Math.max(...scores);
let lowest = Math.min(...scores);
console.log('Highest:', highest);  // 95
console.log('Lowest:', lowest);    // 78

// === CONVERT ITERABLE TO ARRAY ===
// String to array of characters
let str = 'Hello';
let chars = [...str];
console.log(chars);  // ['H', 'e', 'l', 'l', 'o']

// Set to array
let uniqueSet = new Set([1, 2, 2, 3, 3, 3]);
let uniqueArray = [...uniqueSet];
console.log(uniqueArray);  // [1, 2, 3]

// NodeList to array (in browser)
// let elements = [...document.querySelectorAll('div')];

// === REMOVING DUPLICATES (One-liner!) ===
let withDupes = [1, 2, 2, 3, 1, 4, 3, 5];
let unique = [...new Set(withDupes)];
console.log(unique);  // [1, 2, 3, 4, 5]

// === PUSHING MULTIPLE ITEMS ===
let arr = [1, 2];

// Old way: multiple pushes or apply
arr.push(3);
arr.push(4);

// New way: spread in push
let newItems = [5, 6, 7];
arr.push(...newItems);
console.log(arr);  // [1, 2, 3, 4, 5, 6, 7]

// === CLONING AND MODIFYING ===
let todos = [
  { id: 1, text: 'Learn JS', done: false },
  { id: 2, text: 'Build app', done: false }
];

// Add new todo (immutably)
let newTodo = { id: 3, text: 'Deploy', done: false };
let updatedTodos = [...todos, newTodo];
console.log(todos.length);        // 2 (unchanged)
console.log(updatedTodos.length); // 3

// Remove by id (immutably)
let withoutFirst = todos.filter(t => t.id !== 1);
console.log(withoutFirst.length); // 1
```
