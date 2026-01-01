---
type: "WARNING"
title: "Array Pitfalls"
---

### 1. Out of Bounds
If you try to access an index that doesn't exist (like `fruits[99]`), JavaScript doesn't crash. It simply returns `undefined`.
```javascript
const list = ['a', 'b'];
console.log(list[5]); // Result: undefined
```
Always check the `.length` of an array before trying to access deep indices.

### 2. The `length` vs. `index` Mistake
If an array has 3 items, its `length` is `3`. But the highest index is `2`. 
*   **Error:** Trying to access `list[list.length]` will return `undefined`.
*   **Fix:** The last item is always at `list.length - 1`.

### 3. Trailing Commas
```javascript
const colors = ['red', 'green', 'blue',];
```
In modern JavaScript, a comma after the last item is perfectly legal and actually encouraged in professional codebases because it makes "git diffs" cleaner when you add new items.

### 4. Hole-y Arrays (Avoid these!)
If you assign an index far beyond the current length:
```javascript
let nums = [1, 2];
nums[5] = 10;
console.log(nums); // [1, 2, <3 empty items>, 10]
```
This creates "holes" in your array (`undefined` values), which can cause loops and calculations to behave unpredictably. Always add items using specific methods (which we'll learn in the next lesson) instead of jumping indices.
