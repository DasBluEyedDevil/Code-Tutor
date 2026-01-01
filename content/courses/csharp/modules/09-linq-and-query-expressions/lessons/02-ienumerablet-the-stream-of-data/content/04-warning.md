---
type: "WARNING"
title: "Common Pitfalls"
---

## Watch Out For These Mistakes!

**Multiple enumeration trap**: Each time you iterate over IEnumerable<T>, the query re-executes from scratch! If you call .Count() then foreach, the query runs TWICE. Use .ToList() first if you need multiple passes.

**Collection modified during iteration**: IEnumerable is LIVE - it reflects the current state of the source. Adding items to the source list after creating the query will include them when you finally iterate! Use .ToList() immediately for a snapshot.

**Disposed context**: When using IEnumerable with databases (EF Core), the query executes when you iterate. If the database context is disposed before iteration, you'll get an exception! Materialize before exiting the using block.

**.Count vs .Count()**: List<T> has a .Count PROPERTY (instant, O(1)). IEnumerable<T> has .Count() METHOD that iterates the entire collection! On large lazy sequences, .Count() can be very slow.