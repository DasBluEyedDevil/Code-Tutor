---
type: "THEORY"
title: "ArrayList vs Array: Key Differences"
---

ARRAY:
int[] arr = new int[5];
- Fixed size: 5 elements forever
- Access: arr[0]
- Length: arr.length
- Can hold primitives (int, double, char)

ARRAYLIST:
ArrayList<Integer> list = new ArrayList<>();
- Dynamic size: grows/shrinks
- Access: list.get(0)
- Length: list.size()
- Only holds objects (Integer, String, not int)

AUTOBOXING (automatic conversion):
ArrayList<Integer> nums = new ArrayList<>();
nums.add(5);  // int 5 → Integer 5 (autoboxing)
int x = nums.get(0);  // Integer → int (auto-unboxing)