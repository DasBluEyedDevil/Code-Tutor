---
type: "ANALOGY"
title: "Syntax Breakdown"
---

**Creating polymorphic behavior:**

**Step 1: Define common interface**
```python
class Base:
    def method(self):
        raise NotImplementedError("Must override")
```

**Step 2: Override in children**
```python
class Child1(Base):
    def method(self):
        return "Child1 implementation"

class Child2(Base):
    def method(self):
        return "Child2 implementation"
```

**Step 3: Use polymorphically**
```python
def process(obj):
    return obj.method()  # Works with any child!

process(Child1())  # → "Child1 implementation"
process(Child2())  # → "Child2 implementation"
```

**Duck typing:**
```python
def process(obj):
    # Don't check type, just call method
    return obj.method()  # Works with ANY object that has method()
```

**Key pattern:**
```python
# Instead of this (bad):
if isinstance(obj, Dog):
    obj.bark()
elif isinstance(obj, Cat):
    obj.meow()

# Do this (good):
obj.speak()  # All have speak(), different implementations
```