---
type: "EXAMPLE"
title: "Promise.withResolvers() - External Control"
---

Promise.withResolvers() lets you get resolve and reject functions outside the executor callback, enabling cleaner deferred patterns.

```javascript
// OLD WAY: Hoisting variables out of executor
let resolveFromOutside, rejectFromOutside;
const promise = new Promise((resolve, reject) => {
  resolveFromOutside = resolve;
  rejectFromOutside = reject;
});
resolveFromOutside('done');  // Awkward!

// NEW WAY: Promise.withResolvers()
const { promise: p, resolve, reject } = Promise.withResolvers();

// Now you can pass resolve/reject anywhere
setTimeout(() => resolve('done'), 1000);

// Real-world example: Deferred pattern
function createDeferred() {
  return Promise.withResolvers();
}

const deferred = createDeferred();
eventEmitter.once('complete', deferred.resolve);
eventEmitter.once('error', deferred.reject);
await deferred.promise;
```
