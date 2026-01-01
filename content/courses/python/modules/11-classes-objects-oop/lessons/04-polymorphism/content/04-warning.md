---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Not Defining Common Interface in Base Class**
```python
# WRONG - No shared interface, inconsistent methods
class Dog:
    def bark(self):
        return "Woof!"

class Cat:
    def meow(self):  # Different method name!
        return "Meow!"

# CORRECT - Common interface in base class
class Animal:
    def speak(self):  # Define interface
        raise NotImplementedError

class Dog(Animal):
    def speak(self):
        return "Woof!"

class Cat(Animal):
    def speak(self):
        return "Meow!"
```

**2. Checking Type Instead of Behavior**
```python
# WRONG - Checking type breaks polymorphism
def process(animal):
    if isinstance(animal, Dog):
        animal.bark()
    elif isinstance(animal, Cat):
        animal.meow()  # Must change for every new animal!

# CORRECT - Rely on common interface
def process(animal):
    animal.speak()  # Works for any animal with speak()
```

**3. Forgetting to Override Method**
```python
# WRONG - Using parent's generic implementation
class Shape:
    def area(self):
        return 0  # Generic default

class Circle(Shape):
    def __init__(self, radius):
        self.radius = radius
    # Forgot to override area()!

c = Circle(5)
print(c.area())  # Returns 0 - wrong!

# CORRECT - Override method or use NotImplementedError
class Shape:
    def area(self):
        raise NotImplementedError("Subclass must implement")
```

**4. Breaking Liskov Substitution Principle**
```python
# WRONG - Child changes expected behavior
class Bird:
    def fly(self):
        return "Flying high!"

class Penguin(Bird):
    def fly(self):
        raise Exception("Can't fly!")  # Breaks expectations!

# CORRECT - Restructure inheritance
class Bird:
    pass

class FlyingBird(Bird):
    def fly(self):
        return "Flying high!"

class Penguin(Bird):  # Doesn't inherit fly()
    def swim(self):
        return "Swimming!"
```

**5. Not Using ABC for Abstract Base Classes**
```python
# WRONG - Can instantiate "abstract" class
class Shape:
    def area(self):
        raise NotImplementedError

s = Shape()  # Allowed but wrong!

# CORRECT - Use ABC to prevent instantiation
from abc import ABC, abstractmethod

class Shape(ABC):
    @abstractmethod
    def area(self):
        pass

s = Shape()  # TypeError: Can't instantiate abstract class
```