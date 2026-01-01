---
type: "THEORY"
title: "Complete Syntax Reference"
---

**Search Methods Quick Reference:**

| Method | Returns | When Not Found | Use Case |
|--------|---------|----------------|----------|
| find(fn) | Element | undefined | First match |
| findIndex(fn) | Number | -1 | Position of first match |
| findLast(fn) | Element | undefined | Last match |
| findLastIndex(fn) | Number | -1 | Position of last match |
| some(fn) | Boolean | false | Any match exists? |
| every(fn) | Boolean | true* | All match? |
| includes(val) | Boolean | false | Value exists? |
| indexOf(val) | Number | -1 | Position of value |
| at(index) | Element | undefined | Access by index |

*every() returns true for empty arrays (vacuously true)

**Callback Function Signature:**
```javascript
array.method((element, index, array) => {
  // element: current item being processed
  // index: position in array (optional)
  // array: the array being searched (optional)
  return condition;  // true/false for search methods
});
```

**Method Categories:**

1. **Element Finders** (return the element):
   - find(callback) - first match
   - findLast(callback) - last match
   - at(index) - by position

2. **Position Finders** (return index number):
   - findIndex(callback) - first match position
   - findLastIndex(callback) - last match position
   - indexOf(value) - simple value position

3. **Boolean Testers** (return true/false):
   - some(callback) - at least one matches
   - every(callback) - all match
   - includes(value) - value exists

**Short-Circuit Behavior:**
- find/findIndex: Stop at first match
- findLast/findLastIndex: Stop at first match (from end)
- some: Stops when true is found
- every: Stops when false is found
- includes/indexOf: Stop when found

**Equality Comparison:**
- includes() uses SameValueZero (finds NaN)
- indexOf() uses strict equality === (cannot find NaN)
- find/some/every use your callback logic