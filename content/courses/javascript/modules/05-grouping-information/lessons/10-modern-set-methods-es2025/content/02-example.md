---
type: "EXAMPLE"
title: "Creating and Using Sets"
---

A Set is a collection of unique values - duplicates are automatically removed. Create Sets from arrays, add/remove items, and convert back to arrays when needed.

```javascript
// Creating Sets
let fruits = new Set(['apple', 'banana', 'cherry']);
let moreFruits = new Set(['cherry', 'date', 'elderberry']);

// Sets automatically remove duplicates
let numbers = new Set([1, 2, 2, 3, 3, 3]);
console.log(numbers);  // Set { 1, 2, 3 } - duplicates removed!

// Basic Set operations
fruits.add('date');        // Add item
fruits.delete('banana');   // Remove item
fruits.has('apple');       // true - check if exists
fruits.size;               // 3 - number of items

// Convert to array
let fruitArray = [...fruits];  // ['apple', 'cherry', 'date']

// Loop through a Set
for (let fruit of fruits) {
  console.log(fruit);
}

// Remove duplicates from array (common pattern)
let dupes = [1, 2, 2, 3, 3, 3, 4];
let unique = [...new Set(dupes)];
console.log(unique);  // [1, 2, 3, 4]
```
