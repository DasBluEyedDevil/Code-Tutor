---
type: "THEORY"
title: "Understanding the Concept"
---

Think about a guest list for a party. You don't want the same person listed twice - each guest should appear exactly once. That's what a **set** does!

**Sets are collections where every item is unique:**

```python
# Lists can have duplicates
guest_list = ["Alice", "Bob", "Alice", "Charlie", "Bob"]
print(guest_list)  # ['Alice', 'Bob', 'Alice', 'Charlie', 'Bob']

# Sets automatically remove duplicates!
guests = {"Alice", "Bob", "Alice", "Charlie", "Bob"}
print(guests)  # {'Alice', 'Bob', 'Charlie'} - only 3 unique names
```

**Key characteristics of sets:**

- **Unique items only** - Duplicates are automatically removed
- **Unordered** - No index positions (can't do `my_set[0]`)
- **Fast membership testing** - `"Alice" in guests` is very fast
- **Set operations** - Union, intersection, difference (like math sets!)

**When to use sets:**

- Remove duplicates from a list
- Track unique items (visited pages, seen IDs, etc.)
- Fast membership checking
- Set operations (find common items, differences, etc.)