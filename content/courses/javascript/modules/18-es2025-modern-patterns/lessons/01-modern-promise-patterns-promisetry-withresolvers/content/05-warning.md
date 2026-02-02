---
type: "WARNING"
title: "Don't Overuse"
---

1. **Don't wrap async functions in Promise.try()** - They already return promises!
```javascript
async function safe() { /* ... */ }

// WRONG: Unnecessary wrapping
Promise.try(() => safe());

// RIGHT: Just call it
safe();
```

2. **Don't use withResolvers() for simple cases** - The executor pattern is fine for simple promises.