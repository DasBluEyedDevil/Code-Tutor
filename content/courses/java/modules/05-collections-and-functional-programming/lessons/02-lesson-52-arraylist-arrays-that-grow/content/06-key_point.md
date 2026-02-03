---
type: "KEY_POINT"
title: "Common ArrayList Methods"
---

ADDING:
- add(item) → adds to end
- add(index, item) → inserts at position

REMOVING:
- remove(index) → removes at position
- remove(object) → removes first occurrence
- clear() → removes all

ACCESSING:
- get(index) → retrieves element
- set(index, item) → replaces element

QUERYING:
- size() → number of elements
- isEmpty() → true if empty
- contains(item) → true if found
- indexOf(item) → position of item (-1 if not found)

LOOPING:
for (int i = 0; i < list.size(); i++) {
    IO.println(list.get(i));
}

// Enhanced for loop (easier!)
for (String item : list) {
    IO.println(item);
}