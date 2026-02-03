---
type: "KEY_POINT"
title: "Foreach and Collection Selection"
---

## Key Takeaways

- **`foreach` is the cleanest way to iterate** -- `foreach (var item in collection)` works with arrays, lists, dictionaries, and any `IEnumerable<T>`. No index management needed.

- **Do not modify a collection while iterating it** -- adding or removing items during `foreach` throws `InvalidOperationException`. Collect changes in a separate list, then apply them after the loop.

- **Choose the right collection for the job** -- `List<T>` for ordered items, `Dictionary<TKey, TValue>` for key lookups, `HashSet<T>` for unique values, arrays for fixed-size performance-critical data.
