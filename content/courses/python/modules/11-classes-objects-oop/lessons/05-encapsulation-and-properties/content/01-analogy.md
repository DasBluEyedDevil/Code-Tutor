---
type: "ANALOGY"
title: "The Concept: Information Hiding"
---

**Encapsulation** = Bundling data with methods that operate on that data, and restricting direct access

**Think of a car:**
- You don't access engine internals directly
- You use interface: steering wheel, pedals, gear shift
- Engine details are hidden (encapsulated)
- Prevents mistakes (can't accidentally break transmission)

**In Python:**

**Public** (normal attributes)
```python
self.name = "John"  # Anyone can access/modify
obj.name = "Jane"   # Direct access
```

**Protected** (single underscore)
```python
self._internal = value  # Convention: "please don't touch"
# Not enforced, just a hint to other developers
```

**Private** (double underscore)
```python
self.__secret = value  # Name mangling, harder to access
# Becomes _ClassName__secret
```

**Why encapsulate?**

1. **Validation** ✓
   - Check values before setting
   - Prevent invalid states

2. **Controlled access** 🔒
   - Read-only attributes
   - Computed values

3. **Change implementation** 🔧
   - Modify internals without breaking code
   - Maintain backward compatibility

4. **Hide complexity** 🎭
   - Simple interface, complex internals