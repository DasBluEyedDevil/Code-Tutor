---
type: "KEY_POINT"
title: "The Collection Interface Hierarchy"
---

All collections implement common interfaces:

Collection (interface)
  |
  ├─ List (interface)
  │   ├─ ArrayList (class)
  │   └─ LinkedList (class)
  │
  ├─ Set (interface) - No duplicates
  │   ├─ HashSet (class)
  │   └─ TreeSet (class) - Sorted
  │
  └─ Queue (interface)
      └─ LinkedList (class)

Map (separate hierarchy)
  ├─ HashMap (class)
  ├─ LinkedHashMap (maintains order)
  └─ TreeMap (sorted by key)

Common methods ALL collections share:
- add(element)
- remove(element)
- size()
- isEmpty()
- clear()
- contains(element)