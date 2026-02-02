---
type: "EXAMPLE"
title: "The at() Method - Negative Indexing"
---

The at() method lets you access array elements using negative indices. Negative indices count backward from the end, so at(-1) returns the last element - no more awkward array[array.length - 1] syntax!

```javascript
// The old way to get the last element
let fruits = ['apple', 'banana', 'cherry', 'date'];
let last = fruits[fruits.length - 1];
console.log(last);  // 'date'

// With at() - so much cleaner!
let lastFruit = fruits.at(-1);
console.log(lastFruit);  // 'date'

// Negative indices count from the end
console.log(fruits.at(0));   // 'apple' (first)
console.log(fruits.at(1));   // 'banana' (second)
console.log(fruits.at(-1));  // 'date' (last)
console.log(fruits.at(-2));  // 'cherry' (second to last)
console.log(fruits.at(-3));  // 'banana' (third to last)

// Works with strings too!
let word = 'JavaScript';
console.log(word.at(0));   // 'J'
console.log(word.at(-1));  // 't'
console.log(word.at(-2));  // 'p'

// Practical: get the last item of a function result
function getScores() {
  return [85, 92, 78, 95, 88];
}
let topScore = getScores().at(-1);
console.log(topScore);  // 88

// Compare: old way required intermediate variable or ugly syntax
let scores = getScores();
let oldWay = scores[scores.length - 1];  // More verbose
```
