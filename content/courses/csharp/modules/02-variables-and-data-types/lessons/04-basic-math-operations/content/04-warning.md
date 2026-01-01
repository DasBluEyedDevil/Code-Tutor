---
type: "WARNING"
title: "Common Mistakes"
---

**Integer division surprise**: `int result = 7 / 2;` gives 3, not 3.5! The decimal is dropped (truncated, not rounded). Use `double` or `decimal` if you need decimals.

**Using x for multiplication**: `5 x 3` doesn't work! Use the asterisk: `5 * 3`. The letter x is not a valid operator.

**Division by zero**: `int result = 10 / 0;` crashes your program! Always check that your divisor isn't zero before dividing.

**Forgetting operator precedence**: `2 + 3 * 4` equals 14, not 20! Multiplication happens before addition. When in doubt, use parentheses: `(2 + 3) * 4` makes your intent crystal clear.