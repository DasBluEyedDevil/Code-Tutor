---
type: "THEORY"
title: "Breaking Down the Syntax"
---

The four basic array methods:

1. push(item) - add to END
   - Adds one or more items to the end
   - Returns new length
   - Example: arr.push('x') → adds 'x' to end

2. pop() - remove from END
   - Removes last item
   - Returns the removed item
   - Example: let last = arr.pop()

3. unshift(item) - add to FRONT
   - Adds one or more items to the beginning
   - Shifts all existing items to higher indices
   - Returns new length
   - Example: arr.unshift('x') → adds 'x' to front

4. shift() - remove from FRONT
   - Removes first item
   - Shifts all remaining items to lower indices
   - Returns the removed item
   - Example: let first = arr.shift()

Memory trick:
- push/pop: work with the END (both have 'p')
- shift/unshift: work with the FRONT
- push/unshift: ADD items
- pop/shift: REMOVE items

Performance notes:
- push() and pop(): Fast (O(1))
- shift() and unshift(): Slower (O(n)) because indices must be recalculated
- For large arrays, avoid shift/unshift if performance matters

These methods MODIFY the original array:
let arr = [1, 2];
arr.push(3);  // arr is now [1, 2, 3]
// The original array changed!