---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Confusing Class vs Instance Attributes**
```python
# WRONG - Modifying class attribute via instance
class Counter:
    count = 0  # Class attribute

c1 = Counter()
c1.count = 5  # Creates INSTANCE attribute, doesn't change class!
c2 = Counter()
print(c2.count)  # Still 0!

# CORRECT - Use class name to modify class attribute
class Counter:
    count = 0

Counter.count = 5  # Modifies class attribute
print(Counter.count)  # 5
```

**2. Mutable Class Attribute Shared Unexpectedly**
```python
# WRONG - List as class attribute is shared!
class Team:
    members = []  # Shared between all instances!

t1 = Team()
t1.members.append("Alice")
t2 = Team()
print(t2.members)  # ['Alice'] - Wrong!

# CORRECT - Initialize mutable attributes in __init__
class Team:
    def __init__(self):
        self.members = []  # Each instance gets own list
```

**3. Forgetting cls in Class Methods**
```python
# WRONG - Using self in class method
class Person:
    count = 0

    @classmethod
    def get_count(self):  # Should be cls!
        return Person.count

# CORRECT - Use cls as first parameter
class Person:
    count = 0

    @classmethod
    def get_count(cls):
        return cls.count
```

**4. Using self in Static Methods**
```python
# WRONG - Static methods don't have self
class Math:
    @staticmethod
    def add(self, a, b):  # self not available!
        return a + b

# CORRECT - Static methods have no self or cls
class Math:
    @staticmethod
    def add(a, b):
        return a + b
```

**5. Not Using @classmethod for Alternative Constructors**
```python
# WRONG - Regular method for construction
class Date:
    def __init__(self, year, month, day):
        self.year = year
        self.month = month
        self.day = day

    def from_string(self, s):  # Returns wrong class in subclasses!
        parts = s.split('-')
        return Date(*parts)

# CORRECT - Use classmethod for alternative constructors
class Date:
    def __init__(self, year, month, day):
        self.year = year
        self.month = month
        self.day = day

    @classmethod
    def from_string(cls, s):
        parts = s.split('-')
        return cls(*parts)  # Works with subclasses!
```