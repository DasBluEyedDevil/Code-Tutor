---
type: "WARNING"
title: "Watch Out!"
---

## Common Pitfalls with the Ternary Operator

**Forgetting the colon:** The `:` is REQUIRED! `condition ? trueValue` is incomplete and won't compile. You must have `: falseValue` at the end.

**Mixing types:** Both values must be compatible types! `(age >= 18) ? "Adult" : 0` won't work - you can't mix string and int. Both sides should return the same type.

**Overusing ternaries:** Nested ternaries get VERY hard to read. If you have 2+ levels deep, use if-else or a switch expression instead. Readability matters!

**Not a statement:** You can't use ternary for actions: `condition ? DoThis() : DoThat();` is bad practice. It's for VALUES, not side effects. Use if-else for actions.

**Prefer switch expressions for multiple cases:** If you're nesting ternaries like `a ? x : b ? y : c ? z : w`, a switch expression is cleaner and more readable.