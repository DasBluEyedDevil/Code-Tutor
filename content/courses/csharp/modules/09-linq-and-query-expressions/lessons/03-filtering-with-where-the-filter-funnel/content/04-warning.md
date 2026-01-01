---
type: "WARNING"
title: "Common Pitfalls"
---

## Watch Out For These Mistakes!

**Using = instead of ==**: Comparison requires double equals (==)! Single = is assignment and won't compile in a lambda condition: `x => x.Age = 30` is WRONG.

**Null reference in lambda**: If a property could be null, check it first! `x => x.Name.StartsWith("A")` will throw NullReferenceException if Name is null. Use: `x => x.Name != null && x.Name.StartsWith("A")` or null-conditional: `x => x.Name?.StartsWith("A") == true`.

**Case sensitivity**: String comparisons are case-sensitive by default! `p.Category == "electronics"` won't match "Electronics". Use `.Equals("electronics", StringComparison.OrdinalIgnoreCase)` for case-insensitive matching.

**Operator precedence**: `x => x > 5 && x < 10 || x == 100` may not work as expected! Use parentheses for clarity: `x => (x > 5 && x < 10) || x == 100`.