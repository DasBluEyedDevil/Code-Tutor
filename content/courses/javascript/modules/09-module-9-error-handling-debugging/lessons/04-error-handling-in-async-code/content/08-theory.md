---
type: "THEORY"
title: "Unhandled Promise Rejection Behavior"
---

Understanding what happens when promise rejections aren't handled:

**In Node.js (v15+):**
- Unhandled rejections crash the process by default
- This is intentional - unhandled errors are bugs!
- Can be configured with --unhandled-rejections flag

**In Browsers:**
- Logs a warning to console
- Does NOT crash the page
- Still a bug - should always be handled

**Detecting Unhandled Rejections:**

```javascript
// Browser
window.addEventListener('unhandledrejection', event => {
  console.error('Unhandled rejection:', event.reason);
  event.preventDefault(); // Prevent default logging
});

// Node.js
process.on('unhandledRejection', (reason, promise) => {
  console.error('Unhandled rejection at:', promise);
  console.error('Reason:', reason);
});
```

**Common Causes:**
1. Forgetting to await: `asyncFunction();` instead of `await asyncFunction();`
2. Missing .catch(): `promise.then(...)` without `.catch(...)`
3. Errors in .then() without a .catch()
4. Throwing in async function called without await

**Best Practices:**
1. Always await async functions in try-catch blocks
2. Always add .catch() to promise chains
3. Use linting rules to catch missing error handling
4. Set up global rejection handlers as a safety net
5. In production, log unhandled rejections to monitoring