---
type: "WARNING"
title: "Common Pitfalls"
---

## Watch Out For These Mistakes!

**Aggregate on empty collection**: `.Average()`, `.Min()`, `.Max()` throw exceptions on empty collections! Always check `.Any()` first: `list.Any() ? list.Average() : 0`. Or use `.DefaultIfEmpty()` before aggregating.

**Forgetting selector in aggregate**: `.Sum()` on a collection of objects won't work! You must specify what to sum: `.Sum(p => p.Price)`. The parameterless versions only work on numeric collections.

**OrderBy doesn't modify original**: Like all LINQ, `.OrderBy()` returns a NEW sequence! The original collection stays unsorted. You must use the result: `var sorted = list.OrderBy(...)`.

**.Count property vs .Count() method**: `List<T>` has a `.Count` PROPERTY (O(1), instant). `IEnumerable<T>` only has `.Count()` METHOD which must iterate the entire collection! For large lazy sequences, `.Count()` can be very slow. Use `.Any()` instead of `.Count() > 0`.