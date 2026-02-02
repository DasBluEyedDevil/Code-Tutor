---
type: "EXAMPLE"
title: "Core Iterator Helper Methods"
---

ES2025 adds these methods to all iterators (generators, Map.keys(), etc.):

```javascript
// Create a generator for demonstration
function* numbers() {
  for (let i = 1; i <= 100; i++) yield i;
}

// .map() - Transform each value
const doubled = numbers().map(x => x * 2);
console.log([...doubled.take(5)]); // [2, 4, 6, 8, 10]

// .filter() - Keep values matching condition
const evens = numbers().filter(x => x % 2 === 0);
console.log([...evens.take(5)]); // [2, 4, 6, 8, 10]

// .take(n) - Get first n values
const firstThree = numbers().take(3);
console.log([...firstThree]); // [1, 2, 3]

// .drop(n) - Skip first n values
const skipFive = numbers().drop(5).take(3);
console.log([...skipFive]); // [6, 7, 8]

// .flatMap() - Map then flatten one level
function* pairs() {
  yield [1, 2];
  yield [3, 4];
}
const flat = pairs().flatMap(pair => pair);
console.log([...flat]); // [1, 2, 3, 4]

// .toArray() - Collect into array
const arr = numbers().take(5).toArray();
console.log(arr); // [1, 2, 3, 4, 5]
```
