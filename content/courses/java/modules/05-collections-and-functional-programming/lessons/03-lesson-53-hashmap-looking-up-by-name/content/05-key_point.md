---
type: "KEY_POINT"
title: "HashMap Performance and Use Cases"
---

PERFORMANCE:
- put(key, value): O(1) average - FAST!
- get(key): O(1) average - FAST!
- containsKey(key): O(1) average - FAST!

Compare to ArrayList search: O(n) - must check every element

WHEN TO USE HASHMAP:
✓ Need fast lookup by key
✓ Key-value associations (username → profile)
✓ Counting occurrences (word → count)
✓ Caching results (input → output)

WHEN NOT TO USE:
✗ Need to maintain order (use LinkedHashMap)
✗ Need sorting (use TreeMap)
✗ Just storing a list of items (use ArrayList)