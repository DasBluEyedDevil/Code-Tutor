---
type: "THEORY"
title: "The Problem: ArrayList Insertion is Slow"
---

ArrayList stores elements in a continuous block:

[0] [1] [2] [3] [4]

What happens when you INSERT in the middle?

list.add(2, "NEW");  // Insert at index 2

ArrayList must:
1. Shift element at index 2 to index 3
2. Shift element at index 3 to index 4
3. Shift element at index 4 to index 5
4. THEN insert "NEW" at index 2

For 1 million elements, this is SLOW! O(n) time

LinkedList solves this differently!