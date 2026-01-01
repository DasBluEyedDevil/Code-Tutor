---
type: "LEGACY_COMPARISON"
title: "Traditional Promise Constructor"
---

Before ES2024, you had to use the executor callback pattern. This is still valid and works everywhere, but Promise.withResolvers() is cleaner for complex cases where you need to pass resolve/reject to other functions.

```javascript
// The executor pattern (pre-ES2024)
const promise = new Promise((resolve, reject) => {
  // resolve and reject are only available here
  doAsyncWork((err, result) => {
    if (err) reject(err);
    else resolve(result);
  });
});
```
