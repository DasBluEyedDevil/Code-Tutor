---
type: "EXAMPLE"
title: "Code Example: Your First Class"
---

**Key components:**
- `class Dog:` - Defines the class (blueprint)
- `__init__()` - Constructor, runs when creating object
- `self` - Refers to the current object instance
- `self.name` - Instance attribute (unique to each object)
- Methods - Functions inside a class
- `Dog(...)` - Creates a new object (instance)

**The `self` parameter:**
- First parameter of every instance method
- Python automatically passes it
- Refers to the specific object calling the method

```python
# Define a class (blueprint)
class Dog:
    # Constructor - runs when creating a new dog
    def __init__(self, name, age, breed):
        # Instance attributes (each dog has their own)
        self.name = name
        self.age = age
        self.breed = breed
    
    # Instance method (behavior)
    def bark(self):
        return f"{self.name} says: Woof! Woof!"
    
    def birthday(self):
        self.age += 1
        return f"Happy birthday {self.name}! Now {self.age} years old."
    
    def info(self):
        return f"{self.name} is a {self.age}-year-old {self.breed}"

# Create objects (actual dogs)
dog1 = Dog("Buddy", 3, "Golden Retriever")
dog2 = Dog("Max", 5, "Beagle")
dog3 = Dog("Luna", 2, "Husky")

print("=== Dog Objects ===")
print(dog1.info())
print(dog2.info())
print(dog3.info())

print("\n=== Dogs Barking ===")
print(dog1.bark())
print(dog2.bark())
print(dog3.bark())

print("\n=== Birthday Time ===")
print(dog1.birthday())
print(f"Updated info: {dog1.info()}")

# Accessing attributes
print("\n=== Accessing Attributes ===")
print(f"Dog 2's name: {dog2.name}")
print(f"Dog 3's age: {dog3.age}")

# Modifying attributes
dog2.breed = "Beagle Mix"
print(f"Updated: {dog2.info()}")
```
