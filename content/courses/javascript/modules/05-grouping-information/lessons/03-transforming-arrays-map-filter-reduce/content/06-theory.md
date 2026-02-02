---
type: "THEORY"
title: "How Each Method Works"
---

**map(callback) - Transform every element**

Syntax:
```javascript
let newArray = array.map((element, index, array) => {
  return transformedElement;
});
```

- Calls callback for EVERY element
- Returns a NEW array with the SAME length
- Each element in new array = return value of callback
- Original array is unchanged
- If you forget to return, you get undefined for that element

**filter(callback) - Select matching elements**

Syntax:
```javascript
let newArray = array.filter((element, index, array) => {
  return true; // keep element
  return false; // discard element
});
```

- Calls callback for EVERY element
- Returns a NEW array (possibly shorter or empty)
- Keeps elements where callback returns truthy value
- Original array is unchanged
- Empty array [] if nothing matches

**reduce(callback, initialValue) - Accumulate into single value**

Syntax:
```javascript
let result = array.reduce((accumulator, element, index, array) => {
  return newAccumulator; // This becomes accumulator for next iteration
}, initialValue);
```

- accumulator: The running result (starts as initialValue)
- element: Current array element being processed
- Returns a SINGLE value (can be any type!)
- Without initialValue, first element is used as initial accumulator
- Always provide initialValue to avoid edge case bugs

**Callback parameters (all three methods):**

1. element - The current element being processed (always available)
2. index - The index of current element (optional, often not needed)
3. array - The original array (optional, rarely needed)

**Arrow function shortcuts:**

```javascript
// If body is single expression, return is implicit:
array.map(x => x * 2)           // Returns doubled values
array.filter(x => x > 10)       // Returns truthy condition
array.reduce((a, b) => a + b)   // Returns sum

// If body needs multiple statements, use braces and explicit return:
array.map(x => {
  let doubled = x * 2;
  let formatted = '$' + doubled;
  return formatted;
});
```

**Return value comparison:**

| Method | Returns | Length vs Original |
|--------|---------|--------------------|
| map | New array | Same length |
| filter | New array | Same or shorter |
| reduce | Single value | N/A (not an array) |