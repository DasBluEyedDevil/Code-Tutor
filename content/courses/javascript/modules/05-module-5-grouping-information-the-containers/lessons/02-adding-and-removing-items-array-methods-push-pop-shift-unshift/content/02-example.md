---
type: "EXAMPLE"
title: "Code Example"
---

See the code example above demonstrating Code Example.

```javascript
let fruits = ['apple', 'banana'];
console.log(fruits);  // ['apple', 'banana']

// push() - add to the END
fruits.push('cherry');
console.log(fruits);  // ['apple', 'banana', 'cherry']

// Can push multiple at once
fruits.push('date', 'elderberry');
console.log(fruits);  // ['apple', 'banana', 'cherry', 'date', 'elderberry']

// pop() - remove from the END, returns the removed item
let removed = fruits.pop();
console.log(removed);  // 'elderberry'
console.log(fruits);  // ['apple', 'banana', 'cherry', 'date']

// unshift() - add to the FRONT
fruits.unshift('apricot');
console.log(fruits);  // ['apricot', 'apple', 'banana', 'cherry', 'date']

// shift() - remove from the FRONT, returns the removed item
let firstItem = fruits.shift();
console.log(firstItem);  // 'apricot'
console.log(fruits);  // ['apple', 'banana', 'cherry', 'date']

// Practical: stack (Last In, First Out)
let stack = [];
stack.push('task 1');
stack.push('task 2');
stack.push('task 3');
let current = stack.pop();  // 'task 3' - most recent

// Practical: queue (First In, First Out)
let queue = [];
queue.push('person 1');
queue.push('person 2');
queue.push('person 3');
let next = queue.shift();  // 'person 1' - first in line
```
