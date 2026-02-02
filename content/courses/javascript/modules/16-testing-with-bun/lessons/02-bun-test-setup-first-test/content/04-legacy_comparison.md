---
type: "LEGACY_COMPARISON"
title: "Vitest Equivalent"
---

In Vitest, you would need to install and configure the test runner first. Bun's test runner works identically but with zero setup.

```javascript
// Vitest requires installation:
// npm install -D vitest

// And package.json scripts:
// "test": "vitest"

// Then import from 'vitest' instead of 'bun:test':
import { describe, it, expect } from 'vitest';

// The test code itself is identical!
describe('add', () => {
  it('adds numbers', () => {
    expect(add(2, 3)).toBe(5);
  });
});
```
