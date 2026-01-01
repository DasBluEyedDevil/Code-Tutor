---
type: "LEGACY_COMPARISON"
title: "Vitest Equivalent"
---

The AAA pattern and test.each work identically in Vitest - just change the import.

```javascript
// Vitest version - only the import changes
import { describe, it, expect } from 'vitest';

// Everything else is the same!
describe('isEven', () => {
  it.each([[2, true], [3, false]])(
    'isEven(%i) returns %s',
    (input, expected) => {
      expect(isEven(input)).toBe(expected);
    }
  );
});
```
