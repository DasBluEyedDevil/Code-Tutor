---
type: "THEORY"
title: "ArrayList vs LinkedList: Performance Comparison"
---

OPERATION          | ArrayList | LinkedList
-------------------|-----------|-----------
get(index)         | O(1) ✓    | O(n) ✗
add(element)       | O(1) ✓    | O(1) ✓
add(index, elem)   | O(n) ✗    | O(n) ~
addFirst(elem)     | O(n) ✗    | O(1) ✓
addLast(elem)      | O(1) ✓    | O(1) ✓
remove(index)      | O(n) ✗    | O(n) ~
removeFirst()      | O(n) ✗    | O(1) ✓
removeLast()       | O(1) ✓    | O(1) ✓

O(1) = Constant time (FAST)
O(n) = Linear time (scales with size)

SUMMARY:
ArrayList: Fast random access, slow insertions
LinkedList: Slow random access, fast insertions at ends