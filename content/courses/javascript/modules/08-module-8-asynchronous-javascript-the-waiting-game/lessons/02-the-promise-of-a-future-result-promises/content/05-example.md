---
type: "EXAMPLE"
title: "Promise.try() - Safe Error Handling"
---

Promise.try() wraps any function and catches both sync AND async errors. This solves the problem where synchronous throws inside promise chains cause unhandled exceptions.

```javascript
// BEFORE: Manual wrapping for sync errors
function loadConfigOld(path) {
  return Promise.resolve().then(() => {
    if (!path) throw new Error('Path required');
    return Bun.file(path).json();
  });
}

// AFTER: Promise.try() handles sync errors elegantly
function loadConfig(path) {
  return Promise.try(() => {
    if (!path) throw new Error('Path required');
    return Bun.file(path).json();
  });
}

// Usage - both sync and async errors are caught
loadConfig(null)
  .catch(err => console.log('Caught:', err.message));
// Output: Caught: Path required
```
