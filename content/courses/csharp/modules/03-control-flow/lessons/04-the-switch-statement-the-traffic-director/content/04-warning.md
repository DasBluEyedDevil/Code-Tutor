---
type: "WARNING"
title: "Watch Out!"
---

## Common Pitfalls with Switch

**Forgetting break (statements):** Without break, code 'falls through' to the next case! This is rarely intentional. Always end each case with `break`.

**Using variables in case:** `case myVariable:` is WRONG! Case values must be compile-time constants. Use `when` clauses for dynamic conditions.

**Switch expression exceptions:** If no pattern matches in a switch expression and there's no `_` default, you'll get a `SwitchExpressionException` at runtime!

**Pattern order matters:** In switch expressions, patterns are checked top to bottom. Put more specific patterns first! `>= 90` should come before `>= 80`.

**Confusing `or` with `||`:** In switch patterns, use the keyword `or` (not `||`). Example: `1 or 2 or 3 => "Low"`