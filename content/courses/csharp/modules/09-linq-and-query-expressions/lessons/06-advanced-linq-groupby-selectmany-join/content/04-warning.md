---
type: "WARNING"
title: "Common Pitfalls"
---

## Watch Out For These Mistakes!

**Select vs SelectMany confusion**: `Select` returns nested collections (`IEnumerable<string[]>`), `SelectMany` flattens them (`IEnumerable<string>`). If you're getting a collection of collections when you want a flat list, switch to SelectMany!

**IGrouping is enumerable**: Each group from `GroupBy` IS an `IEnumerable`! To access items: `foreach (var item in group) { ... }`. To aggregate: `group.Sum(x => x.Value)`. Don't try to access items directly without iterating.

**Join key type mismatch**: Join keys must be the SAME type! Joining `order.ProductId` (int) with `product.Id` (string) returns ZERO results silently. Convert types if needed: `order => order.ProductId.ToString()`.

**Join is INNER JOIN**: LINQ `.Join()` only returns items with matching keys (like SQL INNER JOIN). Items without matches are silently excluded! For LEFT JOIN behavior (keep all left items), use `.GroupJoin()` with `.DefaultIfEmpty()`.