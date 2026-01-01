---
type: "ANALOGY"
title: "Understanding the Concept"
---

Imagine sorting your groceries: 'How many items per category?' (CountBy) or 'What's the total cost per category?' (AggregateBy). Before .NET 9, you'd need GroupBy + Select + Count/Sum. Now it's one simple method call!

CountBy = Quick counting by key:
- 'How many products in each category?'
- Returns KeyValuePair<TKey, int> for each group
- Single method instead of GroupBy().Select(g => new { g.Key, g.Count() })

AggregateBy = Accumulate values by key:
- 'What's the total price per category?'
- You provide: key selector, seed value, accumulator function
- Returns KeyValuePair<TKey, TAccumulate> for each group

Both methods:
- More efficient (single pass through data)
- Cleaner code (one method vs. chain)
- Type-safe (strong typing on key and result)

Think: 'GroupBy patterns simplified into direct, purpose-built methods!'