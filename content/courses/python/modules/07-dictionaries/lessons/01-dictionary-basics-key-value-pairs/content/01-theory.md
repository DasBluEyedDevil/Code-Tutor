---
type: "THEORY"
title: "Understanding the Concept"
---

Think about a real dictionary - the kind you use to look up words. You don't read it from page 1 to page 500 looking for a word. Instead, you look up the word directly and find its definition.

**Python dictionaries work the same way!**

Instead of storing items by position (like lists do), dictionaries store items by **keys**. Each key is paired with a **value**.

```python
# A list stores by position (index)
fruits = ["apple", "banana", "cherry"]
print(fruits[0])  # "apple" - you need to know position 0

# A dictionary stores by key
prices = {"apple": 1.50, "banana": 0.75, "cherry": 3.00}
print(prices["apple"])  # 1.50 - you look up by name!
```

**Real-world examples of key-value pairs:**

- Phone contacts: name -> phone number
- Student grades: student ID -> grade
- Product inventory: product code -> quantity
- User profiles: username -> user data
- Config settings: setting name -> setting value

**Why use dictionaries?**

- **Fast lookups** - Find data instantly by key (no searching)
- **Meaningful access** - Use descriptive keys instead of remembering positions
- **Flexible structure** - Add or remove items easily
- **Real-world data** - Most data naturally has a key-value structure