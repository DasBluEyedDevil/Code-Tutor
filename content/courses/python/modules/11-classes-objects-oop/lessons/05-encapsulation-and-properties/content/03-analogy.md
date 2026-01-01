---
type: "ANALOGY"
title: "Syntax Breakdown"
---

**Attribute access levels:**
```python
class MyClass:
    def __init__(self):
        self.public = "anyone can access"
        self._protected = "convention: internal use"
        self.__private = "name mangled"
```

**Property (read-only):**
```python
class MyClass:
    def __init__(self):
        self.__value = 0
    
    @property
    def value(self):
        return self.__value

obj = MyClass()
print(obj.value)      # OK - calls getter
obj.value = 10        # Error - no setter
```

**Property with setter:**
```python
class MyClass:
    def __init__(self):
        self.__value = 0
    
    @property
    def value(self):
        return self.__value
    
    @value.setter
    def value(self, val):
        if val < 0:
            raise ValueError("Must be positive")
        self.__value = val

obj = MyClass()
obj.value = 10        # OK - validated
obj.value = -5        # Error - validation failed
```

**Property with deleter:**
```python
@value.deleter
def value(self):
    print("Deleting value")
    del self.__value

del obj.value  # Calls deleter
```