---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**CountBy<TSource, TKey>**
```csharp
source.CountBy(keySelector)
```
- Returns `IEnumerable<KeyValuePair<TKey, int>>`
- Counts occurrences of each key
- Equivalent to: `.GroupBy(key).Select(g => new { g.Key, Count = g.Count() })`

**AggregateBy<TSource, TKey, TAccumulate>**
```csharp
source.AggregateBy(keySelector, seed, func)
```
- `keySelector`: Groups items by this key
- `seed`: Starting value for aggregation (e.g., 0 for sum, "" for strings)
- `func`: Combines accumulator with each item `(acc, item) => newAcc`
- Returns `IEnumerable<KeyValuePair<TKey, TAccumulate>>`

**Why Use These?**
1. **Cleaner code**: One method vs GroupBy+Select+Aggregate chain
2. **More efficient**: Single pass through data
3. **Type-safe**: Strong typing on key and accumulator
4. **Intent clarity**: Method name describes exactly what you're doing

**When to Use Which?**
- Use `CountBy` when you just need counts per group
- Use `AggregateBy` when you need to combine/accumulate values
- Use `GroupBy` when you need the actual grouped items (not just aggregations)