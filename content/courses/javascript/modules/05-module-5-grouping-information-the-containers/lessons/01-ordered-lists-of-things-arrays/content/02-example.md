---
type: "EXAMPLE"
title: "Array Basics"
---

```javascript
// 1. Creating an Array
// We use square brackets [ ] and separate items with commas
const fruits = ['Apple', 'Banana', 'Cherry'];

// 2. Accessing items by Index
// REMEMBER: We start counting at 0!
console.log(fruits[0]); // Apple
console.log(fruits[1]); // Banana
console.log(fruits[2]); // Cherry

// 3. Changing an item (Updating)
fruits[1] = 'Blueberry';
console.log(fruits); // ['Apple', 'Blueberry', 'Cherry']

// 4. Getting the size of the array
console.log(`I have ${fruits.length} fruits.`);

// 5. Accessing the LAST item
// This is a common pattern: length minus one
const lastFruit = fruits[fruits.length - 1];
console.log(`The last fruit is: ${lastFruit}`);

// 6. Mixed-type arrays (Legal, but use carefully!)
const mixed = ['Hello', 42, true, null];
```