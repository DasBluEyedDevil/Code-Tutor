---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **Dictionaries are perfect for lookups** - Store contacts by normalized name for fast access
- **Nested dictionaries** hold complex data - Each contact has name, phone, email, tags
- **Sets for unique values** - Tags are stored as sets (no duplicates, fast membership)
- **Functions organize code** - Each operation is a separate, reusable function
- **Normalize keys** - Use lowercase for case-insensitive lookup
- **Return values, not prints** - Functions return data; callers decide what to display
- **Handle edge cases** - What if contact doesn't exist? What if name is empty?
- **Use `.get()` for safety** - Avoid KeyError when contact might not exist
- **Comprehensions simplify** - `[c["name"] for c in contacts.values() if ...]`
- **This pattern scales** - Same approach works for inventory, users, products, etc.