---
type: "WARNING"
title: "Common Mistakes"
---

**Using double for money**: `double price = 19.99;` can lead to rounding errors! 0.1 + 0.2 might equal 0.30000000000000004. Always use `decimal` for financial calculations.

**Forgetting the 'm' suffix for decimals**: `decimal price = 19.99;` causes an error! You need `decimal price = 19.99m;` - the 'm' tells C# this is a decimal literal.

**Storing decimals in int**: `int price = 19.99;` causes an error! Use `double` or `decimal` for decimal values.

**Using commas instead of periods**: In C#, decimal points are periods. `19.99` is correct, `19,99` is a syntax error.