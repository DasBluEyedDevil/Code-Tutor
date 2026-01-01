---
type: "ANALOGY"
title: "Understanding the Concept"
---

Imagine you're running a car dealership:

Without blueprints (plain JavaScript):
- Every car is just an object with random properties
- One car has 'color', another has 'colour'
- No one knows what properties a car should have
- Lots of inconsistency and confusion

With blueprints (TypeScript Interfaces):
- You create a 'Car Blueprint' that says every car must have:
  * make (string)
  * model (string)
  * year (number)
  * color (string)
- Now every car follows the same structure
- If someone forgets a property, the blueprint catches it
- Everyone knows exactly what a car object should look like

Interfaces are blueprints for objects. They define the exact shape your data should have.