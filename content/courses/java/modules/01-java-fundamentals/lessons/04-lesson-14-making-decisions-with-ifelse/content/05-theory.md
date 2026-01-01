---
type: "THEORY"
title: "Comparison Operators"
---

To write conditions, you need to compare values. Java has these operators:

==  (equals)              5 == 5  → true
!=  (not equals)          5 != 3  → true
>   (greater than)        7 > 3   → true
<   (less than)           3 < 7   → true
>=  (greater or equal)    5 >= 5  → true
<=  (less or equal)       3 <= 7  → true

⚠️ WARNING: A common mistake!
- Use '==' to COMPARE: if (age == 18)
- Use '=' to ASSIGN: age = 18

if (age = 18) is WRONG! (You're assigning, not comparing)
if (age == 18) is CORRECT!