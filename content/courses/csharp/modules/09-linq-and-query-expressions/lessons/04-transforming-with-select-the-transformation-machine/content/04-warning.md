---
type: "WARNING"
title: "Common Pitfalls"
---

## Watch Out For These Mistakes!

**Confusing Where() and Select()**: Where() FILTERS (keeps some items, discards others). Select() TRANSFORMS (changes every item). If you want to remove items, use Where(). If you want to change items, use Select().

**Forgetting to capture result**: `.Select()` returns a NEW sequence - it doesn't modify the original! `list.Select(x => x * 2);` on its own does nothing. You must assign: `var doubled = list.Select(x => x * 2);`

**Anonymous objects can't leave the method**: Anonymous types (`new { Name = x.Name }`) are local only! You cannot return them from methods or store in class fields. For data that needs to leave the method, create a proper class or use ValueTuple.

**Decimal precision with money**: When calculating prices, use the 'm' suffix: `p.Price * 0.9m` not `p.Price * 0.9`. Without 'm', you get double which has floating-point precision issues with money!