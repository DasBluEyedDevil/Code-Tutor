---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **`.keys()`** returns all keys, **`.values()`** returns all values
- **`.items()`** returns key-value pairs as tuples - perfect for loops!
- **Loop through dict** - `for key in dict:` or `for key, value in dict.items():`
- **`.get(key, default)`** - Safe access that never raises KeyError
- **`.setdefault(key, default)`** - Get value or set it if missing
- **`.update(other_dict)`** - Merge another dictionary into this one
- **`.pop(key)`** - Remove key and return its value
- **`.clear()`** - Remove all items from dictionary
- **Python 3.9+**: Use `dict1 | dict2` to merge dictionaries
- **Common pattern**: `count[key] = count.get(key, 0) + 1` for counting