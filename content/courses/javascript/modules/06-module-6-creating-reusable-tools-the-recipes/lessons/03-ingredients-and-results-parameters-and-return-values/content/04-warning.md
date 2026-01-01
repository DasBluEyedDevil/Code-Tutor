---
type: "WARNING"
title: "Common Pitfalls"
---

Common mistakes:

1. Wrong number of arguments:
   function add(a, b) { return a + b; }
   add(5);  // b is undefined, returns NaN
   add(5, 3, 7);  // 7 is ignored

2. Not returning the value:
   function add(a, b) {
     a + b;  // Calculated but not returned!
   }
   let result = add(5, 3);  // undefined

3. Trying to use parameters outside function:
   function greet(name) {
     return 'Hello, ' + name;
   }
   console.log(name);  // ERROR - name only exists inside function

4. Forgetting to call the function:
   let result = add;  // result is the function itself
   let result = add(5, 3);  // result is 8

5. Returning in wrong place:
   function test() {
     if (true) {
       let x = 5;
       return x;  // WRONG - x might not be accessible
     }
   }
   Better: calculate first, then return