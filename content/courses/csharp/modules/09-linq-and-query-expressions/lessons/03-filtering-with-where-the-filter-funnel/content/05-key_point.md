---
type: "KEY_POINT"
title: "Filtering with Where"
---

## Key Takeaways

- **`.Where()` keeps items that match a condition** -- `products.Where(p => p.Price > 10)` returns only products over $10. The original collection is not modified.

- **Combine conditions with `&&` and `||`** -- `p => p.Price > 10 && p.InStock` filters on multiple criteria in a single lambda. Keep complex conditions readable.

- **Chain `.Where()` calls for readability** -- `products.Where(p => p.InStock).Where(p => p.Price < 100)` is equivalent to combining conditions but can be easier to read when filters are independent.
