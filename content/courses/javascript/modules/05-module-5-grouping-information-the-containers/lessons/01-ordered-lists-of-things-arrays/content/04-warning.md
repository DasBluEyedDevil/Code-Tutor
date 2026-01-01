---
type: "WARNING"
title: "Common Pitfalls"
---

Common mistakes:

1. Starting at index 1 instead of 0:
   array[1]  // This is the SECOND item, not first!
   array[0]  // This is the first item

2. Accessing index === length:
   let arr = ['a', 'b', 'c'];  // length is 3
   arr[3]  // undefined - no such index!
   // Valid indices: 0, 1, 2

3. Forgetting .length is a property, not a method:
   array.length()  // WRONG
   array.length    // CORRECT (no parentheses)

4. Confusing length with last index:
   let arr = ['a', 'b', 'c'];
   arr.length       // 3 (count of items)
   arr[arr.length]  // undefined!
   arr[arr.length - 1]  // 'c' (last item)

5. Treating arrays like single values:
   let arr = [1, 2, 3];
   console.log(arr);  // Prints whole array [1, 2, 3]
   console.log(arr[0]);  // Prints just first item: 1