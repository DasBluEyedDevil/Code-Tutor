---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **Sets contain only unique items** - Duplicates are automatically removed
- **Create with curly braces**: `{"a", "b", "c"}` or `set([list])`
- **Empty set**: `set()` (NOT `{}` - that's an empty dict!)
- **`.add(item)`** adds one item, **`.update([items])`** adds multiple
- **`.remove(item)`** raises error if missing, **`.discard(item)`** doesn't
- **Membership test**: `item in my_set` is very fast!
- **Union**: `a | b` - All items from both sets
- **Intersection**: `a & b` - Items in both sets
- **Difference**: `a - b` - Items in a but not in b
- **Remove duplicates from list**: `unique = list(set(my_list))`