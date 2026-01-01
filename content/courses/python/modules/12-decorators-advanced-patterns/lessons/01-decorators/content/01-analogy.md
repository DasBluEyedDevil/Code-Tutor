---
type: "ANALOGY"
title: "The Concept: Gift Wrapping Functions"
---

**Decorators = Function wrappers**

**Think of gift wrapping:**
- You have a gift (function)
- You wrap it in fancy paper (decorator)
- The gift is the same, but now it's enhanced!

**What decorators do:**
- Add functionality to existing functions
- Without modifying the original function
- Reusable across multiple functions

**Common uses:**
1. **Logging** 📝 - Track when functions are called
2. **Timing** ⏱️ - Measure execution time
3. **Authentication** 🔐 - Check permissions
4. **Validation** ✅ - Verify inputs
5. **Caching** 💾 - Store results

**Basic syntax:**
```python
@decorator_name
def my_function():
    pass

# Same as:
my_function = decorator_name(my_function)
```

**Key insight:**
Decorators are functions that take a function and return a wrapped version.