---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`score += 50;`**: This is shorthand for `score = score + 50`. It takes the current value, adds 50, and stores the result back. Fewer chances to typo the variable name!

**`score *= 2;`**: This DOUBLES the score! Same as `score = score * 2`. Great for bonus multipliers in games!

**`lives--;`**: The -- operator subtracts 1. `lives--` is the same as `lives = lives - 1` or `lives -= 1`. Super common in loops and countdowns!

**`++lives vs lives++`**: Both add 1, but `++lives` (prefix) increments BEFORE using the value, while `lives++` (postfix) uses the value THEN increments. For simple statements, they're identical - the difference matters in expressions like `Console.WriteLine(++x);`