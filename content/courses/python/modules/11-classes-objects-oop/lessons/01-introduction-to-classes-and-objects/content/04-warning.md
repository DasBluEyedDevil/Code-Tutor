---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Forgetting self Parameter in Methods**
```python
# WRONG - Missing self parameter
class Dog:
    def bark():  # Missing self!
        print("Woof!")

d = Dog()
d.bark()  # TypeError: bark() takes 0 arguments but 1 was given

# CORRECT - Include self as first parameter
class Dog:
    def bark(self):
        print("Woof!")
```

**2. Not Using self to Access Instance Attributes**
```python
# WRONG - Accessing variable without self
class Person:
    def __init__(self, name):
        name = name  # Assigns to local variable, not attribute!

    def greet(self):
        print(name)  # NameError: name not defined

# CORRECT - Use self for instance attributes
class Person:
    def __init__(self, name):
        self.name = name  # Stores as instance attribute

    def greet(self):
        print(self.name)  # Accesses instance attribute
```

**3. Confusing Class and Instance**
```python
# WRONG - Calling method on class instead of instance
class Calculator:
    def add(self, a, b):
        return a + b

result = Calculator.add(1, 2)  # TypeError!

# CORRECT - Create instance first, then call method
class Calculator:
    def add(self, a, b):
        return a + b

calc = Calculator()  # Create instance
result = calc.add(1, 2)  # Call method on instance
```

**4. Mutable Default Argument in __init__**
```python
# WRONG - Mutable default shared between instances!
class Team:
    def __init__(self, members=[]):  # Shared list!
        self.members = members

t1 = Team()
t1.members.append("Alice")
t2 = Team()
print(t2.members)  # ['Alice'] - Unexpected!

# CORRECT - Use None and create new list
class Team:
    def __init__(self, members=None):
        self.members = members if members else []
```

**5. Printing Object Without __str__**
```python
# WRONG - No readable string representation
class Product:
    def __init__(self, name, price):
        self.name = name
        self.price = price

p = Product("Apple", 1.50)
print(p)  # <__main__.Product object at 0x...> - Useless!

# CORRECT - Define __str__ for readable output
class Product:
    def __init__(self, name, price):
        self.name = name
        self.price = price

    def __str__(self):
        return f"{self.name}: ${self.price}"
```