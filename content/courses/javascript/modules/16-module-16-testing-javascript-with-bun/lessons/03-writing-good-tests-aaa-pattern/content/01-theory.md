---
type: "THEORY"
title: "Pure Functions & Testability"
---

Pure functions are easiest to test:
- Same input always gives same output
- No side effects (no API calls, no DOM, no randomness)

```javascript
// Pure - easy to test
function calculateTax(price, rate) {
  return price * rate;
}

// Impure - harder to test (depends on external state)
function calculateTaxWithDate(price) {
  const rate = new Date().getMonth() < 6 ? 0.08 : 0.1;
  return price * rate;
}
```

Refactor impure functions: inject dependencies instead of creating them inside.