---
type: "THEORY"
title: "Breaking Down the Syntax"
---

**at(index)**
- Access elements with positive or negative indices
- Negative indices count from the end (-1 = last, -2 = second to last)
- Returns undefined if index is out of bounds
```javascript
array.at(0)   // First element
array.at(-1)  // Last element
array.at(-2)  // Second to last
```

**toSorted(compareFn)**
- Returns a NEW sorted array
- Original array is unchanged
- Same comparison function as sort()
```javascript
const sorted = array.toSorted((a, b) => a - b);  // Ascending numbers
const sorted = array.toSorted((a, b) => b - a);  // Descending numbers
const sorted = array.toSorted();  // Default string sort
```

**toReversed()**
- Returns a NEW reversed array
- Original array is unchanged
- No arguments needed
```javascript
const reversed = array.toReversed();
```

**toSpliced(start, deleteCount, ...items)**
- Returns a NEW array with elements removed/added
- Original array is unchanged
- Same parameters as splice()
```javascript
array.toSpliced(1, 2)        // Remove 2 elements at index 1
array.toSpliced(1, 0, 'a')   // Insert 'a' at index 1
array.toSpliced(1, 1, 'a')   // Replace element at index 1 with 'a'
```

**with(index, value)**
- Returns a NEW array with element at index replaced
- Supports negative indices (like at())
- Original array is unchanged
```javascript
array.with(0, 'new')   // Replace first element
array.with(-1, 'new')  // Replace last element
array.with(2, 'new')   // Replace element at index 2
```

**Object.groupBy(array, keyFn)**
- Groups array items into an object
- keyFn returns the group key for each item
- Returns an object where keys are group names, values are arrays
```javascript
Object.groupBy(items, item => item.category);
Object.groupBy(numbers, n => n % 2 === 0 ? 'even' : 'odd');
```

**Map.groupBy(array, keyFn)**
- Like Object.groupBy but returns a Map
- Keys can be ANY type (not just strings)
- Better for non-string keys like booleans, objects, or numbers
```javascript
Map.groupBy(items, item => item.inStock);  // Boolean keys work!
Map.groupBy(items, item => item.category); // Object keys work!
```

**Why use non-mutating methods?**
1. Safer - original data is preserved
2. Predictable - easier to debug
3. Functional programming style
4. Required for React/Vue state management
5. Enables method chaining without side effects