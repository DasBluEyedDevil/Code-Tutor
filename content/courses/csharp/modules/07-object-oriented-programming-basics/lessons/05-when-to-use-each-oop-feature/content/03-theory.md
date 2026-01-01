---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`Decision Tree: Inheritance`**: Use inheritance when: 1) Classes have IS-A relationship, 2) Want to share implementation, 3) Have clear hierarchy. Avoid deep inheritance (3+ levels gets complex).

**`Decision Tree: Abstract Class`**: Use abstract when: 1) Some methods don't make sense in base class, 2) Want to provide SOME shared code, 3) Force derived classes to implement certain methods.

**`Decision Tree: Interface`**: Use interface when: 1) Define behavior contract, 2) No shared implementation needed, 3) Want multiple 'capabilities' (IDrawable, IResizable, ISaveable).

**`Composition vs Inheritance`**: Sometimes COMPOSITION (has-a) is better than INHERITANCE (is-a)! Car HAS-A Engine (composition) is better than Car IS-A Engine (wrong!).