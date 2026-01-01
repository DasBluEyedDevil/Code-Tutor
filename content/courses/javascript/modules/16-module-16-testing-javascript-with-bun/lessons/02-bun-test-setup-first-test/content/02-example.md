---
type: "EXAMPLE"
title: "Your First Test"
---

See the code example above demonstrating Your First Test.

```javascript
// math.ts
export function add(a: number, b: number): number {
  return a + b;
}

// math.test.ts
import { describe, it, expect } from 'bun:test';
import { add } from './math';

describe('add', () => {
  it('adds two positive numbers', () => {
    expect(add(2, 3)).toBe(5);
  });

  it('handles negative numbers', () => {
    expect(add(-1, 1)).toBe(0);
  });
});

// Run: bun test
// Output: bun test v1.x
//         math.test.ts:
//         ✓ add > adds two positive numbers
//         ✓ add > handles negative numbers
```
