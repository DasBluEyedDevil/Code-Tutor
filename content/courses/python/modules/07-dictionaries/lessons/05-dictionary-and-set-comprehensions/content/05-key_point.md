---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **Dict comprehension**: `{key: value for item in iterable}`
- **Set comprehension**: `{expr for item in iterable}`
- **Add conditions**: `{k: v for item in list if condition}`
- **Use `.items()`** to iterate over dict key-value pairs
- **`zip(keys, values)`** combines two lists into pairs
- **Swap dict**: `{v: k for k, v in dict.items()}`
- **Keep it readable** - If comprehension gets complex, use a regular loop
- **Comprehensions are fast** - Often faster than equivalent loops
- **Set comprehension auto-deduplicates** - Great for unique values
- **Common pattern**: Transform list to lookup dict `{item.id: item for item in items}`