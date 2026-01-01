---
type: "WARNING"
title: "Common Inheritance Pitfalls"
---

COMMON MISTAKES TO AVOID:

1. INHERITANCE FOR CODE REUSE ONLY:
   Stack extends Vector // BAD! Stack is NOT a Vector
   Prefer composition: class Stack { private List items; }

2. DEEP INHERITANCE HIERARCHIES:
   A -> B -> C -> D -> E  // Too deep! Hard to maintain
   Keep hierarchies shallow (2-3 levels max).

3. BREAKING PARENT CONTRACT:
   Override methods must honor parent behavior expectations.
   Violating Liskov Substitution Principle causes bugs.

4. FORGETTING super() CALL:
   If parent has no default constructor, must call super(...) explicitly.
   Java 22+ allows statements before super() for validation.

5. JAVA 17+ SEALED CLASSES:
   sealed class Shape permits Circle, Square {}
   Controls exactly which classes can extend yours.
   Enables exhaustive pattern matching in switch.

6. PREFER COMPOSITION OVER INHERITANCE:
   Composition is more flexible and avoids tight coupling.
   Use inheritance only for true IS-A relationships.