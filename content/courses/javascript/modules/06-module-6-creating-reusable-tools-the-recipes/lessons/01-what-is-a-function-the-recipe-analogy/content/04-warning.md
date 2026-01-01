---
type: "WARNING"
title: "Common Pitfalls"
---

Common mistakes:

1. Forgetting parentheses when calling:
   greet  // Doesn't execute - just references the function
   greet()  // Executes the function

2. Confusing parameters and arguments:
   function greet(name) { }  // 'name' is a parameter (placeholder)
   greet('Alice');  // 'Alice' is an argument (actual value)

3. Not returning a value when you need one:
   function add(a, b) {
     a + b;  // WRONG - doesn't return anything!
   }
   function add(a, b) {
     return a + b;  // CORRECT
   }

4. Code after return never runs:
   function test() {
     return 5;
     console.log('This never runs!');  // Unreachable
   }

5. Wrong number of arguments:
   function add(a, b) { return a + b; }
   add(5);  // b is undefined, returns NaN
   add(5, 3, 7);  // Extra argument ignored, returns 8