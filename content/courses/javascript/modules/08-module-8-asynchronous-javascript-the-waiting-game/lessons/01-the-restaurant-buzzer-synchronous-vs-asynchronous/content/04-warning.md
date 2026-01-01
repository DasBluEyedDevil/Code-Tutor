---
type: "WARNING"
title: "Common Pitfalls"
---

Common mistakes:

1. Expecting async code to run in order:
   setTimeout(() => console.log('A'), 1000);
   console.log('B');
   // Output: B, (wait), A  (not A, B!)

2. Not storing interval ID to clear it:
   setInterval(() => console.log('Hi'), 1000);
   // Can't stop it now!
   
   Correct:
   let id = setInterval(() => console.log('Hi'), 1000);
   clearInterval(id);  // Can stop it

3. Confusing milliseconds with seconds:
   setTimeout(() => console.log('Hi'), 5);  // 5 milliseconds!
   setTimeout(() => console.log('Hi'), 5000);  // 5 seconds

4. Creating infinite intervals:
   setInterval(() => {
     // Runs forever!
   }, 1000);
   // Remember to clearInterval when done

5. Trying to 'wait' for async with sync code:
   setTimeout(() => data = fetchData(), 1000);
   console.log(data);  // undefined! Timeout hasn't run yet
   
   Must use callbacks or promises (next lessons)