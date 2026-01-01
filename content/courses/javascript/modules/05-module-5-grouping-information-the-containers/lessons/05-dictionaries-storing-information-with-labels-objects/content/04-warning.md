---
type: "WARNING"
title: "Common Pitfalls"
---

Common mistakes:

1. Forgetting commas between properties:
   {name: 'Alice' age: 25}  // WRONG - missing comma
   {name: 'Alice', age: 25}  // CORRECT

2. Using = instead of ::
   {name = 'Alice'}  // WRONG
   {name: 'Alice'}   // CORRECT

3. Trailing comma on last property:
   {name: 'Alice', age: 25,}  // Works in modern JS, but some old browsers error

4. Confusing arrays and objects:
   let arr = [1, 2, 3];    // Square brackets
   let obj = {a: 1, b: 2}; // Curly braces

5. Trying to use dot notation with spaces:
   obj.first name  // WRONG
   obj['first name']  // CORRECT
   obj.firstName   // BETTER - use camelCase

6. Expecting specific order:
   Objects don't guarantee property order (though modern JS usually preserves it)
   If order matters, use an array!