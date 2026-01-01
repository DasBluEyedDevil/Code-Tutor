---
type: "WARNING"
title: "Common Pitfalls"
---

Common mistakes:

1. Forgetting parentheses with multiple parameters:
   const add = a, b => a + b;  // WRONG
   const add = (a, b) => a + b;  // CORRECT

2. Forgetting curly braces for multi-line:
   const greet = name =>
     let msg = 'Hello';
     return msg;  // WRONG - syntax error
   
   const greet = name => {
     let msg = 'Hello';
     return msg;  // CORRECT
   };

3. Trying to return an object without parentheses:
   const getUser = () => {name: 'Alice'};  // WRONG - thinks { } is function body
   const getUser = () => ({name: 'Alice'});  // CORRECT - wrapped in ()

4. Using arrow function as method:
   const person = {
     name: 'Alice',
     greet: () => console.log(this.name)  // WRONG - 'this' doesn't work as expected
   };
   Use traditional function for object methods

5. Mixing up = and =>:
   const add = (a, b) = a + b;  // WRONG - single =
   const add = (a, b) => a + b;  // CORRECT - arrow =>