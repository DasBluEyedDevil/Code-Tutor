---
type: "KEY_POINT"
title: "CountBy and AggregateBy in .NET 9"
---

## Key Takeaways

- **`CountBy()` replaces `GroupBy().Select(g => g.Count())`** -- `words.CountBy(w => w)` counts occurrences of each word in one call. Returns `KeyValuePair<TKey, int>` pairs.

- **`AggregateBy()` groups and accumulates in one pass** -- `sales.AggregateBy(s => s.Region, 0m, (total, s) => total + s.Amount)` sums sales per region without materializing intermediate groups.

- **Both are more efficient than GroupBy for aggregation** -- they avoid creating intermediate group objects, reducing memory allocations. Use them when you only need counts or accumulated values, not the grouped items themselves.
