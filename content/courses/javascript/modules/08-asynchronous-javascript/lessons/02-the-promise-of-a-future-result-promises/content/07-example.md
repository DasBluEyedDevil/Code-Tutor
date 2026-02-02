---
type: "EXAMPLE"
title: "Promise.withResolvers() - The Modern Way"
---

ES2024 introduces `Promise.withResolvers()` which gives you the promise, resolve, and reject functions separately. This is cleaner than the traditional executor pattern.

```javascript
// TRADITIONAL: Executor callback (still valid but verbose)
function rollDiceOld() {
  return new Promise((resolve, reject) => {
    setTimeout(() => {
      resolve(Math.floor(Math.random() * 6) + 1);
    }, 1000);
  });
}

// MODERN: Promise.withResolvers() (ES2024)
function rollDice() {
  const { promise, resolve } = Promise.withResolvers();
  setTimeout(() => {
    resolve(Math.floor(Math.random() * 6) + 1);
  }, 1000);
  return promise;
}

// Usage is identical
rollDice().then(result => console.log('You rolled:', result));
```
