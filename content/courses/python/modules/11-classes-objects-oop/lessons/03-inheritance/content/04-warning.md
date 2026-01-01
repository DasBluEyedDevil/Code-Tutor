---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Forgetting super().__init__() in Child Class**
```python
# WRONG - Parent __init__ not called
class Animal:
    def __init__(self, name):
        self.name = name

class Dog(Animal):
    def __init__(self, name, breed):
        self.breed = breed  # self.name never set!

d = Dog("Rex", "Labrador")
print(d.name)  # AttributeError!

# CORRECT - Call parent's __init__
class Dog(Animal):
    def __init__(self, name, breed):
        super().__init__(name)
        self.breed = breed
```

**2. Incorrect super() Syntax**
```python
# WRONG - Old Python 2 style
class Dog(Animal):
    def __init__(self):
        super(Dog, self).__init__()  # Verbose, error-prone

# CORRECT - Modern Python 3 style
class Dog(Animal):
    def __init__(self):
        super().__init__()  # Cleaner, always correct
```

**3. Overriding Method Without Calling Parent**
```python
# WRONG - Parent logic completely replaced
class Animal:
    def speak(self):
        print(f"{self.name} makes a sound")
        log_sound()  # Important logging!

class Dog(Animal):
    def speak(self):  # Log never called!
        print("Woof!")

# CORRECT - Extend, don't replace (when appropriate)
class Dog(Animal):
    def speak(self):
        super().speak()  # Parent logging happens
        print("Woof!")  # Add dog-specific behavior
```

**4. Deep Inheritance Hierarchies**
```python
# WRONG - Too many levels, hard to understand
class A: pass
class B(A): pass
class C(B): pass
class D(C): pass
class E(D): pass  # 5 levels deep!

# CORRECT - Prefer composition over deep inheritance
class Engine: pass
class Car:
    def __init__(self):
        self.engine = Engine()  # Composition
```

**5. Not Checking isinstance for Type**
```python
# WRONG - Checking exact class misses subclasses
class Animal: pass
class Dog(Animal): pass

def feed(animal):
    if type(animal) == Animal:  # Dog is not exactly Animal!
        animal.eat()

# CORRECT - Use isinstance to include subclasses
def feed(animal):
    if isinstance(animal, Animal):  # Includes Dog!
        animal.eat()
```