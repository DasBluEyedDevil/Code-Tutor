---
type: "ANALOGY"
title: "Syntax Breakdown"
---

**Instance vs Class Attributes:**
```python
class MyClass:
    # Class attribute (shared)
    class_var = "shared by all"
    
    def __init__(self):
        # Instance attribute (unique)
        self.instance_var = "unique to each"

obj = MyClass()
print(obj.instance_var)  # Unique
print(MyClass.class_var) # Shared
```

**Method decorators:**
```python
class MyClass:
    # Instance method (most common)
    def instance_method(self):
        return f"Instance: {self.attribute}"
    
    # Class method
    @classmethod
    def class_method(cls):
        return f"Class: {cls.class_attribute}"
    
    # Static method
    @staticmethod
    def static_method(arg):
        return f"Static: {arg}"
```

**When to use each:**
- **Instance method**: When you need object's data (self)
- **Class method**: Alternative constructors, class-level operations
- **Static method**: Utility functions that don't need instance/class data