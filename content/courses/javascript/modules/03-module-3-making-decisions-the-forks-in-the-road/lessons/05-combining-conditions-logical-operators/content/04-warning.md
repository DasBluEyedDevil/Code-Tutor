---
type: "WARNING"
title: "Common Pitfalls"
---

Common mistakes:

1. Using 'and' or 'or' instead of symbols:
   if (age > 18 and hasLicense)  // WRONG
   if (age > 18 && hasLicense)   // CORRECT

2. Confusing && and ||:
   if (isWeekend || hasWork)  // Do I work on weekends or any day?
   vs
   if (isWeekend && !hasWork) // Free weekend?
   Read them out loud to check!

3. Forgetting parentheses with mixed operators:
   if (a || b && c)  // Unclear!
   if (a || (b && c))  // Better
   if ((a || b) && c)  // Different meaning!

4. Double negatives:
   if (!!isLoggedIn)  // Just use: if (isLoggedIn)
   Don't overthink it!

5. Not understanding short-circuit:
   if (user && user.name)  // Safe - checks user exists first
   if (user.name && user)  // DANGEROUS - might error if user is null

6. Trying to check multiple values at once:
   if (x === 1 || 2 || 3)  // WRONG - doesn't work!
   if (x === 1 || x === 2 || x === 3)  // CORRECT
   Or better: if ([1,2,3].includes(x))  // We'll learn this later!