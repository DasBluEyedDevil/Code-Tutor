---
type: "KEY_POINT"
title: "describe, it, expect"
---

Three core functions from 'bun:test':

**describe(name, fn)** - Groups related tests
```javascript
import { describe } from 'bun:test';
describe('Calculator', () => { ... });
```

**it(name, fn)** or **test(name, fn)** - Defines a single test
```javascript
import { it, test } from 'bun:test';
it('should add numbers', () => { ... });
```

**expect(value)** - Creates an assertion
```javascript
import { expect } from 'bun:test';
expect(result).toBe(expected);     // Strict equality
expect(arr).toEqual([1, 2, 3]);    // Deep equality
expect(fn).toThrow();              // Throws error
expect(value).toBeTruthy();        // Truthy check
```

Naming convention: Test names should read like sentences.
"it should calculate total with tax" tells you exactly what's being tested.