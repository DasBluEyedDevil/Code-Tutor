---
type: "WARNING"
title: "Common Pitfalls"
---

Common mistakes:

1. Off-by-one errors (VERY common!):
   for (let i = 0; i < 5; i++)  // Runs 5 times (0,1,2,3,4)
   for (let i = 1; i < 5; i++)  // Runs 4 times (1,2,3,4)
   for (let i = 1; i <= 5; i++) // Runs 5 times (1,2,3,4,5)
   Always test: does this loop run the right number of times?

2. Infinite loops:
   for (let i = 0; i < 5; i--) // i gets SMALLER, never reaches 5!
   This will crash your program!

3. Forgetting to increment:
   for (let i = 0; i < 5; ) // Missing i++
   Another infinite loop!

4. Using = instead of ==:
   for (let i = 0; i = 5; i++)  // WRONG - assigns 5 to i!
   for (let i = 0; i < 5; i++)  // CORRECT

5. Modifying the loop variable inside:
   for (let i = 0; i < 5; i++) {
     i = 0;  // BAD - creates infinite loop!
   }
   Don't change i inside the loop body!