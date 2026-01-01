---
type: "WARNING"
title: "Common Pitfalls"
---

## Watch Out For These!

**var doesn't work directly**: Collection expressions require an explicit target type. `var x = [1, 2, 3];` won't compile! Always specify the type: `int[] x = [1, 2, 3];` or `List<int> x = [1, 2, 3];`

**Don't confuse [ ] with { }**: Collection expressions use SQUARE brackets [ ]. The old initializer syntax uses curly braces { }. They're different! [1, 2, 3] is C# 12+, {1, 2, 3} is older syntax.

**Spread element requires two dots**: To spread a collection, use two dots: [..array]. A single dot or no dots treats it as a single element, not a spread.

**Performance consideration**: Collection expressions are highly optimized, especially for immutable collections like ImmutableArray<T>. They often generate better code than traditional initializers!

**Requires .NET 8+**: Collection expressions are a C# 12 feature, which requires .NET 8 or later. Older projects need to upgrade to use this syntax.