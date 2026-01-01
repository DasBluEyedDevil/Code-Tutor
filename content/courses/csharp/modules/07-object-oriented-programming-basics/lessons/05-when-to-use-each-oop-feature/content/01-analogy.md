---
type: "ANALOGY"
title: "Understanding the Concept"
---

You've learned many OOP tools! But WHEN do you use each one? Think of it like a toolbox:

🔧 **INHERITANCE**: Use when there's an 'IS-A' relationship. Dog IS-A Animal, Car IS-A Vehicle. Share common features.

🎨 **ABSTRACT CLASSES**: Use when you want to provide SOME implementation but force derived classes to complete it. Template pattern.

📋 **INTERFACES**: Use when you want to define a CONTRACT without implementation. Multiple classes can implement same interface even if completely unrelated.

🔄 **POLYMORPHISM**: Use when you want different classes to respond to the same method call differently. Shape.Draw() draws differently for Circle vs Rectangle.

Rule of thumb:
• Inheritance: Share CODE (implementation)
• Interfaces: Share CONTRACT (what must be done)
• Abstract: Share BOTH (some code + force completion)