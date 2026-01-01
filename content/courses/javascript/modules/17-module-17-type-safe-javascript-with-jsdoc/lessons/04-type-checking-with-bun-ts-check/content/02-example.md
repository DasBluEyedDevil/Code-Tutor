---
type: "EXAMPLE"
title: "Enabling Type Checking"
---

Add `// @ts-check` at the top of any JavaScript file to enable type checking:

```javascript
// @ts-check

/**
 * @param {string} name
 * @returns {string}
 */
function greet(name) {
  return `Hello, ${name}!`;
}

greet(42);  // ERROR: Argument of type 'number' is not assignable to parameter of type 'string'

/**
 * @type {number}
 */
const count = 'five';  // ERROR: Type 'string' is not assignable to type 'number'
```
