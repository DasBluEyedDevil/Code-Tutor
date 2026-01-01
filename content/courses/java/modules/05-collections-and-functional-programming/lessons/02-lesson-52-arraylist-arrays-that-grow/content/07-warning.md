---
type: "WARNING"
title: "ArrayList Gotchas"
---

IndexOutOfBoundsException:
ArrayList<String> list = new ArrayList<>();
list.get(0);  // CRASH! List is empty
Always check size() before accessing by index.

Remove while iterating (ConcurrentModificationException):
for (String s : list) {
    if (condition) list.remove(s);  // CRASH!
}
Use removeIf() instead:
list.removeIf(s -> s.startsWith("test"));

Primitive remove() confusion:
ArrayList<Integer> nums = new ArrayList<>();
nums.add(1); nums.add(2);
nums.remove(1);  // Removes at INDEX 1, not value 1!
nums.remove(Integer.valueOf(1));  // Removes VALUE 1

Java 21+ Sequenced Collections:
ArrayList now supports getFirst(), getLast(), addFirst(), reversed().