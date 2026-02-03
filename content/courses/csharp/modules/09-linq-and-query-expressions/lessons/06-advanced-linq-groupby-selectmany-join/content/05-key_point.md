---
type: "KEY_POINT"
title: "GroupBy, SelectMany, and Join"
---

## Key Takeaways

- **`GroupBy` creates groups with a `.Key` property** -- `products.GroupBy(p => p.Category)` returns groups where each group is an `IGrouping<string, Product>`. Chain `.Select()` to aggregate each group.

- **`SelectMany` flattens nested collections** -- if each order has a list of items, `orders.SelectMany(o => o.Items)` extracts all items into one flat sequence. Essential for one-to-many relationships.

- **`Join` connects two collections by matching keys** -- similar to SQL INNER JOIN. Use `GroupJoin` for LEFT JOIN behavior where you need all items from the first collection even without matches.
