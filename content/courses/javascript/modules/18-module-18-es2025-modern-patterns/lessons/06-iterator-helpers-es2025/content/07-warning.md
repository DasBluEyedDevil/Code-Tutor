---
type: "WARNING"
title: "Common Pitfalls"
---

1. **Iterators are single-use**
```javascript
const iter = [1, 2, 3].values().map(x => x * 2);
console.log([...iter]); // [2, 4, 6]
console.log([...iter]); // [] - Already exhausted!

// Fix: Create a new iterator each time
const getData = () => [1, 2, 3].values().map(x => x * 2);
console.log([...getData()]); // [2, 4, 6]
console.log([...getData()]); // [2, 4, 6]
```

2. **Consumption methods exhaust the iterator**
```javascript
const iter = [1, 2, 3].values();
const found = iter.find(x => x > 1);  // 2
const arr = iter.toArray();           // [3] - Only remaining values!
```

3. **Arrays don't have these methods directly**
```javascript
// WRONG: Arrays are not iterators
[1, 2, 3].take(2);  // Error: take is not a function

// RIGHT: Get the iterator first
[1, 2, 3].values().take(2);  // Works!
```