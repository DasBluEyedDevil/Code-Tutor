---
type: "KEY_POINT"
title: "Flexible Constructor Bodies (Java 22+)"
---

Java 22+ allows initializing fields BEFORE calling super():

// Pre-Java 22 - had to call super() first
class Child extends Parent {
    final int computed;
    Child(int x) {
        super();  // Must be first!
        this.computed = x * 2;
    }
}

// Java 22+ - can initialize before super()
class Child extends Parent {
    final int computed;
    Child(int x) {
        this.computed = x * 2;  // Initialize first!
        super();
    }
}

This enables:
- Validate/transform arguments before passing to parent
- Initialize final fields with computed values
- Write cleaner constructor logic