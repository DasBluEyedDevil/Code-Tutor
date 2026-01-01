---
type: "KEY_POINT"
title: "Inheritance Best Practices"
---

1. IS-A RELATIONSHIP:
   - Dog IS-A Animal ✓ (inheritance makes sense)
   - Car IS-A Engine ✗ (Car HAS-A Engine, use composition instead)

2. INHERITANCE HIERARCHY:
   Object (built-in Java class)
     ↑
   Animal
     ↑
   Dog, Cat, Bird (all extend Animal)

3. EVERYTHING EXTENDS Object:
   - Every Java class automatically extends Object
   - Object provides: toString(), equals(), hashCode()

4. SINGLE INHERITANCE ONLY:
   - Java: class Dog extends Animal (ONE parent only)
   - Multiple inheritance causes problems (diamond problem)
   - Use interfaces for multiple "contracts" (later lesson)