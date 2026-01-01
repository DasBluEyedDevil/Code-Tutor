---
type: "EXAMPLE"
title: "The Core JSDoc Tags"
---

These three core JSDoc tags cover 90% of your typing needs. Use @param for function parameters, @returns for return types, and @type for variables.

```javascript
/**
 * Calculates the total price with tax
 * @param {number} price - The base price
 * @param {number} [taxRate=0.1] - Tax rate (default 10%)
 * @returns {number} The total with tax
 */
function calculateTotal(price, taxRate = 0.1) {
  return price * (1 + taxRate);
}

// @type for variables
/** @type {string[]} */
const names = ['Alice', 'Bob', 'Charlie'];

/** @type {{ id: number, name: string }} */
const user = { id: 1, name: 'Alice' };

// @type for destructuring
/** @type {{ port: number, host: string }} */
const { port, host } = config;
```
