---
type: "WARNING"
title: "Common Pitfalls"
---

Common mistakes:

1. Using 'in' instead of 'of':
   for (let fruit in fruits)  // WRONG - gives index, not item!
   for (let fruit of fruits)  // CORRECT - gives the item
   
   Confusing, right? Remember: 'of' for values, 'in' for keys (we'll learn later)

2. Trying to get the index:
   for (let fruit of fruits) {
     console.log(i);  // ERROR - i doesn't exist!
   }
   If you need the index, use a regular for loop or .forEach() (later)

3. Modifying the array while looping:
   for (let fruit of fruits) {
     fruits.push('new');  // DANGEROUS - might cause infinite loop!
   }
   Don't modify the array you're looping through

4. Expecting it to work on objects:
   let person = {name: 'Alice', age: 25};
   for (let prop of person)  // ERROR - for...of doesn't work on plain objects!
   Use for...in for objects (later lesson)

5. Using wrong variable name:
   for (let fruit of fruits) {
     console.log(fruits);  // Prints whole array each time!
   }
   Should be: console.log(fruit);  // Just one item