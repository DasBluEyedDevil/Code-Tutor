---
type: "KEY_POINT"
title: "Transforming with Select"
---

## Key Takeaways

- **`.Select()` transforms every item** -- unlike `.Where()` which filters, `.Select()` changes each element. `numbers.Select(n => n * 2)` doubles every number.

- **Select can change the output type** -- `people.Select(p => p.Name)` transforms `IEnumerable<Person>` into `IEnumerable<string>`. Use anonymous objects `new { p.Name, p.Age }` to reshape data.

- **Combine Where and Select for filter-then-transform** -- `products.Where(p => p.InStock).Select(p => p.Name)` first filters, then extracts names. Order matters: filter first to reduce the set before transforming.
