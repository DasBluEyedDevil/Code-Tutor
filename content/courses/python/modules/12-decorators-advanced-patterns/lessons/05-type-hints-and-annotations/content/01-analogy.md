---
type: "ANALOGY"
title: "The Concept: Documentation in Code"
---

**Type Hints = Code documentation + Error prevention**

**Think of labeled containers:**

❌ **Without type hints:**
```python
def process(data, flag):
    # What type is data? String? List?
    # What's flag? Boolean? String?
    pass
```

✅ **With type hints:**
```python
def process(data: list[str], flag: bool) -> None:
    # Clear! data is list of strings
    # flag is boolean
    # Returns nothing
    pass
```

**Benefits:**

1. **Better documentation** 📖
   - See expected types at a glance
   - No need to guess

2. **IDE support** 🚀
   - Better autocomplete
   - Catch errors before running

3. **Error prevention** 🛡️
   - Type checkers find bugs
   - Before code runs!

4. **Code maintainability** 🔧
   - Easier for others to understand
   - Refactoring is safer

**Important:** Type hints are **optional** and **not enforced at runtime**!
- Python doesn't check types when running
- Use tools like mypy for type checking
- Mainly for development/tooling

**Common types:**
- `int`, `float`, `str`, `bool`
- `list`, `dict`, `set`, `tuple`
- `Optional[type]` - can be None
- `Union[type1, type2]` - can be either
- `Any` - any type