---
type: "WARNING"
title: "Watch Out!"
---

## Common Pitfalls with Operators

**Single & or | vs && or ||:** In C#, use DOUBLE symbols (`&&` and `||`) for logical operators. Single `&` and `|` are bitwise operators - they work but don't short-circuit, which can cause bugs!

**Short-circuit evaluation:** `&&` stops checking if the left side is false. `||` stops if the left is true. This matters with method calls: `obj != null && obj.Value > 0` is safe because if obj is null, the second part never runs.

**Operator precedence:** `age > 18 && hasTicket || isVIP` is confusing! Use parentheses: `(age > 18 && hasTicket) || isVIP` to make intent clear. `&&` has higher precedence than `||`.

**Pattern matching alternative:** Modern C# offers `is not null` instead of `!= null`, which reads more naturally: `if (obj is not null)`.