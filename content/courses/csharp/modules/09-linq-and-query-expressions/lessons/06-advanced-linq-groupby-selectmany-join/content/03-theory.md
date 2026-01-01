---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`collection.GroupBy(x => x.Key)`**: Groups items by a key. Returns IEnumerable<IGrouping<TKey, TElement>>. Each group has .Key (the grouping value) and acts as IEnumerable of items.

**`GroupBy + Select for aggregation`**: Common pattern: `.GroupBy(x => x.Category).Select(g => new { g.Key, Count = g.Count(), Sum = g.Sum(...) })`. Transforms groups into summary objects.

**`collection.SelectMany(x => x.Items)`**: Flattens nested collections. If each element contains a collection, SelectMany extracts and concatenates them all.

**`SelectMany with result selector`**: `.SelectMany(d => d.Items, (parent, item) => new { parent.Name, item })`. Second lambda combines parent object with each flattened item.

**`outer.Join(inner, outerKey, innerKey, result)`**: Matches items where keys are equal. Like SQL INNER JOIN. Only items with matching keys appear in result.

**`GroupJoin for LEFT JOIN behavior`**: Use `.GroupJoin()` when you need all outer items even without matches (like SQL LEFT JOIN).