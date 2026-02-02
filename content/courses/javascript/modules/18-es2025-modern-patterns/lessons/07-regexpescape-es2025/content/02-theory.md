---
type: "THEORY"
title: "The Problem RegExp.escape Solves"
---

When building regex patterns dynamically from user input, special characters cause bugs or security vulnerabilities:

```javascript
// User wants to search for 'price: $100'
const userInput = 'price: $100';

// WRONG: $ has special meaning in regex (end of string)
const badRegex = new RegExp(userInput);
badRegex.test('price: $100');     // Unpredictable!
badRegex.test('price: 100');       // Might match unexpectedly

// Before ES2025, you needed a helper function:
function escapeRegExp(str) {
  return str.replace(/[.*+?^${}()|[\]\\]/g, '\\$&');
}
const goodRegex = new RegExp(escapeRegExp(userInput));
goodRegex.test('price: $100');     // true - correct!
```

The old approach had problems:
- Easy to forget or miss characters
- Different implementations disagreed on which characters to escape
- Security risk if any character was missed

`RegExp.escape()` standardizes this once and for all.