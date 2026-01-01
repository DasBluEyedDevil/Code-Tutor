---
type: "ANALOGY"
title: "The Concept: Compact List Creation"
---

**Comprehensions = Concise collection creation**

**Think of a shopping list:**

❌ **Traditional approach (loop):**
```python
shopping_list = []
for item in all_items:
    if item.price < 10:
        shopping_list.append(item.name)
```

✅ **Comprehension (one line):**
```python
shopping_list = [item.name for item in all_items if item.price < 10]
```

**Types of comprehensions:**

1. **List comprehension** `[...]`
   ```python
   squares = [x**2 for x in range(10)]
   ```

2. **Dictionary comprehension** `{key: value}`
   ```python
   squares_dict = {x: x**2 for x in range(10)}
   ```

3. **Set comprehension** `{value}`
   ```python
   unique_lengths = {len(word) for word in words}
   ```

4. **Generator expression** `(...)`
   ```python
   squares_gen = (x**2 for x in range(10))
   ```

**When to use comprehensions:**
- ✅ Simple transformations
- ✅ Filtering collections
- ✅ Creating new collections
- ❌ Complex logic (use regular loops)
- ❌ Side effects (printing, file I/O)

**Benefits:**
- More readable (when simple)
- Often faster
- More Pythonic
- Less code