---
type: "WARNING"
title: "Common Pitfalls"
---

Common mistakes:

1. Forgetting to update the condition variable:
   let x = 0;
   while (x < 10) {
     console.log(x);  // x never changes - INFINITE LOOP!
   }

2. Updating in the wrong direction:
   let x = 0;
   while (x < 10) {
     console.log(x);
     x--;  // x gets MORE negative - INFINITE LOOP!
   }

3. Wrong comparison operator:
   let x = 0;
   while (x < 10) {
     console.log(x);
     x--;  // Should be x++
   }

4. Condition that's never true:
   let x = 5;
   while (x < 0) {  // 5 is not < 0
     // This never runs at all!
   }

5. Not initializing before the loop:
   while (count < 10) {  // count is not defined!
     count++;
   }
   Must declare: let count = 0; BEFORE the while loop