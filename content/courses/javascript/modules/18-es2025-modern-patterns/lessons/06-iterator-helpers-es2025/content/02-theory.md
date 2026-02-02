---
type: "THEORY"
title: "Why Iterator Helpers Matter"
---

Before ES2025, if you wanted to transform data from an iterator, you had two choices:

1. **Convert to array first** - Wasteful for large datasets
```javascript
// Old way: Loads everything into memory
const arr = [...hugeIterator];
const result = arr.filter(x => x > 10).map(x => x * 2);
```

2. **Write manual loops** - Verbose and error-prone
```javascript
// Old way: Lots of boilerplate
const results = [];
for (const item of hugeIterator) {
  if (item > 10) results.push(item * 2);
}
```

**Now with Iterator Helpers:**
```javascript
// New way: Lazy, memory-efficient, readable
const result = hugeIterator
  .filter(x => x > 10)
  .map(x => x * 2)
  .toArray();
```

The key insight: Iterator helpers return new iterators, not arrays. They don't execute until you consume the results. This makes them memory-efficient for processing millions of items.