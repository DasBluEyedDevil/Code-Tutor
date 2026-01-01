---
type: "ANALOGY"
title: "The Concept: Many Forms, Same Interface"
---

**Polymorphism** = "Many forms"

**Think of it like a TV remote:**
- Same button ("Play") works on:
  - DVD player → plays DVD
  - Streaming box → plays stream
  - Game console → starts game
- **Same interface, different behavior**

**In Python:**
```
Same method name, different implementations:

dog.speak() → "Woof!"
cat.speak() → "Meow!"
bird.speak() → "Chirp!"

All respond to speak(), each does it differently
```

**Two types of polymorphism:**

**1. Method Overriding** (inheritance-based)
- Child classes override parent method
- Same method name, different implementation
```python
class Animal:
    def speak(self): pass

class Dog(Animal):
    def speak(self): return "Woof"

class Cat(Animal):
    def speak(self): return "Meow"
```

**2. Duck Typing** ("If it walks like a duck...")
- Don't check type, check behavior
- If it has the method, use it!
```python
# Don't care about type, just that it has speak()
for animal in animals:
    print(animal.speak())  # Works for any object with speak()
```

**Benefits:**
- Write generic code
- Easy to extend
- Flexible and reusable