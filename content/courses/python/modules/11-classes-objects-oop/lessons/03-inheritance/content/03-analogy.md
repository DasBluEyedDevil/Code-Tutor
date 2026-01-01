---
type: "ANALOGY"
title: "Syntax Breakdown"
---

**Creating inheritance:**
```python
# Parent class
class Parent:
    def __init__(self, x):
        self.x = x
    
    def method(self):
        return "parent"

# Child class
class Child(Parent):  # Inherit from Parent
    def __init__(self, x, y):
        super().__init__(x)  # Call parent's __init__
        self.y = y
    
    # Override parent method
    def method(self):
        return "child"
```

**super() function:**
```python
super().__init__(args)     # Call parent's __init__
super().method(args)       # Call parent's method
```

**Multiple levels:**
```python
class Grandparent:
    pass

class Parent(Grandparent):
    pass

class Child(Parent):
    pass  # Child inherits from both Parent and Grandparent
```

**Checking relationships:**
```python
isinstance(obj, ClassName)  # Is obj an instance of ClassName?
issubclass(Child, Parent)   # Is Child a subclass of Parent?
```