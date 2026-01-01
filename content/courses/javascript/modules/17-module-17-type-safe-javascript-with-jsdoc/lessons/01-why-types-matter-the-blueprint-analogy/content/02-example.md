---
type: "EXAMPLE"
title: "A Real Production Bug"
---

This code shipped to production. Can you spot the bug?

```javascript
function calculateDiscount(price, discountPercent) {
  return price - (price * discountPercent);
}

// Developer A uses it correctly
const salePrice = calculateDiscount(100, 0.2);  // 80

// Developer B reads "discountPercent" and does this
const wrongPrice = calculateDiscount(100, 20);  // -1900 (oops!)

// With JSDoc types, the IDE would warn Developer B immediately:
/**
 * @param {number} price - Original price
 * @param {number} discountPercent - Discount as decimal (0.2 = 20%)
 * @returns {number}
 */
function calculateDiscountTyped(price, discountPercent) {
  return price - (price * discountPercent);
}
```
