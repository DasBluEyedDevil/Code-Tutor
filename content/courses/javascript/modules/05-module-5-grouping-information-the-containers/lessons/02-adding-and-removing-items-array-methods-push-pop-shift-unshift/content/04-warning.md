---
type: "WARNING"
title: "Common Pitfalls"
---

Common mistakes:

1. Forgetting these methods modify the array:
   let arr = [1, 2, 3];
   arr.push(4);  // arr is NOW [1, 2, 3, 4]
   // It's not arr2 = arr.push(4)

2. Expecting push/pop to work on both ends:
   arr.pop()  // Removes from END, not front
   // Use shift() to remove from front

3. Not using the return value:
   arr.pop();  // Item is removed AND returned
   let item = arr.pop();  // Save the removed item!

4. Confusing shift/unshift names:
   shift = remove first (shifts everything left)
   unshift = add to first (unshifts everything right)
   Confusing, but that's the name!

5. Using on non-arrays:
   let str = 'hello';
   str.push('x');  // ERROR - strings don't have push
   // Convert to array first: str.split('')