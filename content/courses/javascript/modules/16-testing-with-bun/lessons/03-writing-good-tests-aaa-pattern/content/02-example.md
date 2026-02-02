---
type: "EXAMPLE"
title: "Arrange-Act-Assert Pattern"
---

See the code example above demonstrating Arrange-Act-Assert Pattern.

```javascript
// AAA makes tests readable and consistent

import { describe, it, expect } from 'bun:test';
import { applyDiscount } from './pricing';

describe('applyDiscount', () => {
  it('applies percentage discount correctly', () => {
    // Arrange - set up test data
    const price = 100;
    const discountPercent = 20;

    // Act - call the function
    const result = applyDiscount(price, discountPercent);

    // Assert - verify the result
    expect(result).toBe(80);
  });
});
```
