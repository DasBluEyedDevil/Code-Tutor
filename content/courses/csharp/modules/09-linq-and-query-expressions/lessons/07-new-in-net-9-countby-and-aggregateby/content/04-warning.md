---
type: "WARNING"
title: "Common Pitfalls"
---

## Watch Out For These Mistakes!

**.NET 9 required!**: CountBy and AggregateBy are NEW in .NET 9! They won't compile on .NET 8 or earlier. Ensure your project targets `net9.0` in your .csproj file: `<TargetFramework>net9.0</TargetFramework>`.

**Wrong seed type in AggregateBy**: The seed value's type determines the accumulator type! Using `0` (int) when summing decimals causes type mismatch. Match your seed to your values: `0m` for decimal, `0.0` for double, `""` for string concatenation.

**Forgetting the seed in AggregateBy**: AggregateBy requires THREE arguments: key selector, seed, and accumulator function. Missing the seed causes a compiler error.

**Still using GroupBy+Select for simple counts**: If you're writing `.GroupBy(x => x.Key).Select(g => new { g.Key, Count = g.Count() })`, switch to `.CountBy(x => x.Key)` for cleaner, more efficient code. Same applies to simple aggregations with AggregateBy.