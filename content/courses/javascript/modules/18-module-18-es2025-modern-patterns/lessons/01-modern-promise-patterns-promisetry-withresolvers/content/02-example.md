---
type: "EXAMPLE"
title: "Promise.try() - The Universal Error Catcher"
---

Promise.try() wraps any function and catches ALL errors - sync or async:

```javascript
// The Problem: Sync errors bypass .catch()
function riskySync(input) {
  if (!input) throw new Error('Input required!');  // This is sync!
  return fetch(`/api/${input}`);
}

// OLD WAY: Errors thrown before the promise is created aren't caught
riskySync(null).catch(console.error);  // UNCAUGHT! throws before .catch() is set up

// OLD WORKAROUND: Wrap in Promise.resolve().then()
Promise.resolve().then(() => riskySync(null)).catch(console.error);  // Works but ugly

// NEW WAY: Promise.try() handles everything
Promise.try(() => riskySync(null)).catch(console.error);  // 'Input required!' - caught!

// Works with any return value:
Promise.try(() => 42).then(console.log);                    // 42
Promise.try(() => fetch('/api')).then(r => console.log(r)); // Response
Promise.try(() => { throw 'oops'; }).catch(console.log);    // 'oops'
```
