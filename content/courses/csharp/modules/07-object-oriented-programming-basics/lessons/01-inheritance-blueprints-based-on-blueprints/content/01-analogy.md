---
type: "ANALOGY"
title: "Understanding the Concept"
---

Imagine a car factory has a 'Vehicle' blueprint with wheels, engine, steering. Now they want to make a 'Car' - instead of starting from scratch, they say: 'Take the Vehicle blueprint and ADD doors, trunk, and seats!'

That's INHERITANCE! You create a new class BASED ON an existing one:
• The original class = BASE CLASS (or parent, superclass)
• The new class = DERIVED CLASS (or child, subclass)

The derived class INHERITS everything from the base class and can ADD NEW features or MODIFY existing ones.

Example: Animal (base) → Dog (derived)
• Animal has: Name, Age, Eat(), Sleep()
• Dog inherits ALL of those AND adds: Breed, Bark()

Inheritance promotes CODE REUSE - don't repeat yourself! Write common features once in the base class, share with all derived classes.