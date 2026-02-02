---
type: "EXAMPLE"
title: "Predicate Methods: some, every, find"
---

These methods consume the iterator and return a single value:

```javascript
function* numbers() {
  for (let i = 1; i <= 10; i++) yield i;
}

// .some() - Does any value match?
const hasEven = numbers().some(x => x % 2 === 0);
console.log(hasEven); // true (stops at first match!)

// .every() - Do all values match?
const allPositive = numbers().every(x => x > 0);
console.log(allPositive); // true (must check all)

// .find() - Get first matching value
const firstBigNumber = numbers().find(x => x > 7);
console.log(firstBigNumber); // 8 (stops at first match)

// .forEach() - Execute side effect for each
numbers().take(3).forEach(x => console.log(`Value: ${x}`));
// Value: 1
// Value: 2
// Value: 3

// .reduce() - Accumulate into single value
const sum = numbers().reduce((acc, x) => acc + x, 0);
console.log(sum); // 55
```
