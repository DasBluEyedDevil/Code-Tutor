---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`.OrderBy(x => key)`**: Sorts ascending by key. For numbers: .OrderBy(n => n). For objects: .OrderBy(p => p.Price). Returns IEnumerable in sorted order.

**`.ThenBy(x => secondKey)`**: Secondary sort after OrderBy. Example: .OrderBy(p => p.Category).ThenBy(p => p.Name) sorts by category first, then name within each category.

**`.Count(), .Sum(), .Average()`**: Aggregation methods return single value. .Count() returns int. .Sum() adds values. .Average() calculates mean. These EXECUTE the query immediately!

**`.Min() / .Max()`**: Find smallest/largest. Simple: .Min() on numbers. With selector: .Max(p => p.Price) finds most expensive. Returns single value, not collection.