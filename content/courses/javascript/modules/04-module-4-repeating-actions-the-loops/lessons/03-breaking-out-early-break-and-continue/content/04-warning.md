---
type: "WARNING"
title: "Common Pitfalls"
---

Common mistakes:

1. Using break outside a loop:
   if (x > 5) {
     break;  // ERROR - not in a loop!
   }
   break only works inside loops or switch statements

2. Confusing break and return:
   - break exits a loop
   - return exits a function (we'll learn soon)
   Don't mix them up!

3. Expecting break to exit nested loops:
   for (let i = 0; i < 3; i++) {
     for (let j = 0; j < 3; j++) {
       if (j === 1) break;  // Only exits inner loop!
     }
   }
   To exit all loops, use a flag or a function with return

4. Forgetting to update counter before continue:
   let i = 0;
   while (i < 10) {
     if (i === 5) continue;  // INFINITE LOOP!
     console.log(i);
     i++;  // Never reached when i is 5
   }
   Fix: Put i++ before the continue check

5. Overusing break/continue:
   Sometimes an if statement is clearer:
   for (let i = 0; i < 10; i++) {
     if (i !== 5) {  // Clearer than using continue
       console.log(i);
     }
   }