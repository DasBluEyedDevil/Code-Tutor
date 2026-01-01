---
type: "WARNING"
title: "Common Pitfalls"
---

## Watch Out For These!

**Cannot modify collection during foreach**: Adding or removing items while iterating with foreach throws InvalidOperationException! If you need to modify, use a regular for loop or create a copy of the collection first.

**foreach is read-only**: You cannot change the loop variable inside foreach. `foreach (var x in list) { x = 5; }` won't actually modify the list - the variable is a copy!

**No index access in foreach**: If you need the position/index of each item, use a regular for loop or use LINQ's Select with index. foreach only gives you values, not positions.

**break and continue still work**: Use `break` to exit the loop early, `continue` to skip to the next iteration. These work the same as in for loops.

**Empty collections are safe**: Iterating over an empty collection with foreach simply does nothing - no crash, no error. It's safe to use!