---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Using Single Underscore Thinking It's Private**
```python
# WRONG - Single underscore is just convention, not enforced
class Account:
    def __init__(self):
        self._balance = 100  # "Private" by convention only

acc = Account()
acc._balance = -1000  # Still accessible!

# CORRECT - Use double underscore for name mangling
class Account:
    def __init__(self):
        self.__balance = 100  # Name-mangled to _Account__balance

acc = Account()
acc.__balance = -1000  # Creates NEW attribute, doesn't modify original!
```

**2. Property Without Setter Allows No Modification**
```python
# WRONG - Read-only property but trying to set
class Person:
    def __init__(self, name):
        self._name = name

    @property
    def name(self):
        return self._name

p = Person("Alice")
p.name = "Bob"  # AttributeError: can't set attribute

# CORRECT - Add setter if modification needed
class Person:
    def __init__(self, name):
        self._name = name

    @property
    def name(self):
        return self._name

    @name.setter
    def name(self, value):
        self._name = value
```

**3. Property Setter Not Validating**
```python
# WRONG - Setter allows invalid values
class Circle:
    @property
    def radius(self):
        return self._radius

    @radius.setter
    def radius(self, value):
        self._radius = value  # No validation!

c = Circle()
c.radius = -5  # Invalid but accepted!

# CORRECT - Validate in setter
class Circle:
    @radius.setter
    def radius(self, value):
        if value < 0:
            raise ValueError("Radius must be positive")
        self._radius = value
```

**4. Computing Property Every Access**
```python
# WRONG - Expensive computation on every access
class Report:
    @property
    def summary(self):
        # Queries database every time!
        return database.generate_report()

r = Report()
print(r.summary)  # Database query
print(r.summary)  # Another query!

# CORRECT - Cache computed values
class Report:
    def __init__(self):
        self._summary = None

    @property
    def summary(self):
        if self._summary is None:
            self._summary = database.generate_report()
        return self._summary
```

**5. Accessing Private Attributes Directly in Subclass**
```python
# WRONG - Name mangling affects subclass access
class Parent:
    def __init__(self):
        self.__secret = 42

class Child(Parent):
    def reveal(self):
        return self.__secret  # AttributeError!

# CORRECT - Use protected (_) or provide accessor
class Parent:
    def __init__(self):
        self._secret = 42  # Protected, not private

class Child(Parent):
    def reveal(self):
        return self._secret  # Works
```