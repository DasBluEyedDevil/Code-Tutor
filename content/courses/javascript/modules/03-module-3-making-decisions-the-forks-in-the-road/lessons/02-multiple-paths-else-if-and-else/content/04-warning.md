---
type: "WARNING"
title: "Common Pitfalls"
---

Common mistakes:

1. Wrong order of conditions:
   if (score >= 60) { ... } else if (score >= 90) { ... }
   This is wrong! If score is 95, the first condition (>= 60) is true, so it stops there. Always check from most specific to least specific.

2. Using separate if statements instead of else if:
   if (temp > 80) { ... }
   if (temp > 60) { ... }  // WRONG - both could run!
   Instead use: else if (temp > 60) { ... }

3. Forgetting the 'else' keyword: writing if (cond1) { } if (cond2) { } won't work as intended.

4. Putting code between the blocks:
   if (x > 5) { }
   console.log('hello');  // This runs no matter what!
   else { }  // ERROR - can't have code between if and else