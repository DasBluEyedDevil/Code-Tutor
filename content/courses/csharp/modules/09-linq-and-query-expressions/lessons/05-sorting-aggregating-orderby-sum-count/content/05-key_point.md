---
type: "KEY_POINT"
title: "Sorting and Aggregation"
---

## Key Takeaways

- **`.OrderBy()` sorts ascending, `.OrderByDescending()` sorts descending** -- use `.ThenBy()` for secondary sort criteria. `.OrderBy(p => p.Category).ThenBy(p => p.Name)` sorts by category first, then name within each.

- **Aggregation methods execute immediately** -- `.Count()`, `.Sum()`, `.Average()`, `.Min()`, `.Max()` return a single value and trigger query execution. They do not return `IEnumerable<T>`.

- **Use overloads with selectors** -- `.Sum(p => p.Price)` and `.Max(p => p.Rating)` operate on a specific property without needing a separate `.Select()` call.
