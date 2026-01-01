---
type: "WARNING"
title: "Common Pitfalls"
---

Common mistakes:

1. Using variables outside their scope:
   function test() {
     let x = 5;
   }
   console.log(x);  // ERROR - x only exists inside test()

2. Forgetting var escapes block scope:
   if (true) {
     var x = 5;  // Function scoped, not block scoped!
   }
   console.log(x);  // 5 - var leaks out
   
   Always use let/const, never var!

3. Shadowing by accident:
   let name = 'Alice';
   function greet() {
     let name = 'Bob';  // Different variable!
   }
   Confusing - use different names

4. Trying to access parameters outside function:
   function add(a, b) {
     return a + b;
   }
   console.log(a);  // ERROR - parameters are function-scoped

5. Not understanding hoisting:
   console.log(x);  // undefined (not error with var)
   var x = 5;
   
   vs
   
   console.log(x);  // ERROR - cannot access before initialization
   let x = 5;
   
   Another reason to use let/const!