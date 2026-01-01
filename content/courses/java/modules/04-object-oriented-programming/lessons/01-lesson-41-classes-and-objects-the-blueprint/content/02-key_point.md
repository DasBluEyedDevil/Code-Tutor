---
type: "KEY_POINT"
title: "Classes are Blueprints, Objects are Houses"
---

Think of building houses:

BLUEPRINT (Class):
- Defines what a house HAS: rooms, doors, windows
- Defines what a house DOES: open door, turn on lights
- You can't live in a blueprint!

ACTUAL HOUSE (Object):
- Built FROM the blueprint
- Has actual values: 3 bedrooms, blue door, 5 windows
- You can interact with it

From ONE blueprint, you can build MANY houses, each with different colors, sizes, etc.

In Java:
- Class = Blueprint (you define it once)
- Object = Actual instance (you create many from the blueprint)

class Student { } // This is the blueprint
Student alice = new Student(); // This is an actual object