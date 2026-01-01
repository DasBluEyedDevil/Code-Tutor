---
type: "WARNING"
title: "Common Pitfalls"
---

Common mistakes:

1. Calling Object.keys() on non-object:
   Object.keys([1,2,3])  // Works but returns ['0', '1', '2'] (indices as strings)
   Object.keys('hello')  // Works but returns ['0', '1', '2', '3', '4']
   
2. Forgetting Object.keys() returns an array:
   Object.keys(obj)  // Returns ARRAY of keys
   // Must loop through: for (let key of Object.keys(obj))

3. Confusing for...of and for...in:
   for (let key of obj)  // ERROR - objects aren't iterable with for...of
   for (let key in obj)  // CORRECT - for...in works on objects
   for (let key of Object.keys(obj))  // ALSO CORRECT

4. Not using destructuring with entries:
   for (let entry of Object.entries(obj)) {
     console.log(entry[0], entry[1]);  // Works but clunky
   }
   for (let [key, value] of Object.entries(obj)) {
     console.log(key, value);  // Much cleaner!
   }

5. Expecting specific order:
   Object properties don't have a guaranteed order
   (Though modern JS usually maintains insertion order)