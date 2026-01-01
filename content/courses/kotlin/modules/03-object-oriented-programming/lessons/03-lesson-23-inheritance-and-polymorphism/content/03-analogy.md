---
type: "ANALOGY"
title: "The Concept"
---


### What is Inheritance?

**Inheritance** is a mechanism where a new class (child/subclass) is based on an existing class (parent/superclass), inheriting its properties and methods.

**Real-World Analogy: Vehicle Hierarchy**


- **Vehicle** (parent): Has wheels, can move, has fuel
- **Car** (child): Inherits from Vehicle, adds doors and trunk
- **SportsCar** (grandchild): Inherits from Car, adds turbo boost

**Why Inheritance?**
- **Code Reuse**: Don't repeat common functionality
- **Logical Organization**: Model real-world relationships
- **Maintainability**: Change once, affect all subclasses
- **Polymorphism**: Treat different types uniformly

---



```kotlin
        Vehicle
       /   |   \
     Car  Bike  Truck
    /
  SportsCar
```
