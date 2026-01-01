---
type: "KEY_POINT"
title: "When to Use Promise.try()"
---

Use `Promise.try()` when:

1. **Your callback might throw synchronously** - Validation, parsing, etc.
2. **You want consistent error handling** - All errors go to `.catch()`
3. **Mixing sync and async code** - The callback can return a value OR a Promise

```javascript
// Works with sync returns
Promise.try(() => 42).then(console.log); // 42

// Works with async returns
Promise.try(() => fetch('/api')).then(console.log); // Response

// Works with sync throws
Promise.try(() => { throw new Error('Oops'); }).catch(console.log); // Error: Oops
```