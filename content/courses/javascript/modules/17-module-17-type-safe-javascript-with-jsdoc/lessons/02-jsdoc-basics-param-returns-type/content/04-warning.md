---
type: "WARNING"
title: "JSDoc Gotchas"
---

1. **Put JSDoc DIRECTLY above the function** - No blank lines between!
```javascript
// WRONG - blank line breaks the connection
/**
 * My function
 */

function myFunc() {}

// RIGHT - no blank line
/**
 * My function
 */
function myFunc() {}
```

2. **Use curly braces around types**: It's `@param {string}` not `@param string`

3. **Arrays need element type**: `{string[]}` not just `{array}`