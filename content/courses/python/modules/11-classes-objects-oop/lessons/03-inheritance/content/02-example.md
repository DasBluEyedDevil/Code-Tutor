---
type: "EXAMPLE"
title: "Code Example: Basic Inheritance"
---

**Key concepts:**

**Inheritance syntax:**
```python
class ChildClass(ParentClass):
    ...
```

**super() function:**
- Calls parent class methods
- Common in `__init__` to initialize parent attributes
- `super().__init__(args)` calls parent's constructor

**Method overriding:**
- Child can replace parent's method
- Same name, different implementation
- Dog overrides Animal's `info()` method

**Inherited features:**
- Dog, Cat, Bird all inherit `eat()` and `sleep()`
- No need to rewrite common functionality

```python
# Parent class (base class)
class Animal:
    def __init__(self, name, age):
        self.name = name
        self.age = age
        print(f"Animal created: {name}")
    
    def eat(self):
        return f"{self.name} is eating"
    
    def sleep(self):
        return f"{self.name} is sleeping"
    
    def info(self):
        return f"{self.name} is {self.age} years old"

# Child class (derived class)
class Dog(Animal):  # Inherits from Animal
    def __init__(self, name, age, breed):
        # Call parent's __init__
        super().__init__(name, age)
        # Add dog-specific attribute
        self.breed = breed
    
    def bark(self):
        return f"{self.name} says: Woof! Woof!"
    
    # Override parent method
    def info(self):
        return f"{self.name} is a {self.age}-year-old {self.breed}"

class Cat(Animal):  # Also inherits from Animal
    def __init__(self, name, age, indoor=True):
        super().__init__(name, age)
        self.indoor = indoor
    
    def meow(self):
        return f"{self.name} says: Meow!"
    
    def info(self):
        location = "indoor" if self.indoor else "outdoor"
        return f"{self.name} is a {self.age}-year-old {location} cat"

class Bird(Animal):
    def __init__(self, name, age, can_fly=True):
        super().__init__(name, age)
        self.can_fly = can_fly
    
    def chirp(self):
        return f"{self.name} says: Chirp chirp!"
    
    def fly(self):
        if self.can_fly:
            return f"{self.name} is flying!"
        return f"{self.name} can't fly (maybe a penguin?)"

print("=== Creating Animals ===")
dog = Dog("Buddy", 3, "Golden Retriever")
cat = Cat("Whiskers", 2, indoor=True)
bird = Bird("Tweety", 1, can_fly=True)
penguin = Bird("Pingu", 5, can_fly=False)

print("\n=== Inherited Methods (from Animal) ===")
print(dog.eat())
print(cat.sleep())
print(bird.eat())

print("\n=== Child-Specific Methods ===")
print(dog.bark())
print(cat.meow())
print(bird.chirp())
print(bird.fly())
print(penguin.fly())

print("\n=== Overridden info() Method ===")
print(dog.info())
print(cat.info())
print(bird.info())

print("\n=== Checking Inheritance ===")
print(f"Is dog an Animal? {isinstance(dog, Animal)}")
print(f"Is dog a Dog? {isinstance(dog, Dog)}")
print(f"Is dog a Cat? {isinstance(dog, Cat)}")
print(f"Is Dog a subclass of Animal? {issubclass(Dog, Animal)}")
```
