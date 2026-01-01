---
type: "THEORY"
title: "Other Useful Collections Methods"
---

REVERSE:
Collections.reverse(list);  // Reverse the order

SHUFFLE:
Collections.shuffle(list);  // Randomize order

MAX AND MIN:
ArrayList<Integer> nums = new ArrayList<>();
nums.add(5);
nums.add(2);
nums.add(9);
int max = Collections.max(nums);  // 9
int min = Collections.min(nums);  // 2

FREQUENCY:
ArrayList<String> items = new ArrayList<>();
items.add("apple");
items.add("banana");
items.add("apple");
int count = Collections.frequency(items, "apple");  // 2

FILL:
Collections.fill(list, "X");  // Replace all with "X"