---
type: "WARNING"
title: "Data Type Dangers"
---

### 1. The Number vs. String Trap
JavaScript will sometimes try to be "helpful" and convert types for you. This can lead to unexpected results:
```javascript
console.log("5" + 5); // Result: "55" (It treats it as text!)
console.log("5" - 5); // Result: 0 (It treats it as a number!)
```
**Rule:** Always ensure you are working with the correct types before doing math.

### 2. Backticks vs. Quotes
Template literals ONLY work with backticks (`` ` ``). If you use single or double quotes, the `${variable}` part will just be printed as literal text.

### 3. Comparing Null and Undefined
While they both represent "nothing," they are not strictly the same:
```javascript
console.log(null == undefined); // true (they are similar)
console.log(null === undefined); // false (they are different types)
```
In modern JavaScript, it's best practice to use `null` for intentional emptiness.

### 4. Precision Problems
Computers sometimes struggle with very precise decimal math (binary floating point math):
```javascript
console.log(0.1 + 0.2); // Result: 0.30000000000000004
```
For most basic apps, this doesn't matter, but for financial apps, developers usually store money in cents (as whole numbers) to avoid this.
