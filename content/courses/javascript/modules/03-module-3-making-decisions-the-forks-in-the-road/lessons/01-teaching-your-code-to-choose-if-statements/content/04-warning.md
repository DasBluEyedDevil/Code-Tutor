---
type: "WARNING"
title: "Common Pitfalls"
---

Common mistakes:

1. Forgetting parentheses around the condition: if age >= 18 won't work. Must be if (age >= 18)

2. Using = instead of ==  or ===:
   - if (age = 18) is WRONG - this assigns 18 to age!
   - if (age === 18) is CORRECT - this checks if age equals 18

3. Forgetting curly braces: While technically optional for single-line if statements, always use { } to avoid bugs later.

4. Putting a semicolon after the condition: if (age >= 18); is wrong. The semicolon ends the if statement before it does anything!

5. Trying to use 'AND' or 'OR' in English: if (age > 17 and age < 65) won't work. JavaScript uses && for 'and' and || for 'or' (we'll learn these soon).