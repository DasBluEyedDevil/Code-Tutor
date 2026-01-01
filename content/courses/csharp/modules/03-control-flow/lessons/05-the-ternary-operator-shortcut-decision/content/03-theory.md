---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`(condition) ? valueIfTrue : valueIfFalse`**: The three parts: condition to check, value if true (after ?), value if false (after :). The whole expression RETURNS a value!

**`Parentheses around condition`**: The parentheses around the condition are optional but make it clearer: (age >= 18) ? ... is more readable than age >= 18 ? ...

**`Returns a value`**: The ternary operator PRODUCES a value, so you can assign it: string x = condition ? "yes" : "no"; or use it inline: Console.WriteLine(x > 5 ? "big" : "small");

**`Nested ternaries`**: You CAN nest ternaries but it gets hard to read fast! grade >= 90 ? "A" : grade >= 80 ? "B" : "C". Use regular if-else when it's complex!