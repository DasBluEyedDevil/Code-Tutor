---
type: "WARNING"
title: "Common Mistakes"
---

**1. Forgetting to return inside arrow function with braces**

```javascript
// WRONG - no return, results in [undefined, undefined, ...]
let doubled = numbers.map(x => { x * 2 });

// CORRECT - explicit return with braces
let doubled = numbers.map(x => { return x * 2; });

// CORRECT - implicit return without braces
let doubled = numbers.map(x => x * 2);
```

**2. Mutating the accumulator object in reduce**

```javascript
// RISKY - mutating the same object
let counts = items.reduce((acc, item) => {
  acc[item] = (acc[item] || 0) + 1;
  return acc;  // Returning mutated object works but can cause bugs
}, {});

// SAFER - create new object each time (for complex cases)
let counts = items.reduce((acc, item) => {
  return {
    ...acc,
    [item]: (acc[item] || 0) + 1
  };
}, {});
```

**3. Forgetting reduce's initial value**

```javascript
// RISKY - no initial value
let sum = [].reduce((a, b) => a + b);  // TypeError! Empty array!

// SAFE - always provide initial value
let sum = [].reduce((a, b) => a + b, 0);  // Returns 0
```

**4. Thinking map can filter (it cannot)**

```javascript
// WRONG - map always returns same length array
let adults = users.map(user => {
  if (user.age >= 18) return user;
  // Returns undefined for non-adults!
});
// Result: [Alice, undefined, Charlie, undefined, ...]

// CORRECT - use filter for selection, map for transformation
let adults = users.filter(user => user.age >= 18);
let adultNames = users.filter(u => u.age >= 18).map(u => u.name);
```

**5. Not saving the result (these methods don't mutate)**

```javascript
let numbers = [3, 1, 2];
numbers.map(x => x * 2);     // Creates new array but throws it away!
console.log(numbers);        // Still [3, 1, 2]

// CORRECT - save the result
let doubled = numbers.map(x => x * 2);
console.log(doubled);        // [6, 2, 4]
```

**6. Performance: Excessive chaining for large arrays**

```javascript
// Creates 3 intermediate arrays - fine for small arrays
let result = hugeArray
  .filter(x => x > 0)
  .map(x => x * 2)
  .filter(x => x < 100);

// For very large arrays, single reduce might be faster:
let result = hugeArray.reduce((acc, x) => {
  if (x > 0) {
    let doubled = x * 2;
    if (doubled < 100) {
      acc.push(doubled);
    }
  }
  return acc;
}, []);
```

**7. Confusing return types**

```javascript
// filter returns ARRAY of matching items
let adults = users.filter(u => u.age >= 18);  // Array!

// find returns SINGLE item (or undefined) - different method!
let firstAdult = users.find(u => u.age >= 18);  // Single object!

// reduce returns WHATEVER you return from callback
let sum = numbers.reduce((a, b) => a + b, 0);      // Number
let joined = words.reduce((a, b) => a + ' ' + b);  // String
let grouped = items.reduce(..., {});               // Object
```