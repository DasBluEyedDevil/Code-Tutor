---
type: "WARNING"
title: "Common Array Pitfalls"
---

ArrayIndexOutOfBoundsException:
int[] arr = new int[5];
arr[5] = 10;  // CRASH! Valid indexes are 0-4

Arrays start at 0, so arr[arr.length] is ALWAYS invalid.

Fixed size cannot change:
int[] arr = new int[3];
// Cannot add a 4th element - use ArrayList instead!

Default values:
- int[]: all zeros
- boolean[]: all false
- Object[]: all null (NullPointerException risk!)

For dynamic sizing, prefer ArrayList over arrays.