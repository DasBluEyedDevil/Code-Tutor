---
type: "EXAMPLE"
title: "Tests as Documentation"
---

See the code example above demonstrating Tests as Documentation.

```javascript
// Tests document expected behavior better than comments

// What does this function do? The test tells us:
test('calculateDiscount applies 10% off for orders over $100', () => {
  expect(calculateDiscount(150)).toBe(15);
  expect(calculateDiscount(50)).toBe(0);
});

// Now we know:
// - Orders over $100 get 10% discount
// - Orders $100 or less get no discount
// This documentation never goes stale!
```
