---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **Dictionaries store key-value pairs** - Look up values by their key, not position
- **Create with curly braces**: `{"key": value, "key2": value2}`
- **Access values**: `dict["key"]` or safer `dict.get("key")`
- **Add/modify**: `dict["new_key"] = value`
- **Delete**: `del dict["key"]`
- **Check key exists**: `"key" in dict`
- **Get length**: `len(dict)`
- **Keys must be immutable** - Strings, numbers, tuples (not lists!)
- **Values can be anything** - Strings, numbers, lists, even other dictionaries
- **Use `get()` with default** - `dict.get("key", "default")` prevents KeyError