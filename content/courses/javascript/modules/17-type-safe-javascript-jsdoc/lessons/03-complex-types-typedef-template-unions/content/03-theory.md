---
type: "THEORY"
title: "@template - Generic Types"
---

@template lets you create flexible, reusable types:

```javascript
/**
 * Wraps any value in an object with metadata
 * @template T
 * @param {T} value - Any value
 * @returns {{ value: T, timestamp: number }}
 */
function wrap(value) {
  return { value, timestamp: Date.now() };
}

const wrapped = wrap('hello');  // { value: string, timestamp: number }
const wrappedNum = wrap(42);    // { value: number, timestamp: number }
```

**Multiple type parameters:**
```javascript
/**
 * @template K, V
 * @param {K} key
 * @param {V} value
 * @returns {Map<K, V>}
 */
function createMap(key, value) {
  return new Map([[key, value]]);
}
```