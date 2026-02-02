---
type: "KEY_POINT"
title: "test.each for Multiple Cases"
---

Avoid repetitive tests with test.each:

```javascript
import { describe, it, expect } from 'bun:test';

describe('isEven', () => {
  it.each([
    [2, true],
    [3, false],
    [0, true],
    [-4, true],
  ])('isEven(%i) returns %s', (input, expected) => {
    expect(isEven(input)).toBe(expected);
  });
});
```

Test edge cases:
- Empty inputs (null, undefined, '', [])
- Boundary values (0, -1, MAX_INT)
- Invalid inputs (wrong types)
- Large inputs (performance)