---
type: "THEORY"
title: "Breaking Down the Syntax"
---

The six comparison operators:

1. >   Greater than
   - 5 > 3 → true
   - 3 > 5 → false
   - 5 > 5 → false (not greater, they're equal)

2. <   Less than
   - 3 < 5 → true
   - 5 < 3 → false
   - 5 < 5 → false

3. >=  Greater than OR equal to
   - 5 >= 5 → true (the equal part makes it true)
   - 6 >= 5 → true
   - 4 >= 5 → false

4. <=  Less than OR equal to
   - 5 <= 5 → true
   - 4 <= 5 → true
   - 6 <= 5 → false

5. === Exactly equal to (strict equality)
   - 5 === 5 → true
   - 5 === '5' → false (number vs string)
   - 'cat' === 'cat' → true
   - 'Cat' === 'cat' → false (case matters!)

6. !== Not equal to (strict inequality)
   - 5 !== 3 → true (they are different)
   - 5 !== 5 → false (they are the same)

All comparison operators return a boolean (true or false). You can use them:
- Directly in if statements: if (age > 18)
- Store in variables: let canVote = age >= 18;
- Display them: console.log(5 > 3);  // Shows: true