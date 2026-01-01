---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`int quotient = a / b;`**: WATCH OUT! When dividing two integers, C# gives you an INTEGER result. 10/3 = 3 (not 3.333...). The decimal part is thrown away - not rounded!

**`int remainder = a % b;`**: The % operator (modulus) gives you the REMAINDER after division. 10 % 3 = 1 because 10 / 3 = 3 with 1 left over. Super useful for checking even/odd (n % 2 == 0 means even)!

**`Parentheses`**: Use ( ) to control order! 5 + 3 * 2 = 11, but (5 + 3) * 2 = 16. Parentheses are always calculated first.

**`$"Sum: {sum}"`**: This is string interpolation - a cleaner way to include variables in strings. The $ before the quote enables it, and {} contains the expression.